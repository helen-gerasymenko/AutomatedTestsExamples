using System;
using System.Globalization;
using System.Text;

namespace Ui.Tests
{
    public static class TestDataGenerator
    {
        private static readonly Random Rng = new Random();
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const string AlphatebicChars = "abcdefghijklmnopqrstuvwxyz";

        public static RoleData CreateValidRoleData(
            string code, 
            string name, 
            string licensee, 
            string description = null)
        {
            var result = new RoleData
            {
                RoleCode = code ?? GetRandomString(6),
                RoleName = name ?? "User" + GetRandomString(14),
                Licensee = licensee ?? "SampleLicensee",
                Description = description ?? "Description" + GetRandomString(6),
            };
            return result;
        }

        public static string GetRandomString(int size = 7, string charsToUse = Chars)
        {
            var buffer = new char[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = charsToUse[Rng.Next(charsToUse.Length)];
            }
            return new string(buffer);
        }

        public static AdminUserRegistrationData CreateValidAdminUserRegistrationData(
           string role, 
           string licensee, 
           string brand,
           string status,
           string currency)
        {
            var userName = "User" + GetRandomString(5);
            var result = new AdminUserRegistrationData()
            {
                UserName = userName,
                Password = userName,
                RepeatPassword = userName,
                Role = role,
                FirstName = "First-name" + GetRandomString(5),
                LastName = "Last-name" + GetRandomString(5),
                Status = status,
                Licensee = licensee,
                Brand = brand,
                Currency = currency,
                Description = "Description" + GetRandomString(5),
            };
            return result;
        }

        public static string GetRandomAlphabeticString(int size, string charsToUse = AlphatebicChars)
        {
            var buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = charsToUse[Rng.Next(charsToUse.Length)];
            }
            return new string(buffer);
        }

        #region help classes
        public class RoleData
        {
            public string RoleCode;
            public string RoleName;
            public string Licensee;
            public string Description;
        }

        public class AdminUserRegistrationData
        {
            public string UserName;
            public string Password;
            public string RepeatPassword;
            public string Role;
            public string FirstName;
            public string LastName;
            public string Language;
            public string Status;
            public string Description;
            public string Licensee;
            public string Brand;
            public string Currency;
        }

#endregion

        public static string GetRandomEmail()
        {
            return $"{GetRandomString()}@mailinator.com";
        }

        public static string GetRandomPhoneNumber(bool useDashes)
        {
            var rand = new Random();
            var telNumber = new StringBuilder(12);
            int number;

            for (int i = 0; i < 3; i++)
            {
                number = rand.Next(0, 8); // digit between 0 (incl) and 8 (excl)
                telNumber = telNumber.Append(number.ToString(CultureInfo.InvariantCulture));
            }

            if (useDashes)
                telNumber = telNumber.Append("-");
            number = rand.Next(0, 743); // number between 0 (incl) and 743 (excl)
            telNumber = telNumber.Append($"{number:D3}");

            if (useDashes)
                telNumber = telNumber.Append("-");
            number = rand.Next(0, 10000); // number between 0 (incl) and 10000 (excl)
            telNumber = telNumber.Append($"{number:D4}");
            return telNumber.ToString(); 
        }

        public static string GetRandomWebsiteUrl()
        {
            return $"http://www.{GetRandomString()}.com";
        }
    }
}