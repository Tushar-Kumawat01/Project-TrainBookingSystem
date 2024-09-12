using System;
using System.Collections.Generic;
using System.IO;
using TrainBookingSystem.Constants;
using TrainBookingSystem.DataLog;
using TrainBookingSystem.DataUtility;
using TrainBookingSystem.Utility;

namespace TrainBookingSystem.Validation
{
    internal class AuthValidatior
    {
        public static bool UsernameValidation(string username)
        {
            string str = SearchData.SearchUserDetails(username);
            if (!String.IsNullOrWhiteSpace(str))
            {
                PrintUtils.InputFailure("Username is Already Taken ,Try Again");
                Logger.LogAuthException("Credentials Mismatch", "Null or Empty Inputs by user");
                Console.ReadKey();
                return false;
            }
            return true;
        }
        public static bool IsCredentialLength(string InputData)
        {
            if (InputData.Length >= 4)
            {
                return true;
            }
            PrintUtils.InputFailure("Minimum 4 characters required in the input");
            Logger.LogAuthException("Credentials Mismatch", "Inputted Credentials are not of minimum 4 chracters");
            return false;
        }
        public static bool SignUpValidation(string username, string password, string confirmPassword)
        {
            string str = SearchData.SearchUserDetails(username);
            if (String.IsNullOrWhiteSpace(str))
            {
                if (password != confirmPassword)
                {
                    PrintUtils.InputFailure("Password and Confirm Password are not same");
                    Logger.LogAuthException("Credentials Mismatch", "password and confirm password does not match");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            PrintUtils.InputFailure("These Credentials are Already present in Record");
            return false;
        }

        public static bool LogInValidation(string username, string password)
        {
            FileInfo fileInfo = new FileInfo(Paths.UserPath);
            List<string> UserList = SearchData.FetchAllDetails(Paths.UserPath);

            for (int i = 0; i < UserList.Count; i++)
            {
                string[] str = UserList[i].Split(',');
                if (str[0] == username && str[1] == password)
                {
                    return true;
                }
            }
            Logger.LogAuthException("Credentials exception", "Username and Password doesn't match for " + username);
            return false;
        }
    }
}
