using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVM.Services
{
    public static class DataRepository
    {
        #region Fields

        private static readonly List<User> usersList = new List<User>();

        #endregion

        #region Methods

        public static bool AddUser(User user)
        {
            if (usersList.Any(u =>
                u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            usersList.Add(user);
            return true;
        }

        public static User FindUser(string email, string password)
        {
            return usersList.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)
                && u.Password == password);
        }

        public static bool EmailExists(string email)
        {
            return usersList.Any(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        #endregion
    }
}