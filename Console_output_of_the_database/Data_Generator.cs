using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_output_of_the_database
{
    public class Data_Generator
    {
        public static Random random = new Random();
        public static string GenerateRandomName()
        {
            string firstName = GenerateRandomString(random.Next(5, 11));
            string lastName = GenerateRandomString(random.Next(5, 11));
            string patronymic = GenerateRandomString(random.Next(5, 11));

            return firstName + '_' + lastName + '_' + patronymic;
        }
        public static string GenerateRandomString(int length)
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] nameArray = new char[length];
            for (int i = 0; i < length; i++)
            {
                nameArray[i] = alphabet[random.Next(alphabet.Length)];
            }
            return new string(nameArray);
        }
        public static string GenerateRandomGender()
        {
            string g = "";
            if (random.Next(0, 2) == 0)
            {
                g = "m";
            }
            else
            {
                g = "f";
            }
            return g;
        }
        public static DateTime GenerateRandomBirthDate()
        {
            DateTime startDate = new DateTime(1950, 1, 1);
            DateTime endDate = new DateTime(2023, 7, 30);
            int range = (endDate - startDate).Days;
            int randomDays = random.Next(range);
            DateTime randomBirthDate = startDate.AddDays(randomDays);
            return randomBirthDate;
        }
    }
}
