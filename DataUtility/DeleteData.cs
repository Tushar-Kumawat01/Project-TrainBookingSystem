using System;
using System.Collections.Generic;
using TrainBookingSystem.Constants;
using TrainBookingSystem.DataLog;
using TrainBookingSystem.Utility;

namespace TrainBookingSystem.DataUtility
{
    internal class DeleteData
    {
        //Delete Train Details
        public static void DeleteTrainDetails(int TrainId)
        {
            if (!SearchData.IsFileOpen(Paths.TrainPath))
            {
                string str = SearchData.SearchTrainDetails(TrainId);
                if (!String.IsNullOrWhiteSpace(str))
                {
                    List<string> list = SearchData.FetchAllDetails(Paths.TrainPath);

                    for (int i = 0; i < list.Count; i++)
                    {
                        string[] newStr = list[i].Split(',');

                        if (newStr[0] == TrainId.ToString())
                        {
                            list.RemoveAt(i);
                            Logger.LogCancelTrain(newStr[0].ToString(), newStr[1]);
                        }
                    }

                    WriteData.AddAllTrainDetails(list);
                }
                else
                {
                    Console.WriteLine("Invalid train id");
                }
            }
            else PrintUtils.FileOpenException();
        }

        //Delete Ticket Details
        public static void DeleteTicketDetails(string TicketId)
        {
            if (!SearchData.IsFileOpen(Paths.TicketPath))
            {
                List<string> str = SearchData.FetchAllDetails(Paths.TicketPath);
                List<string> CurrentUser = SearchData.FetchAllDetails(Paths.CurrentUserPath);
                string[] username = CurrentUser[0].Split(',');
                for (int i = 0; i < str.Count; i++)
                {
                    string[] newStr = str[i].Split(',');

                    if (newStr[2] == username[0] && newStr[0] == TicketId)
                    {
                        str.RemoveAt(i);
                        Logger.LogTicketCancellation(username[0], newStr[3], newStr[0]);
                    }
                }
                WriteData.AddUpdatedTicketDetails(str);
            }
            else
                PrintUtils.FileOpenException();
        }

        //Delete User Details
        public static void DeleteUserDetails(string username)
        {
            if (!SearchData.IsFileOpen(Paths.UserPath))
            {
                string str = SearchData.SearchUserDetails(username);
                if (!String.IsNullOrWhiteSpace(str))
                {
                    List<string> list = SearchData.FetchAllDetails(Paths.UserPath);

                    for (int i = 0; i < list.Count; i++)
                    {
                        string[] newStr = list[i].Split(',');

                        if (newStr[0] == username)
                        {
                            list.RemoveAt(i);
                        }
                    }
                    WriteData.AddAllUserDetails(list);
                }
                else
                {
                    Console.WriteLine("Invalid username");
                }
            }
            else
                PrintUtils.FileOpenException();
        }


        public static void DeletePassengerDetails(string TicketId)
        {
            //tring str = SearchData.SearchUserDetails(TicketId);
            if (!SearchData.IsFileOpen(Paths.BookedTickets))
            {
                List<string> list = SearchData.FetchAllDetails(Paths.BookedTickets);

                for (int i = 0; i < list.Count; i++)
                {
                    string[] newStr = list[i].Split(',');

                    if (newStr[0] == TicketId)
                    {
                        list.RemoveAt(i);
                    }
                }
                WriteData.AddPassengerDetails(list);
            }
            else
                PrintUtils.FileOpenException();
        }
    }
}
