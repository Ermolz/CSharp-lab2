using System;
using System.Threading.Tasks;

namespace CSharp_lab2.Models
{
    public class Person
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }

        private bool _isAdult;
        public bool IsAdult => _isAdult;

        private string _sunSign;
        public string SunSign => _sunSign;

        private string _chineseSign;
        public string ChineseSign => _chineseSign;

        private bool _isBirthday;
        public bool IsBirthday => _isBirthday;

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
        }

        public Person(string firstName, string lastName, string email)
            : this(firstName, lastName, email, default(DateTime))
        {
        }

        public Person(string firstName, string lastName, DateTime birthDate)
            : this(firstName, lastName, null, birthDate)
        {
        }

        public async Task ComputePropertiesAsync()
        {
            await Task.Delay(1000);
            _isAdult = (DateTime.Now.Year - BirthDate.Year -
                        (DateTime.Now < BirthDate.AddYears(DateTime.Now.Year - BirthDate.Year) ? 1 : 0)) >= 18;
            _sunSign = ComputeSunSign(BirthDate);
            _chineseSign = ComputeChineseSign(BirthDate);
            _isBirthday = (BirthDate.Day == DateTime.Now.Day && BirthDate.Month == DateTime.Now.Month);
        }

        private string ComputeSunSign(DateTime birthDate)
        {
            int day = birthDate.Day;
            int month = birthDate.Month;
            if ((month == 3 && day >= 21) || (month == 4 && day <= 19))
                return "Aries";
            if ((month == 4 && day >= 20) || (month == 5 && day <= 20))
                return "Taurus";
            if ((month == 5 && day >= 21) || (month == 6 && day <= 20))
                return "Gemini";
            if ((month == 6 && day >= 21) || (month == 7 && day <= 22))
                return "Cancer";
            if ((month == 7 && day >= 23) || (month == 8 && day <= 22))
                return "Leo";
            if ((month == 8 && day >= 23) || (month == 9 && day <= 22))
                return "Virgo";
            if ((month == 9 && day >= 23) || (month == 10 && day <= 22))
                return "Libra";
            if ((month == 10 && day >= 23) || (month == 11 && day <= 21))
                return "Scorpio";
            if ((month == 11 && day >= 22) || (month == 12 && day <= 21))
                return "Sagittarius";
            if ((month == 12 && day >= 22) || (month == 1 && day <= 19))
                return "Capricorn";
            if ((month == 1 && day >= 20) || (month == 2 && day <= 18))
                return "Aquarius";
            if ((month == 2 && day >= 19) || (month == 3 && day <= 20))
                return "Pisces";
            return "Unknown";
        }

        private string ComputeChineseSign(DateTime birthDate)
        {
            int year = birthDate.Year;
            string[] chineseZodiacs =
            {
                "Monkey", "Rooster", "Dog", "Pig",
                "Rat", "Ox", "Tiger", "Rabbit",
                "Dragon", "Snake", "Horse", "Goat"
            };
            return chineseZodiacs[year % 12];
        }
    }
}
