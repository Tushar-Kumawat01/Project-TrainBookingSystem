using System;
using System.Collections.Generic;
using System.IO;
using TrainBookingSystem.Constants;
using TrainBookingSystem.DataLog;
using TrainBookingSystem.Trains;
using TrainBookingSystem.Users;
using TrainBookingSystem.Utility;
namespace TrainBookingSystem.DataUtility
{
    internal class WriteData
    {
        //Write Train Details Taken From The User
        public static void AddTrainDetails(Train train)
        {
            if (File.Exists(Paths.TrainPath) && !SearchData.IsFileOpen(Paths.TrainPath))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(Paths.TrainPath);
                    using (StreamWriter sw = fileInfo.AppendText())
                    {
                        sw.WriteLine(String.Join(",", train.TrainId, train.TrainName, train.OnBoarding, train.Destination, train.NoOfSeats));
                    }
                }
                catch (Exception)
                {
                    
                    Logger.LogFileException(Paths.TrainPath);
                    PrintUtils.FileOpenException();
                }
            }
        }

        //Write User Details Taken From The User
        public static void AddUserDetails(User user)
        {
            if (File.Exists(Paths.UserPath) && !SearchData.IsFileOpen(Paths.UserPath))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(Paths.UserPath);
                    using (StreamWriter sw = fileInfo.AppendText())
                    {
                        sw.WriteLine(String.Join(",", user.UserName, user.Password, user.Role = "user", user.Status = "working"));
                    }
                }
                catch (Exception)
                {
                    Logger.LogFileException(Paths.UserPath);
                    PrintUtils.FileOpenException();
                }
            }
        }

        //Write Ticket Details Taken From The User
        public static void AddTicketDetails(params string[] Details)
        {
            if (File.Exists(Paths.TicketPath) && !SearchData.IsFileOpen(Paths.TicketPath))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(Paths.TicketPath);
                    using (StreamWriter sw = fileInfo.AppendText())
                    {
                        sw.WriteLine(String.Join(",", Details));
                    }
                }
                catch (Exception)
                {
                    Logger.LogFileException(Paths.TicketPath);
                    PrintUtils.FileOpenException();
                }
            }
        }

        //Write Ticket Details After Deletion of a particular user
        public static void AddUpdatedTicketDetails(List<string> Details)
        {
            if (File.Exists(Paths.TicketPath) && !SearchData.IsFileOpen(Paths.TicketPath))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(Paths.TicketPath);
                    fileInfo.Delete();
                    using (StreamWriter sw = fileInfo.AppendText())
                    {
                        for (int i = 0; i < Details.Count; i++)
                        {
                            String[] newStr = Details[i].Split(',');
                            sw.WriteLine(String.Join(",", newStr));
                        }
                    }
                }
                catch (Exception)
                {
                    Logger.LogFileException(Paths.TicketPath);
                    PrintUtils.FileOpenException();
                }
            }
        }

        //Write Train Details After Updating From The User
        public static void AddAllUserDetails(List<string> list)
        {
            if (File.Exists(Paths.UserPath) && !SearchData.IsFileOpen(Paths.UserPath))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(Paths.UserPath);
                    fileInfo.Delete();
                    using (StreamWriter sw = fileInfo.AppendText())
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            string[] str = list[i].Split(',');
                            sw.WriteLine(String.Join(",", str[0], str[1], str[2], str[3]));
                        }
                    }
                }
                catch (Exception)
                {
                    Logger.LogFileException(Paths.UserPath);
                    PrintUtils.FileOpenException();
                }
            }
        }

        //Add Modified data into the Train Data list
        public static void AddAllTrainDetails(List<string> list)
        {
            if (File.Exists(Paths.TrainPath) && !SearchData.IsFileOpen(Paths.TrainPath))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(Paths.TrainPath);
                    File.Delete(Paths.TrainPath);
                    using (StreamWriter sw = fileInfo.AppendText())
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                            string[] str = list[j].Split(',');
                            sw.WriteLine(String.Join(",", str[0], str[1], str[2], str[3], str[4], str[5]));
                        }

                    }
                }
                catch (Exception)
                {
                    Logger.LogFileException(Paths.TrainPath);
                    PrintUtils.FileOpenException();
                }
            }
        }

        public static string GetCurrentUserName()
        {
            if (!SearchData.IsFileOpen(Paths.CurrentUserPath))
            {
                List<string> UserFile = SearchData.FetchAllDetails(Paths.CurrentUserPath);
                string[] Username = UserFile[0].Split(',');
                return Username[0];
            }
            else PrintUtils.FileOpenException();
            return null;
        }
        public static string GetCurrentPassword()
        {
            if (!SearchData.IsFileOpen(Paths.CurrentUserPath))
            {
                List<string> UserFile = SearchData.FetchAllDetails(Paths.CurrentUserPath);
                string[] Username = UserFile[0].Split(',');
                return Username[1];
            }
            else
                PrintUtils.FileOpenException();
            return null;
        }

        public static string GetCurrentUserRole()
        {
            if (!SearchData.IsFileOpen(Paths.CurrentUserPath))
            {
                List<string> UserFile = SearchData.FetchAllDetails(Paths.CurrentUserPath);
                string[] Username = UserFile[0].Split(',');
                return Username[2];
            }
            else
                PrintUtils.FileOpenException();
            return null;
        }
        public static void AddCurrentUserDetails(String str)
        {
            if (File.Exists(Paths.CurrentUserPath) && !SearchData.IsFileOpen(Paths.CurrentUserPath))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(Paths.CurrentUserPath);
                    fileInfo.Delete();
                    using (StreamWriter sw = fileInfo.AppendText())
                    {
                        string[] CheckStr = str.Split(',');
                        sw.WriteLine(String.Join(",", CheckStr[0], CheckStr[1], CheckStr[2], CheckStr[3]));
                    }
                }
                catch (Exception)
                {
                    Logger.LogFileException(Paths.CurrentUserPath);
                    PrintUtils.FileOpenException();
                }
            }
        }

        public static void StoreCurrentUser(string username)
        {
            if (!SearchData.IsFileOpen(Paths.UserPath))
            {
                List<string> list = SearchData.FetchAllDetails(Paths.UserPath);

                for (int i = 0; i < list.Count; i++)
                {
                    string[] str = list[i].ToString().Split(',');

                    if (str[0] == username)
                    {
                        AddCurrentUserDetails(list[i]);
                    }
                }
            }
            else
                PrintUtils.FileOpenException();
        }

        public static void StorePassengerData(string TicketId, List<string> passengerData)
        {
            if (File.Exists(Paths.BookedTickets) && !SearchData.IsFileOpen(Paths.BookedTickets)) 
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(Paths.BookedTickets);
                    using (StreamWriter sw = fileInfo.AppendText())
                    {
                        for (int i = 0; i < passengerData.Count; i++)
                        {
                            string[] str = passengerData[i].ToString().Split(',');
                            sw.WriteLine(String.Join(",", TicketId, str[0], str[1], str[2], str[3], str[4]));
                        }
                    }
                }
                catch (Exception)
                {
                    Logger.LogFileException(Paths.BookedTickets);
                    PrintUtils.FileOpenException();
                }
            }

        }

        public static void AddPassengerDetails(List<string> passengerDetails)
        {
            if (File.Exists(Paths.BookedTickets) && !SearchData.IsFileOpen(Paths.BookedTickets))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(Paths.BookedTickets);
                    using (StreamWriter sw = fileInfo.AppendText())
                    {
                        for (int i = 0; i < passengerDetails.Count; i++)
                        {
                            string[] str = passengerDetails[i].ToString().Split(',');
                            sw.WriteLine(String.Join(",", str[0], str[1], str[2], str[3], str[4], str[5]));
                        }
                    }
                }
                catch (Exception)
                {
                    Logger.LogFileException(Paths.BookedTickets);
                    PrintUtils.FileOpenException();
                }
            }
        }

        public static void AddStationsTime(String TrainId, List<string> stationsTime)
        {
            if (!SearchData.IsFileOpen(Paths.StationTiming))
            {
                FileInfo fileInfo = new FileInfo(Paths.StationTiming);
                using (StreamWriter sw = fileInfo.AppendText())
                {

                    string check = String.Join(",", stationsTime);
                    sw.WriteLine(string.Join(",", TrainId, check));

                }
            }
            else
                PrintUtils.FileOpenException();
        }

        public static void AddStations(string TrainId, List<string> stations)
        {
            if (!SearchData.IsFileOpen(Paths.AllStations))
            {
                FileInfo fileInfo = new FileInfo(Paths.AllStations);
                using (StreamWriter sw = fileInfo.AppendText())
                {
                    string check = String.Join(",", stations);
                    sw.WriteLine(String.Join(",", TrainId, check));

                }
            }
            else
                PrintUtils.FileOpenException();
        }

        public static void AddAllStations(List<string> list)
        {
            if (File.Exists(Paths.AllStations) && !SearchData.IsFileOpen(Paths.AllStations))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(Paths.AllStations);
                    File.Delete(Paths.AllStations);
                    using (StreamWriter sw = fileInfo.AppendText())
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                            string[] str = list[j].Split(',');
                            sw.WriteLine(String.Join(",", str));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogFileException(Paths.AllStations);
                    throw ex;
                }
            }
            else
                PrintUtils.FileOpenException();
        }
    }
}
