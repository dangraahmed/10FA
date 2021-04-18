using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    public class ValidateSEDOL
    {
        public ISedolValidationResult IsValid(string valueToCheck)
        {

            var sedol = new Sedol(valueToCheck);

            var sedolValidationResult = new SedolValidationResult
            {
                InputString = valueToCheck,
                IsUserDefined = false,
                IsValidSedol = false,
                ValidationDetails = null
            };

            if (!sedol.IsValidLength)
            {
                sedolValidationResult.ValidationDetails = Constants.INPUT_STRING_NOT_VALID_LENGTH;
                return sedolValidationResult;
            }
            else if (!sedol.IsAlphaNumeric)
            {
                sedolValidationResult.ValidationDetails = Constants.INPUT_STRING_NOT_ALPHANUMERIC;
                return sedolValidationResult;
            }
            else if (sedol.IsUserDefined)
            {
                sedolValidationResult.IsUserDefined = true;
                if (sedol.HasValidCheckDigit)
                {
                    sedolValidationResult.IsValidSedol = true;
                    return sedolValidationResult;
                }
                sedolValidationResult.ValidationDetails = Constants.CHECKSUM_NOT_VALID;
                return sedolValidationResult;
            }
            else if (sedol.HasValidCheckDigit)
            {
                sedolValidationResult.IsValidSedol = true;
            }
            else
            {
                sedolValidationResult.ValidationDetails = Constants.CHECKSUM_NOT_VALID;
            }

            return sedolValidationResult;
        }


    }
}
