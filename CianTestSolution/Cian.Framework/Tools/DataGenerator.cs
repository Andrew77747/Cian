using System;
using System.Text;

namespace Cian.Framework.Tools
{
    public class DataGenerator
    {
        public static string GenerateRandomString(int size, bool lowerCase = true)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();

            char ch;

            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32((Math.Floor(26 * random.NextDouble() + 65))));
                stringBuilder.Append(ch);
            }

            if (lowerCase)
                return stringBuilder.ToString().ToLower();

            return stringBuilder.ToString();
        }

        public static string GenerateRandomEmail(string nameDomen, int size = 10)
        {
            return GenerateRandomString(size) + nameDomen;
        }

        public static string GenerateRandomData(int size)
        {
            int[] array = new int[size];
            Random random = new Random();
            string data = "";

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(33, 125);
                data += (char)array[i];
            }

            return data;
        }

        public static string GenerateRandomPassword(int size)
        {
            return GenerateRandomData(size);
        }

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            var random = new Random();

            return random.Next(minValue, maxValue);
        }

        public static string GenerateRandomNumbers(int minValue, int maxValue, int size)
        {
            string resultNumber = null;

            for (int i = 0; i < size; i++)
            {
                resultNumber += GenerateRandomNumber(minValue, maxValue);
            }

            return resultNumber;
        }

        public static string GenerateRandomPhoneNumber(int countryCode)
        {
            int numberLength = 0;

            switch (countryCode)
            {
                case 7:
                    numberLength = 10;
                    break;
                case 46:
                    numberLength = 9;
                    break;
                case 375:
                    numberLength = 9;
                    break;
            }

            var random = new Random();
            int[] array = new int[numberLength];
            string phoneNumber = "";

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(0, 9);
                phoneNumber += array[i];
            }

            phoneNumber = countryCode + phoneNumber;

            return phoneNumber;
        }
    }
}