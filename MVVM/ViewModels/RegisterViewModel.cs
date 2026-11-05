
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVM.Models;
using MVVM.Services;

namespace MVVM.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        #region Fields

        private string userName = string.Empty;
        private string email = string.Empty;
        private string password = string.Empty;
        private string confirmPassword = string.Empty;

        private string userNameError = string.Empty;
        private string emailError = string.Empty;
        private string passwordError = string.Empty;
        private string confirmPasswordError = string.Empty;
        private string dateOfBirthError = string.Empty;

        private DateTime dateOfBirth = DateTime.Today;

        #endregion

        #region Properties

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public string UserNameError
        {
            get => userNameError;
            set
            {
                userNameError = value;
                OnPropertyChanged();
            }
        }

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged();
            }
        }

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPasswordError
        {
            get => confirmPasswordError;
            set
            {
                confirmPasswordError = value;
                OnPropertyChanged();
            }
        }

        public string DateOfBirthError
        {
            get => dateOfBirthError;
            set
            {
                dateOfBirthError = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }

        #endregion

        #region Constructor

        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () => await Register());
            LoginCommand = new Command(async () => await Login());
        }

        #endregion

        #region Methods

        private async Task Register()
        {
            UserNameError = "";
            EmailError = "";
            PasswordError = "";
            ConfirmPasswordError = "";
            DateOfBirthError = "";

            string username = UserName;
            string emailAddress = Email;
            string userPassword = Password;
            string confirmPassword = ConfirmPassword;

            DateTime birthday = DateOfBirth.Date;
            DateTime today = DateTime.Today;

            if (string.IsNullOrWhiteSpace(username))
            {
                UserNameError = "Username can't be empty.";
                return;
            }

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                EmailError = "Email can't be empty.";
                return;
            }

            if (!Regex.IsMatch(emailAddress,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                EmailError = "Please enter a valid email address.";
                return;
            }

            if (string.IsNullOrWhiteSpace(userPassword))
            {
                PasswordError = "Password can't be empty.";
                return;
            }

            if (!Regex.IsMatch(userPassword,
                @"^(?=.*[A-Z])(?=.*\d)\S{5,}$"))
            {
                PasswordError = "Password needs 5 characters, one capital letter, one number, and no spaces.";
                return;
            }

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                ConfirmPasswordError = "Please confirm your password.";
                return;
            }

            if (userPassword != confirmPassword)
            {
                ConfirmPasswordError = "Passwords don't match.";
                return;
            }

            int age = today.Year - birthday.Year;

            if (birthday.Date > today.AddYears(-age))
            {
                age--;
            }

            if (age < 14)
            {
                DateOfBirthError = "You must be at least 14 years old.";
                return;
            }

            // Create the new user.
            User newUser = new User
            {
                UserName = username,
                Email = emailAddress,
                Password = userPassword,
                DateBirth = birthday.ToString("yyyy-MM-dd")
            };

            // Save the user in the repository.
            bool added = DataRepository.AddUser(newUser);

            if (!added)
            {
                EmailError = "This email is already registered.";
                return;
            }

            await Application.Current.MainPage.DisplayAlert(
                "Registration Successful",
                "Your account has been created! You can now log in.",
                "OK");

            // Return to the existing login page.
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task Login()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion
    }
}