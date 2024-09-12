using System;
using System.Collections.Generic;
using TrainBookingSystem.Constants;
using TrainBookingSystem.Utility;

namespace TrainBookingSystem.DataUtility
{
    internal class UpdateData
    {

        //Update Train Name
        public static void AddUpdatedDetails(int TrainId, string DataChange, int index)
        {
            if (!SearchData.IsFileOpen(Paths.TrainPath))
            {
                List<string> list = SearchData.FetchAllDetails(Paths.TrainPath);
                if (!String.IsNullOrWhiteSpace(list.ToString()))
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string[] newStr = list[i].Split(',');

                        if (newStr[0] == TrainId.ToString())
                        {
                            newStr[index] = DataChange;
                            list[i] = String.Join(",", newStr[0], newStr[1], newStr[2], newStr[3], newStr[4], newStr[5]);
                        }
                    }
                    WriteData.AddAllTrainDetails(list);
                }
                else
                {
                    Console.WriteLine("Data not updated");
                }
            }
            else
                PrintUtils.FileOpenException();
        }

        public static void updateRemainingSeats(string TrainId, string seats)
        {
            if (!SearchData.IsFileOpen(Paths.TrainPath))
            {
                List<string> list = SearchData.FetchAllDetails(Paths.TrainPath);
                if (!String.IsNullOrWhiteSpace(list.ToString()))
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string[] newStr = list[i].Split(',');

                        if (newStr[0] == TrainId.ToString())
                        {
                            newStr[5] = seats;
                            list[i] = String.Join(",", newStr[0], newStr[1], newStr[2], newStr[3], newStr[4], newStr[5]);
                        }
                    }
                    WriteData.AddAllTrainDetails(list);
                }
                else
                {
                    Console.WriteLine("Data not updated");
                }
            }
            else
                PrintUtils.FileOpenException();
        }

        //Modify User Details
        public static void UpdateUserPassword(string username, string password)
        {
            if (!SearchData.IsFileOpen(Paths.UserPath))
            {
                List<string> list = SearchData.FetchAllDetails(Paths.UserPath);

                for (int i = 0; i < list.Count; i++)
                {
                    string[] str = list[i].Split(',');
                    if (str[0] == username)
                    {
                        str[1] = password;
                        list[i] = String.Join(",", str[0], str[1], str[2], str[3]);
                        break;
                    }
                }
                WriteData.AddAllUserDetails(list);
                WriteData.StoreCurrentUser(username);
            }
            else
                PrintUtils.FileOpenException();
        }

        public static void BlockUser(string username, string Status)
        {
            if (!SearchData.IsFileOpen(Paths.UserPath))
            {
                List<string> list = SearchData.FetchAllDetails(Paths.UserPath);
                for (int i = 0; i < list.Count; i++)
                {
                    string[] str = list[i].ToString().Split(',');

                    if (str[0] == username)
                    {
                        str[3] = Status;
                        list[i] = String.Join(",", str[0], str[1], str[2], str[3]);
                    }
                }
                WriteData.AddAllUserDetails(list);
            }
            else
                PrintUtils.FileOpenException();
        }

        public static void UpdateSource(string Trainid, string source)
        {
            if (!SearchData.IsFileOpen(Paths.AllStations))
            {
                List<string> list = SearchData.FetchAllDetails(Paths.AllStations);
                for (int i = 0; i < list.Count; i++)
                {
                    string[] str = list[i].ToString().Split(',');

                    if (str[0] == Trainid)
                    {
                        str[1] = source;
                        list[i] = String.Join(",", str);
                    }
                }
                WriteData.AddAllStations(list);
            }
            else
                PrintUtils.FileOpenException();
        }

        public static void UpdateDestination(string Trainid, string destination)
        {
            if (!SearchData.IsFileOpen(Paths.AllStations))
            {
                List<string> list = SearchData.FetchAllDetails(Paths.AllStations);
                for (int i = 0; i < list.Count; i++)
                {
                    string[] str = list[i].ToString().Split(',');

                    if (str[0] == Trainid)
                    {
                        for (int k = 1; k < str.Length; k++)
                        {
                            if (!String.IsNullOrWhiteSpace(str[k - 1]) && String.IsNullOrWhiteSpace(str[k]))
                            {
                                str[k - 1] = destination;
                            }
                            else
                            {
                                str[str.Length - 1] = destination;
                            }
                        }
                        list[i] = String.Join(",", str);
                    }
                }
                WriteData.AddAllStations(list);
            }
            else
                PrintUtils.FileOpenException();
        }
    }
}
