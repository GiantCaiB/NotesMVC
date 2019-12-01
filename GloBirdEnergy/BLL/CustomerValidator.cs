using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BLL
{
    public class CustomerValidator
    {
        /// <summary>
        /// Validation for age over 110
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public bool CheckAgeIsValid(DateTime dateOfBirth)
        {
            var maxAge = 110;
            var today = DateTime.Today;
            if (dateOfBirth.CompareTo(today)>=0)
            {
                return false;
            }
            if (dateOfBirth.CompareTo(today.AddYears(-maxAge)) < 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Check it's a valid Australian landline or number numbers
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public bool CheckIsAustralianNumber(string phoneNumber)
        {
            var regex = new Regex(@"^0[0-8]\d{8}$");
            return regex.IsMatch(phoneNumber);
        }
        /// <summary>
        /// Check input string is a blank or null string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool InputIsBlank(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }
        /// <summary>
        /// Check input string contains any number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool InputContainsNumber(string input)
        {
            return input.All(char.IsDigit);
        }
    }
}
