using System;

namespace App.services
{
    public class CustomerValidationService
    {
        public bool IsValid(string firstName, string sureName, string email)
        {
            if ((string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(sureName)))
            {
                return false;
            }

            if ((!email.Contains("@") && !email.Contains(".")))
            {
                return false;
            }
            return true;
        }

        public bool IsValidAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }
            return true;
        }
    }
}
