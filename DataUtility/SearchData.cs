using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TrainBookingSystem.Constants;
using TrainBookingSystem.DataLog;
using TrainBookingSystem.Users;
using TrainBookingSystem.Utility;

namespace TrainBookingSystem.DataUtility
{
    internal class SearchData
    {

        //Fetch All Records According to path
        public static List<string> FetchAllDetails(string path)
        {
            try
            {
                List<string> strings = File.ReadAllLines(path).ToList();
                return strings;
            }
            catch (Exception ex)
            {
                PrintUtils.InputFailure("File is been used already , Close the file");
                Logger.LogFileException(path);
                Passenger.PassengerMenu();
                throw ex;
            }
        }

        //METHOD OVERLOADING
        //Search a Particular Data in Train.csv
        public static string SearchTrainDetails(int TrainId, string path)
        {
            if (!IsFileOpen(path))
            {
                List<string> str = FetchAllDetails(path);
                for (int i = 0; i < str.Count; i++)
                {
                    string[] newStr = str[i].Split(',');

                    if (newStr[0] == TrainId.ToString())
                    {
                        return str[i];
                    }
                }
            }
            else
                PrintUtils.FileOpenException();
            return null;
        }

        public static string SearchTrainDetails(int TrainId)
        {
            if (!IsFileOpen(Paths.TrainPath))
            {
                List<string> str = FetchAllDetails(Paths.TrainPath);
                for (int i = 0; i < str.Count; i++)
                {
                    string[] newStr = str[i].Split(',');

                    if (newStr[0] == TrainId.ToString())
                    {
                        return str[i];
                    }
                }
            }
            else
                PrintUtils.FileOpenException();
            return null;
        }

        public static List<string> SearchBookedUser(string TicketId)
        {
            if (!IsFileOpen(Paths.BookedTickets))
            {
                List<string> str = FetchAllDetails(Paths.BookedTickets);
                List<string> users = new List<string>();
                for (int i = 0; i < str.Count; i++)
                {
                    string[] newStr = str[i].Split(',');

                    if (newStr[0] == TicketId.ToString())
                    {
                        users.Add(str[i]);
                    }
                }
                return users;
            }
            else
                PrintUtils.FileOpenException();
            return null;
        }


        //Search a Particular Data in Users.csv
        public static string SearchUserDetails(string Username)
        {
            if (!IsFileOpen(Paths.UserPath))
            {
                List<string> str = FetchAllDetails(Paths.UserPath);
                for (int i = 0; i < str.Count; i++)
                {
                    string[] newStr = str[i].Split(',');

                    if (newStr[0] == Username)
                    {
                        return str[i];
                    }
                }
            }
            else
                PrintUtils.FileOpenException();
            return null;
        }
        //METHOD OVERLOADING

