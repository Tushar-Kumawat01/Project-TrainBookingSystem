using System;
using System.Collections.Generic;
using TrainBookingSystem.Constants;
using TrainBookingSystem.DataLog;
using TrainBookingSystem.DataUtility;
using TrainBookingSystem.Utility;
using TrainBookingSystem.Users;

namespace TrainBookingSystem.Trains
{
    internal class TrainManagementSystem
    {
        private Train train = new Train();
        private User user = new User();
        internal void AddTrain()
        {
            Console.Clear();

            PrintUtils.ProjectSubHeader("Add train details");

            Console.Write("Enter train id : ");
            string TrainId = Console.ReadLine().Trim();
            if (!PrintUtils.NullCheck(TrainId)) AddTrain();
            if (int.TryParse(TrainId, out int id))
            {
                TrainId = id.ToString();
            }
            string str = SearchData.SearchTrainDetails(id);
            if (!string.IsNullOrWhiteSpace(str))
            {
                PrintUtils.InputFailure("This train id is already taken");
                AddTrain();
            }

            Console.Write("Enter train name : ");
            train.TrainName = Console.ReadLine().Trim();
            if (!PrintUtils.NullCheck(train.TrainName)) AddTrain();

            Console.Write("Enter onboarding station : ");
            train.OnBoarding = Console.ReadLine().Trim();
            if (!PrintUtils.NullCheck(train.OnBoarding)) AddTrain();

            Console.Write("Enter Arrival Time [07:20]: ");
            string time = Console.ReadLine();

            if (!TimeSpan.TryParseExact(time, "hh\\:mm", null, out TimeSpan arrivalTime))
            {
                PrintUtils.InputFailure("Invalid Time Format");
                AddTrain();
            }
            List<string> Stations = new List<string>();
            Stations.Add(train.OnBoarding);
            List<string> Stationtime = new List<string>();
            Stationtime.Add(time);
            while (true)
            {
                Console.WriteLine("Add Station : ");
                Console.Write("Do you want to add another station (yes / no):");
                string option = Console.ReadLine().Trim().ToLower();

                if (!PrintUtils.NullCheck(option)) AddTrain();
                if (option == "no")
                {
                    break;
                }
                Console.Write("Enter Station : ");
                string station1 = Console.ReadLine().Trim();
                if (!PrintUtils.NullCheck(station1)) AddTrain();

                Console.Write("Enter Arrival Time [07:20]: ");
                time = Console.ReadLine();

                if (!TimeSpan.TryParseExact(time, "hh\\:mm", null, out TimeSpan value))
                {
                    PrintUtils.InputFailure("Invalid Time Format");
                    AddTrain();
                }
                Stations.Add(station1);
                Stationtime.Add(time);

            }
            Console.WriteLine("Enter destination station : ");
            train.Destination = Console.ReadLine().Trim();
            if (!PrintUtils.NullCheck(train.Destination)) AddTrain();

            time = Console.ReadLine();

            if (!TimeSpan.TryParseExact(time, "hh\\:mm", null, out arrivalTime))
            {
                PrintUtils.InputFailure("Invalid Time Format");
                AddTrain();
            }
            Console.WriteLine("Enter number of seats : ");
            train.NoOfSeats = Console.ReadLine().Trim();
            Stations.Add(train.Destination);
            Stationtime.Add(time);
            if (!PrintUtils.NullCheck(train.NoOfSeats)) AddTrain();
            if (int.TryParse(train.NoOfSeats, out int Seatvalue))
            {
                train.NoOfSeats = Seatvalue.ToString();
            }

            WriteData.AddTrainDetails(new Train(TrainId, train.TrainName, train.OnBoarding, train.Destination, train.NoOfSeats));
            WriteData.AddStations(TrainId, Stations);
            WriteData.AddStationsTime(TrainId, Stationtime);
            Logger.LogAddTrain((TrainId).ToString(), train.TrainName);
            PrintUtils.InputSuccess("Go back to admin menu");
            return;
        }


