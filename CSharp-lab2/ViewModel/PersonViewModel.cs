using CSharp_lab2.Commands;
using CSharp_lab2.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CSharp_lab2.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); ValidateFields(); }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); ValidateFields(); }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); ValidateFields(); }
        }

        private DateTime? _birthDate;
        public DateTime? BirthDate
        {
            get => _birthDate;
            set { _birthDate = value; OnPropertyChanged(); ValidateFields(); }
        }

        private bool _isAdult;
        public bool IsAdult
        {
            get => _isAdult;
            private set { _isAdult = value; OnPropertyChanged(); }
        }

        private string _sunSign = string.Empty;
        public string SunSign
        {
            get => _sunSign;
            private set { _sunSign = value; OnPropertyChanged(); }
        }

        private string _chineseSign = string.Empty;
        public string ChineseSign
        {
            get => _chineseSign;
            private set { _chineseSign = value; OnPropertyChanged(); }
        }

        private bool _isBirthday;
        public bool IsBirthday
        {
            get => _isBirthday;
            private set { _isBirthday = value; OnPropertyChanged(); }
        }

        public ICommand ProceedCommand { get; }

        private bool _canProceed;
        public bool CanProceed
        {
            get => _canProceed;
            private set { _canProceed = value; OnPropertyChanged(); }
        }

        public PersonViewModel()
        {
            ProceedCommand = new AsyncRelayCommand(ExecuteProceedAsync, () => CanProceed);
        }

        private void ValidateFields()
        {
            CanProceed = !string.IsNullOrWhiteSpace(FirstName) &&
                         !string.IsNullOrWhiteSpace(LastName) &&
                         !string.IsNullOrWhiteSpace(Email) &&
                         BirthDate.HasValue;
            (ProceedCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
        }

        private async Task ExecuteProceedAsync()
        {
            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Invalid email format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (BirthDate.Value > DateTime.Now)
            {
                MessageBox.Show("Birth date cannot be in the future.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int age = DateTime.Now.Year - BirthDate.Value.Year;
            if (BirthDate.Value.AddYears(age) > DateTime.Now)
                age--;
            if (age > 135)
            {
                MessageBox.Show("Age cannot be greater than 135 years.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var person = new Person(FirstName, LastName, Email, BirthDate.Value);
            await person.ComputePropertiesAsync();
            IsAdult = person.IsAdult;
            SunSign = person.SunSign;
            ChineseSign = person.ChineseSign;
            IsBirthday = person.IsBirthday;
            if (IsBirthday)
            {
                MessageBox.Show("Happy Birthday! Wishing you a wonderful day!", "Birthday Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            string result = $"First Name: {FirstName}\n" +
                            $"Last Name: {LastName}\n" +
                            $"Email: {Email}\n" +
                            $"Birth Date: {BirthDate.Value:d}\n" +
                            $"IsAdult: {IsAdult}\n" +
                            $"SunSign: {SunSign}\n" +
                            $"ChineseSign: {ChineseSign}\n" +
                            $"IsBirthday: {IsBirthday}";
            MessageBox.Show(result, "Result");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
