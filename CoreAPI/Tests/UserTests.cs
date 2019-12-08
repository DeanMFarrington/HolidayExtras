using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using CoreAPI.Controllers;
using CoreAPI.Models;
using CoreAPI.Helpers;

namespace CoreAPI.Tests
{
    public class UserTests
    {
        protected UserController _userController;

        public UserTests()
        {
            _userController = new UserController();
        }

        [Fact]
        public void CanAddUser()
        {
            // Arrange
            User tempUser = new User(new Guid("b36944cf-d8a8-4ac5-ac9e-c59b6e0dab1a"),
                Formatter.FormatEmail("CreatedUser@TestSuite.co.uk"),
                Formatter.FormatName("Created"),
                Formatter.FormatName("User"),
                new DateTime(2019, 12, 8));

            // Act
            _userController.Put(tempUser.Id.ToString(), tempUser.Email, tempUser.GivenName, tempUser.FamilyName);

            //Assert
            string lastUser = _userController.Users[_userController.Users.Count - 1].UserContact();
            string tempUserContact = tempUser.UserContact();
            bool foundUser = lastUser == tempUserContact;

            Assert.True(foundUser);
        }

        [Fact]
        public void CanDeleteUser()
        {
            // Arrange
            User userToRemove = _userController.Users[_userController.Users.Count - 1];

            // Act
            _userController.Delete(userToRemove.Id.ToString());

            //Assert
            bool foundUser = false;
            for (int i = 0; i < _userController.Users.Count; i++)
            {
                if (_userController.Users[i].UserContact() == userToRemove.UserContact())
                    foundUser = true;
            }

            Assert.False(foundUser);
        }

        [Fact]
        public void CanUpdateUser()
        {
            // Arrange
            int userListCount = _userController.Users.Count;

            User userToUpdate = _userController.Users[userListCount - 1];
            string newEmail = "newemailaddress@testing.com";

            // Act
            _userController.Put(userToUpdate.Id.ToString(), newEmail, userToUpdate.GivenName, userToUpdate.FamilyName);

            //Assert
            bool updatedUser = _userController.Users[userListCount - 1].Email == newEmail;
            bool noNewUserAdded = userListCount == _userController.Users.Count;

            Assert.True(updatedUser && noNewUserAdded);
        }
    }
}