        internal void DeleteTrain()
        {
            Console.Clear();
            PrintUtils.ProjectSubHeader("Deleting a train");
            Console.WriteLine("Enter train number");
            string TrainId = Console.ReadLine().Trim();
            if (!PrintUtils.NullCheck(TrainId)) DeleteTrain();
            if (int.TryParse(TrainId, out int id))
            {
                string str = SearchData.SearchTrainDetails(id);
                if (string.IsNullOrWhiteSpace(str))
                {
                    PrintUtils.InputFailure("Record Of this trainid is not present");
                    return;
                }
                else
                {
                    DeleteData.DeleteTrainDetails(id);
                }
            }
            else
            {
                PrintUtils.InputFailure("Not a valid trainid");
            }
            PrintUtils.InputSuccess("Go back to admin menu");
            return;
        }


        internal void UpdateTrain()
        {
            Console.Clear();
            PrintUtils.ProjectSubHeader("Update train details");

            Console.WriteLine("Enter train id: ");
            string TrainId = Console.ReadLine().Trim();
            if (!PrintUtils.NullCheck(TrainId)) UpdateTrain();
            if (int.TryParse(TrainId, out int id))
            {
                TrainId = id.ToString();
            }
            else
            {
                PrintUtils.InputFailure("Not a valid integer input");
                return;
            }
            string ValidTrainId = SearchData.SearchTrainDetails(id);
            if (!PrintUtils.NullCheck(ValidTrainId)) UpdateTrain();
            Console.WriteLine();
            Console.WriteLine("1. Update train name");
            Console.WriteLine("2. Update onboarding station");
            Console.WriteLine("3. Update destination station");
            Console.WriteLine("4. Update number of seats");
            Console.WriteLine("5. Back to main menu");

            string option = Console.ReadLine().Trim();

            if (!PrintUtils.NullCheck(option)) UpdateTrain();
            if (!int.TryParse(option, out int value))
            {
                PrintUtils.InputFailure("Enter a valid integer input");
                UpdateTrain();
            }

            switch (value)
            {
                case 1:
                    UpdateTrainName(int.Parse(TrainId));
                    break;
                case 2:
                    UpdateOnBoarding(int.Parse(TrainId));
                    break;
                case 3:
                    UpdateDestination(int.Parse(TrainId));
                    break;
                case 4:
                    UpdateSeats(int.Parse(TrainId));
                    break;
                case 5:
                    Console.WriteLine("Back to main menu");
                    Console.ReadKey();
                    return;

                default:
                    PrintUtils.InputFailure("Invalid choice");
                    break;
            }
            PrintUtils.InputSuccess("Go back to admin menu");
            return;
        }


        internal void UpdateTrainName(int TrainId)
        {
            PrintUtils.ProjectSubHeader("Update train name");
            Console.WriteLine("Enter train name :");
            string TrainName = Console.ReadLine();

            if (!PrintUtils.NullCheck(TrainName)) UpdateTrain();

            else
            {
                UpdateData.AddUpdatedDetails(TrainId, TrainName, 1);
                PrintUtils.InputSuccess("Train name updated");
                return;
            }
            PrintUtils.InputSuccess("Go back to admin menu");
            return;
        }


        internal void UpdateOnBoarding(int TrainId)
        {
            PrintUtils.ProjectSubHeader("Update onboarding station");
            Console.WriteLine("Enter onboarding station:");
            string OnBoarding = Console.ReadLine();

            if (!PrintUtils.NullCheck(OnBoarding)) UpdateTrain();
            else
            {
                UpdateData.AddUpdatedDetails(TrainId, OnBoarding, 2);
                UpdateData.UpdateSource(TrainId.ToString(), OnBoarding);
                PrintUtils.InputSuccess("onboarding station updated");
                return;
            }
            PrintUtils.InputSuccess("Go back to admin menu");
            return;
        }

        internal void UpdateDestination(int TrainId)
        {
            PrintUtils.ProjectSubHeader("Update  destination station");
            Console.WriteLine("Enter destination station :");
            string Destination = Console.ReadLine();

            if (!PrintUtils.NullCheck(Destination)) UpdateTrain();
            else
            {
                UpdateData.AddUpdatedDetails(TrainId, Destination, 3);
                UpdateData.UpdateDestination(TrainId.ToString(), Destination);
                PrintUtils.InputSuccess("Destination station updated");
                return;
            }
            PrintUtils.InputSuccess("Go back to admin menu");
            return;
        }

