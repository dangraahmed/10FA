using Newtonsoft.Json;
using System;
using Xunit;

namespace BusinessLogic.Test
{
    public class ValidateSEDOLTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("12")]
        [InlineData("123456789")]
        public void InputStringWasNot7charactersLong(string sedol)
        {
            var actual = new ValidateSEDOL().IsValid(sedol);
            var expected = new SedolValidationResult()
            {
                InputString = sedol,
                IsValidSedol = false,
                IsUserDefined = false,
                ValidationDetails = Constants.INPUT_STRING_NOT_VALID_LENGTH
            };

            Assert.Equal(expected.ToJson(), actual.ToJson());
        }

        [Theory]
        [InlineData("1234567")]
        public void ChecksumDigitDoesNotAgreeWithTheRestOfTheInput(string sedol)
        {
            var actual = new ValidateSEDOL().IsValid(sedol);
            var expected = new SedolValidationResult()
            {
                InputString = sedol,
                IsValidSedol = false,
                IsUserDefined = false,
                ValidationDetails = Constants.CHECKSUM_NOT_VALID
            };

            Assert.Equal(expected.ToJson(), actual.ToJson());
        }

        [Theory]
        [InlineData("0709954")]
        [InlineData("B0YBKJ7")]
        public void ValidSEDOL(string sedol)
        {
            var actual = new ValidateSEDOL().IsValid(sedol);
            var expected = new SedolValidationResult()
            {
                InputString = sedol,
                IsValidSedol = true,
                IsUserDefined = false,
                ValidationDetails = null
            };

            Assert.Equal(expected.ToJson(), actual.ToJson());
        }

        [Theory]
        [InlineData("9123451")]
        [InlineData("9ABCDE8")]
        public void ChecksumDigitDoesNotAgreeWithTheRestOfTheInputUserDefine(string sedol)
        {
            var actual = new ValidateSEDOL().IsValid(sedol);
            var expected = new SedolValidationResult()
            {
                InputString = sedol,
                IsValidSedol = false,
                IsUserDefined = true,
                ValidationDetails = Constants.CHECKSUM_NOT_VALID
            };

            Assert.Equal(expected.ToJson(), actual.ToJson());
        }

        [Theory]
        [InlineData("9123_51")]
        [InlineData("VA.CDE8")]
        public void SEDOLContainsInvalidCharacters(string sedol)
        {
            var actual = new ValidateSEDOL().IsValid(sedol);
            var expected = new SedolValidationResult()
            {
                InputString = sedol,
                IsValidSedol = false,
                IsUserDefined = false,
                ValidationDetails = Constants.INPUT_STRING_NOT_ALPHANUMERIC
            };

            Assert.Equal(expected.ToJson(), actual.ToJson());
        }

        [Theory]
        [InlineData("9123458")]
        [InlineData("9ABCDE1")]
        public void ValidSEDOLUserDefined(string sedol)
        {
            var actual = new ValidateSEDOL().IsValid(sedol);
            var expected = new SedolValidationResult()
            {
                InputString = sedol,
                IsValidSedol = true,
                IsUserDefined = true,
                ValidationDetails = null
            };

            Assert.Equal(expected.ToJson(), actual.ToJson());
        }

    }
}
