using System;
using System.IO;
using TrainBookingSystem.Constants;
using TrainBookingSystem.DataLog;
using TrainBookingSystem.DataUtility;
using TrainBookingSystem.Users;
using TrainBookingSystem.Utility;
using TrainBookingSystem.Validation;

namespace TrainBookingSystem.Auth
{
    internal class Authentication 
    {
        private User user = new User();
        public void AuthMenu()
        {
            Console.Clear();
            PrintUtils.ProjectHeader();
            Console.WriteLine("1. Log-In");
            Console.WriteLine("2. Sign-Up");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice : ");
            string option = Console.ReadLine().Trim();
            if (!PrintUtils.NullCheck(option)) AuthMenu();

            if (!int.TryParse(option, out int id))
            {
                PrintUtils.InputFailure("Enter a valid integer input");
                AuthMenu();
            }
            switch (id)
            {
                case 1:
                    LogIn();
                    break;
                case 2:
                    SignUp();
                    break;
                case 3:
                    Console.WriteLine("Double press any key to exit");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                default:
                    PrintUtils.InputFailure("Invalid choice");
                    AuthMenu();
                    break;
            }
        }
        private void LogIn()
        {
            Console.Clear();
            PrintUtils.ProjectHeader();
            PrintUtils.ProjectSubHeader("Log-In To your Account");

            PrintUtils.TakeInput("Enter UserName : ");
            user.UserName = Console.ReadLine();
            if (!PrintUtils.NullCheck(user.UserName)) LogIn();

            PrintUtils.TakeInput("Enter Password : ");
            user.Password = PrintUtils.HidePassword();


            if (!PrintUtils.NullCheck(user.Password)) LogIn();

            bool IsValid = AuthValidatior.LogInValidation(user.UserName, user.Password);
            string IsAuthorized = SearchData.GetUserStatus(user.UserName);

            if (IsAuthorized == "Block")
            {
                PrintUtils.InputFailure("You're Account Has Been Blocked\nTry Again");
                AuthMenu();
            }

            if (IsValid == true)
            {
                PrintUtils.InputSuccess("LogIn SuccessFul\nPress Any Key To Continue");
                Logger.LogAuthException("Login Success", user.UserName + " Logged In Successfully");
                WriteData.StoreCurrentUser(user.UserName);
                string UserRole = WriteData.GetCurrentUserRole();

                if (UserRole == "admin") Admin.AdminMenu();
                else
                {
                    Passenger.PassengerMenu();
                }
            }
            else
            {
                PrintUtils.InputFailure("Login Failed\nPress Any Key To Try Again");
                Logger.LogAuthException("Login Failure", user.UserName + " Login Unsuccessful");
                LogIn();
            }
        }


        private void SignUp()
        {
            Console.Clear();

            PrintUtils.ProjectHeader();
            PrintUtils.ProjectSubHeader("SignUp To Create Your Account");
            Console.WriteLine("( Inputs must be of Minimum 4 Characters )");
            FileInfo fileInfo = new FileInfo(Paths.UserPath);

            PrintUtils.TakeInput("Enter UserName : ");
            user.UserName = Console.ReadLine();
            if (!PrintUtils.NullCheck(user.UserName)) SignUp();
            if (!AuthValidatior.UsernameValidation(user.UserName)) SignUp();
            if (!AuthValidatior.IsCredentialLength(user.UserName)) SignUp();

            PrintUtils.TakeInput("Enter Password : ");
            user.Password = PrintUtils.HidePassword();
            if (!PrintUtils.NullCheck(user.Password)) SignUp();
            if (!AuthValidatior.IsCredentialLength(user.Password)) SignUp();

            Console.WriteLine();
            PrintUtils.TakeInput("Enter Confirm Password : ");
            string ConfirmPassword = PrintUtils.HidePassword();
            if (!PrintUtils.NullCheck(ConfirmPassword)) SignUp();
            if (!AuthValidatior.IsCredentialLength(ConfirmPassword)) SignUp();

            bool IsValid = AuthValidatior.SignUpValidation(user.UserName, user.Password, ConfirmPassword);

            if (IsValid == true)
            {
                using (StreamWriter sw = fileInfo.AppendText())
                {
                    sw.Write(String.Join(",", user.UserName, user.Password, user.Role = "user", user.Status = "Normal"));
                    sw.Write('\n');
                }
                PrintUtils.InputSuccess("SignUp SuccessFul\nPress Any Key To Log-In To Your Account");
                Logger.LogAuthException("SignUp Success", user.UserName + " Created Account Successfullt");
                LogIn();
            }
            else
            {
                AuthMenu();
            }
        }
    }
}
