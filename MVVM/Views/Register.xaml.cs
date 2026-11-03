using System.Text.RegularExpressions;
using MVVM.Models;
using MVVM.Services;
using System;

namespace MVVM.Views;

public partial class Register : ContentPage
{
    #region Constructor

    public Register()
    {
        InitializeComponent();
    }

    #endregion

    #region Navigation

    private async void OnLoginClicked(object sender, TappedEventArgs e)
    {
        await Navigation.PopAsync();
    }

    #endregion

    #region Registration

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        string username = UserName.Text;
        string email = EmailAdress.Text;
        string password = PassWord.Text;
        string confirmPassword = ConfirmPassWord.Text;

        DateTime birthday = DateOfBirth.Date;
        DateTime today = DateTime.Today;

        errUserName.Text = "";
        errEmail.Text = "";
        errPassword.Text = "";
        errConfirmPassword.Text = "";
        errDateOfBirth.Text = "";

        if (username == null || username == "")
        {
            errUserName.Text = "Username can't be empty.";
            return;
        }

        if (email == null || email == "")
        {
            errEmail.Text = "Email can't be empty.";
            return;
        }

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            errEmail.Text = "Please enter a valid email address.";
            return;
        }

        if (password == null || password == "")
        {
            errPassword.Text = "Password can't be empty.";
            return;
        }
        if (!Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*\d)\S{5,}$"))
        {
            errPassword.Text = "Password needs 5 characters, one capital letter, one number, and no spaces.";
            return;
        }

        if (confirmPassword == null || confirmPassword == "")
        {
            errConfirmPassword.Text = "Please confirm your password.";
            return;
        }

        if (password != confirmPassword)
        {
            errConfirmPassword.Text = "Passwords don't match.";
            return;
        }

        int age = today.Year - birthday.Year;

        if (birthday.Date > today.AddYears(-age))
        {
            age--;
        }

        if (age < 14)
        {
            errDateOfBirth.Text = "You must be at least 14 years old.";
            return;
        }

        // Create the new user.
        User newUser = new User
        {
            UserName = username,
            Email = email,
            Password = password,
            DateBirth = birthday.ToString("yyyy-MM-dd")
        };

        // Save the user in our fake-data repository.
        bool added = DataRepository.AddUser(newUser);

        if (!added)
        {
            errEmail.Text = "This email is already registered.";
            return;
        }

        await DisplayAlert(
            "Registration Successful",
            "Your account has been created! You can now log in.",
            "OK");

        // Return to the existing login page.
        await Navigation.PopAsync();
    }

    #endregion
}