using System;
using TrainBookingSystem.DataLog;
using TrainBookingSystem.DataUtility;
using TrainBookingSystem.Utility;
using TrainBookingSystem.Validation;
using TrainBookingSystem.Auth;

namespace TrainBookingSystem.Users
{
    internal class Account
    {
        public void AccountMenu()
        {
            Console.Clear();
            PrintUtils.ProjectSubHeader("My Account");

            Console.WriteLine("1. Reset Password");
            Console.WriteLine("2. Delete Account");
            Console.WriteLine("3. Log out");
            Console.WriteLine("4. Go Back to admin menu");
            Console.Write("Enter your choice : ");
            string choice = Console.ReadLine();

            if (!PrintUtils.NullCheck(choice)) AccountMenu();

            if (!int.TryParse(choice, out int id)) AccountMenu();

            switch (id)
            {
                case 1:
                    ResetUserPassword();
                    break;
                case 2:
                    DeleteUserAccount();
                    break;
                case 3:
                    LogOut();
                    break;
                case 4:
                    Console.WriteLine("Back to menu");
                    return;
                default:
                    PrintUtils.InputFailure("Invalid choice");
                    break;
            }
        }
 
        private void ResetUserPassword()
        {
            Console.Clear();
            Console.WriteLine("Reset password");
            Console.WriteLine("Enter your username :");
            string username = Console.ReadLine();

            if (!PrintUtils.NullCheck(username)) ResetUserPassword();

            string CurrentUsername = WriteData.GetCurrentUserName();
            string password = "";
            string confirmPassword = "";
            if (username != CurrentUsername)
            {
                PrintUtils.InputFailure("You are not authorized to delete other users account");
                Console.ReadKey();
                AccountMenu();
            }
            Console.WriteLine("Enter your new password");
            password = Console.ReadLine();
            if (!PrintUtils.NullCheck(password)) ResetUserPassword();
            if (!AuthValidatior.IsCredentialLength(password)) ResetUserPassword();


            Console.WriteLine("Confirm your new password");
            confirmPassword = Console.ReadLine();
            if (!PrintUtils.NullCheck(confirmPassword)) ResetUserPassword();
            if (!AuthValidatior.IsCredentialLength(confirmPassword)) ResetUserPassword();


            Console.WriteLine("Press any key");
            UpdateData.UpdateUserPassword(username, password);
            Console.ReadKey();
            AccountMenu();

        }
        private void DeleteUserAccount()
        {
            Console.Clear();
            Console.WriteLine("Permanently delete your account");
            Console.WriteLine("Enter your username :");
            string CurrentUsername = WriteData.GetCurrentUserName();
            string CurrentUserPassword = WriteData.GetCurrentPassword();

            string username = Console.ReadLine();
            if (!PrintUtils.NullCheck(username)) DeleteUserAccount();
            if (username != CurrentUsername)
            {
                PrintUtils.InputFailure("Username is not correct");
                AccountMenu();
            }

            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();
            if (!PrintUtils.NullCheck(password)) DeleteUserAccount();
            if (password != CurrentUserPassword)
            {
                PrintUtils.InputFailure("Password is not correct");
                AccountMenu();
            }
            DeleteData.DeleteUserDetails(username);
            Logger.LogDeleteAccount(username);
            PrintUtils.InputFailure("Your account has been deleted");
            Environment.Exit(0);
        }

        private void LogOut()
        {
            PrintUtils.InputSuccess("You've been logged out from your account");
        }
    }
}