        public static List<string> SearchTicketDetails()
        {
            if (!IsFileOpen(Paths.TicketPath) && !IsFileOpen(Paths.CurrentUserPath))
            {
                List<string> str = FetchAllDetails(Paths.TicketPath);
                List<string> CurrentUser = FetchAllDetails(Paths.CurrentUserPath);
                string[] username = CurrentUser[0].Split(',');
                List<string> ticket = new List<string>();
                for (int i = 0; i < str.Count; i++)
                {
                    string[] newStr = str[i].Split(',');

                    string.Equals(newStr[2], username[0]);
                    newStr[2].Equals(username[0]);
                    if (newStr[2] == username[0])
                    {
                        ticket.Add(str[i]);
                    }
                }
                return ticket;
            }
            else
                PrintUtils.FileOpenException();
            return null;
        }
        public static bool SearchTicketDetails(string TicketId)
        {
            if (!IsFileOpen(Paths.TicketPath) && !IsFileOpen(Paths.CurrentUserPath))
            {
                List<string> str = FetchAllDetails(Paths.TicketPath);
                List<string> CurrentUser = FetchAllDetails(Paths.CurrentUserPath);
                string[] username = CurrentUser[0].Split(',');
                List<string> ticket = new List<string>();
                for (int i = 0; i < str.Count; i++)
                {
                    string[] newStr = str[i].Split(',');

                    if (newStr[2] == username[0] && newStr[0] == TicketId)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
                PrintUtils.FileOpenException();
            return false;
        }

        public static string[] SearchTicketDetails(string TicketId, int check1 = 1)
        {
            if (!IsFileOpen(Paths.TicketPath) && !IsFileOpen(Paths.CurrentUserPath))
            {
                List<string> str = FetchAllDetails(Paths.TicketPath);
                List<string> CurrentUser = FetchAllDetails(Paths.CurrentUserPath);
                string[] username = CurrentUser[0].Split(',');
                List<string> ticket = new List<string>();
                for (int i = 0; i < str.Count; i++)
                {
                    string[] newStr = str[i].Split(',');

                    if (newStr[2] == username[0] && newStr[0] == TicketId)
                    {
                        return newStr;
                    }
                }
            }
            else
                PrintUtils.FileOpenException();
            return null;
        }

        //Fetch UserStatus - Blocked or Not 
        public static string GetUserStatus(string username)
        {
            string CheckStr = SearchUserDetails(username);
            if (!string.IsNullOrWhiteSpace(CheckStr))
            {
                string[] str = CheckStr.Split(',');
                return str[3];
            }
            return null;
        }

        public static List<string> SearchTrainsBySource(string source)
        {
            if (!IsFileOpen(Paths.AllStations))
            {
                List<string> StationsData = FetchAllDetails(Paths.AllStations);
                List<string> output = new List<string>();
                for (int i = 0; i < StationsData.Count; i++)
                {
                    string[] split = StationsData[i].Split(',');
                    int sourceindex = split.ToList().IndexOf(source);

                    if (sourceindex + 1 < split.Length && !String.IsNullOrWhiteSpace(split[sourceindex + 1]) && sourceindex >= 0)
                    {
                        output.Add(StationsData[i]);
                    }
                }
                return output;
            }
            else
                PrintUtils.FileOpenException();
            return null;
        }

        public static List<string> SearchTrainsDestination(string source, string destination)
        {
            if (!IsFileOpen(Paths.AllStations))
            {
                List<string> output = new List<string>();
                List<string> StationsData = FetchAllDetails(Paths.AllStations);
                for (int i = 0; i < StationsData.Count; i++)
                {
                    string[] split = StationsData[i].Split(',');
                    int sourceindex = split.ToList().IndexOf(source);
                    int destinationindex = split.ToList().IndexOf(destination);

                    if (destinationindex - sourceindex > 0 && destinationindex > 0 && sourceindex >= 0)
                    {
                        output.Add(StationsData[i]);
                    }
                }
                return output;
            }
            else
                PrintUtils.FileOpenException();
            return null;
        }
        public static List<string> GetBookedTickets(string TicketId)
        {
            if (!IsFileOpen(Paths.BookedTickets))
            {
                List<string> data = FetchAllDetails(Paths.BookedTickets);
                List<string> output = new List<string>();

                for (int i = 0; i < data.Count; i++)
                {
                    string[] split = data[i].Split(',');

                    if (split[0] == TicketId)
                    {
                        output.Add(data[i]);
                    }
                }
                return output;
            }
            else
                PrintUtils.FileOpenException();
            return null;
        }

        public static string GetAllStations(string TrainNumber)
        {
            if (!IsFileOpen(Paths.AllStations))
            {
                List<string> data = FetchAllDetails(Paths.AllStations);

                for (int i = 0; i < data.Count; i++)
                {
                    string[] split = data[i].Split(',');

                    if (split[0] == TrainNumber)
                    { return data[i]; }
                }
            }
            else
                PrintUtils.FileOpenException();
            return null;
        }

        public static string GetStationTimings(string TrainNumber)
        {
            if (!IsFileOpen(Paths.StationTiming))
            {
                List<string> data = FetchAllDetails(Paths.StationTiming);

                for (int i = 0; i < data.Count; i++)
                {
                    string[] split = data[i].Split(',');
                    if (split[i] == TrainNumber) { return data[i]; }
                }
            }
            else
                PrintUtils.FileOpenException();
            return null;
        }

        public static bool IsFileOpen(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return false;
                }
            }
            catch (IOException)
            { 
            return true;
            }
            
        }
    }
}
