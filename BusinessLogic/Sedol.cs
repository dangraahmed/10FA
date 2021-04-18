using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    class Sedol
    {
        private readonly string _sedolValue;
        private readonly List<int> _weights;
        private const int CHECK_DIGIT_IDX = 6;
        private const int SEDOL_LENGTH = 7;
        private const int USER_DEFINED_IDX = 0;
        private const char USER_DEFINED_CHAR = '9';

        public Sedol(string input)
        {
            // Could potentially inject weights
            _weights = new List<int> { 1, 3, 1, 7, 3, 9 };
            _sedolValue = input;
        }

        public static int Code(char input)
        {
            if (Char.IsLetter(input))
                return Char.ToUpper(input) - 55;
            return input - 48;
        }

        public char CheckDigit
        {
            get
            {
                var codes = _sedolValue.Take(SEDOL_LENGTH - 1).Select(Code).ToList();
                var weightedSum = _weights.Zip(codes, (w, c) => w * c).Sum();
                return Convert.ToChar(((10 - (weightedSum % 10)) % 10).ToString(CultureInfo.InvariantCulture));
            }
        }

        public bool IsAlphaNumeric
        {
            get { return Regex.IsMatch(_sedolValue, "^[a-zA-Z0-9]*$"); }
        }

        public bool IsValidLength
        {
            get { return !String.IsNullOrEmpty(_sedolValue) && _sedolValue.Length == SEDOL_LENGTH; }
        }

        public bool IsUserDefined
        {
            get { return _sedolValue[USER_DEFINED_IDX] == USER_DEFINED_CHAR; }
        }

        public bool HasValidCheckDigit
        {
            get { return _sedolValue[CHECK_DIGIT_IDX] == CheckDigit; }
        }
    }
}