        internal void UpdateDate(int TrainId)
        {
            Console.WriteLine("Update date");
            Console.WriteLine("Enter date [month/day/year]:");
            string Date = Console.ReadLine();

            if (!PrintUtils.NullCheck(Date)) UpdateTrain();
            else if (DateTime.TryParse(Date, out DateTime value))
            {
                Date = value.ToShortDateString();
                UpdateData.AddUpdatedDetails(TrainId, Date, 4);
                PrintUtils.InputSuccess("Date updated");
                return;
            }
            else
            {
                PrintUtils.InputFailure("Enter valid date format");
            }
            PrintUtils.InputSuccess("Go back to admin menu");
            return;
        }

        internal void UpdateSeats(int TrainId)
        {
            Console.WriteLine("Update available no. of seats");
            Console.WriteLine("Enter no. of seats :");
            string NoOfSeats = Console.ReadLine();

            if (!PrintUtils.NullCheck(NoOfSeats)) UpdateTrain();
            if (int.TryParse(NoOfSeats, out int value))
            {
                if (value > 0)
                {
                    UpdateData.AddUpdatedDetails(TrainId, value.ToString(), 5);
                    PrintUtils.InputSuccess("No. of seats updated");
                    return;
                }
            }
            else
            {
                PrintUtils.InputFailure("Enter valid input");
            }
            PrintUtils.InputSuccess("Go back to admin menu");
            return;
        }

        internal void BlockUser()
        {
            Console.Clear();
            PrintUtils.ProjectSubHeader("Block a user");
            Console.WriteLine("Enter username :");
            user.UserName = Console.ReadLine();

            if (!PrintUtils.NullCheck(user.UserName)) BlockUser();
            string str = SearchData.SearchUserDetails(user.UserName);
            string username = WriteData.GetCurrentUserName();
            if (string.IsNullOrEmpty(str))
            {
                PrintUtils.InputFailure("Records for this username not found");
                Console.ReadKey();
                return;
            }
            if (username == user.UserName)
            {
                PrintUtils.InputFailure("Admin's can't be blocked");
                return;
            }
            Console.WriteLine("1. Block the user");
            Console.WriteLine("2. UnBlock the user");
            string option = Console.ReadLine().Trim();

            if (!PrintUtils.NullCheck(option)) BlockUser();

            if (int.TryParse(option, out int id))
            {
                switch (id)
                {
                    case 1:
                        UpdateData.BlockUser(user.UserName, "Block");
                        PrintUtils.InputFailure("User has been successfully blocked");
                        break;
                    case 2:
                        UpdateData.BlockUser(user.UserName, "Normal");
                        PrintUtils.InputSuccess("User has been successfully Un-blocked");
                        break;
                    default:
                        PrintUtils.InputFailure("Invalid choice");
                        break;
                }
            }
            else
            {
                PrintUtils.InputFailure("Not a valid option");
            }
            PrintUtils.InputSuccess("Go back to admin menu");
            return;
        }
        internal void ViewAllTrains()
        {
            Console.Clear();
            List<string> str = SearchData.FetchAllDetails(Paths.TrainPath);
            PrintUtils.ProjectSubHeader("Available trains : ");
            Console.WriteLine();
            for (int i = 0; i < str.Count; i++)
            {
                string[] newStr = str[i].Split(',');
                PrintUtils.PrintTrains(newStr);
                Console.WriteLine();
            }
            PrintUtils.InputSuccess("Go back to main menu");
            Console.ReadKey();
            Console.Clear();
            return;
        }


        protected void SearchTrain()
        {
            Console.Clear();
            PrintUtils.ProjectSubHeader("Search train");
            Console.WriteLine("Enter train id:");
            string TrainId = Console.ReadLine();
            if (!PrintUtils.NullCheck(TrainId)) SearchTrain();
            if (!int.TryParse(TrainId, out int index))
            {
                PrintUtils.InputFailure("Enter a valid integer input");
                Console.ReadKey();
                SearchTrain();
            }
            string str = SearchData.SearchTrainDetails(index);
            if (!String.IsNullOrWhiteSpace(str))
            {
                string[] newStr = str.Split(',');
                PrintUtils.PrintTrains(newStr);
            }
            else
            {
                PrintUtils.InputFailure("Invalid train id");
            }
            PrintUtils.InputSuccess("Go back to main menu");
            Console.ReadKey();
            return;
        }

    }
}
