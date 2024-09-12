using System.Collections.Generic;
using System.Linq;
using System;
using TrainBookingSystem.Constants;
using TrainBookingSystem.DataLog;
using TrainBookingSystem.DataUtility;
using TrainBookingSystem.Users;
using TrainBookingSystem.Utility;

namespace TrainBookingSystem.Tickets
{
    internal class FirstClassTicket : Ticket
    {
        public const string CoachType = "1AC";

        internal override string TicketPreference()
        {
            return CoachType;
        }

        internal void BookTickets()
        {
            Console.Clear();
            PrintUtils.ProjectSubHeader("Book Ticket");
            Console.WriteLine();
            Console.Write("Enter Source :");
            string Source = Console.ReadLine();

            if (!PrintUtils.NullCheck(Source)) Passenger.PassengerMenu();
            Console.WriteLine();
            Console.Write("Enter Destination :");
            string Destination = Console.ReadLine();

            List<string> Show = new List<string>();
            if (String.IsNullOrWhiteSpace(Destination))
            {
                Show = SearchData.SearchTrainsBySource(Source);
            }
            else
            {
                Show = SearchData.SearchTrainsDestination(Source, Destination);
            }
            if (Show.Count < 1)
            {
                PrintUtils.InputFailure("No Trains Available from or To This station");
                Console.ReadKey();
                BookTickets();
            }
            List<string> Trains = new List<string>();
            for (int i = 0; i < Show.Count; i++)
            {
                var Split = Show[i].Split(',');
                var TrainNumber = Split[0];
                string str = SearchData.SearchTrainDetails(int.Parse(TrainNumber), Paths.TrainPath);
                Trains.Add(str);
            }
            if (Trains.Count > 0)
            {
                for (int i = 0; i < Trains.Count; i++)
                {
                    Console.WriteLine();
                    if (!String.IsNullOrWhiteSpace(Trains[i]))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(i + 1);
                        PrintUtils.PrintCheckTrains(Trains[i].Split(','));
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    Console.WriteLine();
                }
                string choice = "";
                Console.Write("Enter your choice : ");
                choice = Console.ReadLine();
                if (!PrintUtils.IntInputValiadation(choice))
                {
                    Passenger.PassengerMenu();
                }
                int value = int.Parse(choice);
                if (Trains.Count < value)
                {
                    PrintUtils.InputFailure("Invalid Choice");
                    Console.ReadKey();
                    Passenger.PassengerMenu();
                }
                string[] str1 = Trains[value - 1].Split(',');
                string AllTrains = SearchData.SearchTrainDetails(int.Parse(str1[0]), Paths.TrainPath);
                string[] TrainInfo = AllTrains.Split(',');
                string AllStations = SearchData.SearchTrainDetails(int.Parse(str1[0]), Paths.AllStations);
                string[] StationInfo = AllStations.Split(',');
                string StationTiming = SearchData.SearchTrainDetails(int.Parse(str1[0]), Paths.StationTiming);
                string[] Timing = StationTiming.Split(',');
                int SourceIndex = StationInfo.ToList().IndexOf(Source);
                //int DestinationIndex = StationInfo.ToList().IndexOf(Destination);
                int DestinationIndex = -1;
                if (!String.IsNullOrWhiteSpace(Destination)) DestinationIndex = StationInfo.ToList().IndexOf(Destination);
                else
                {
                    for (int i = 0; i < StationInfo.Length - 1; i++)
                    {
                        if (!String.IsNullOrWhiteSpace(StationInfo[i]) && String.IsNullOrWhiteSpace(StationInfo[i + 1]))
                        {
                            DestinationIndex = i;
                            break;
                        }
                    }
                }
                // Console.WriteLine(DestinationIndex);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(TrainInfo[1].ToUpper() + "( " + TrainInfo[0] + " )");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(Timing[SourceIndex] + "|");
                Console.Write(StationInfo[SourceIndex] + " | " + DateTime.Today.ToShortDateString());
                Console.Write("\t\t-- --\t\t");
                if (DestinationIndex > 0 && !String.IsNullOrWhiteSpace(StationInfo[DestinationIndex])) Console.Write(Timing[DestinationIndex] + "|" + StationInfo[DestinationIndex]);
                else Console.Write(Timing[StationInfo.Length - 1] + "|" + StationInfo[StationInfo.Length - 1]);
                Console.WriteLine(" | " + DateTime.Today.ToShortDateString());
                Console.WriteLine("Available Seats : " + TrainInfo[5]);

                Console.Write("Enter Number of seats : ");
                string NoOfSeats = Console.ReadLine().Trim();
                if (!PrintUtils.IntInputValiadation(NoOfSeats))
                {
                    Passenger.PassengerMenu();
                }
                int Seatvalue = int.Parse(NoOfSeats);

                if ((int.Parse(TrainInfo[5]) - Seatvalue) < 0)
                {
                    PrintUtils.InputFailure("Seats are not available");
                    return;
                }


                string RemainingSeats = (int.Parse(TrainInfo[5]) - Seatvalue).ToString();
                int PassengerSeats = int.Parse(TrainInfo[5]);
                int k = Seatvalue;
                List<String> PassengerDetails = new List<String>();
                while (k > 0)
                {
                    Console.WriteLine("Enter Name : ");
                    String PassengerName = Console.ReadLine().Trim();
                    if (!PrintUtils.NullCheck(PassengerName)) BookTickets();

                    Console.WriteLine("Enter Age : ");
                    String PassengerAge = Console.ReadLine().Trim();
                    if (!PrintUtils.NullCheck(PassengerAge)) BookTickets();
                    if (!int.TryParse(PassengerAge, out int FunAge))
                    {
                        PrintUtils.InputFailure("Invalid Input");
                        Passenger.PassengerMenu();
                    }

                    Console.WriteLine("Enter Gender [M/F]:");
                    String PassengerGender = Console.ReadLine().Trim();
                    if (!PrintUtils.NullCheck(PassengerGender)) BookTickets();
                    if (PassengerGender == "M" || PassengerGender == "F")
                    {
                        PassengerDetails.Add(String.Join(",", PassengerSeats, PassengerName, PassengerAge, PassengerGender, TicketPreference()));
                        PassengerSeats--;
                        k--;
                    }
                    else
                    {
                        PrintUtils.InputFailure("Gender Format [ M / F]");
                        Passenger.PassengerMenu();
                    }
                }
                List<string> UserDetails = SearchData.FetchAllDetails(Paths.CurrentUserPath);
                string[] SeparateUserDetail = UserDetails[0].ToString().Split(',');
                string TicketId = String.Concat(SeparateUserDetail[0][0], TrainInfo[1][0], GetTicketId());
                PrintUtils.InputSuccess("Your ticket has been booked with ticket Id :" + TicketId);
                string CoachType = TicketPreference();
              
                UpdateData.updateRemainingSeats(TrainInfo[0], RemainingSeats);
                
                WriteData.StorePassengerData(TicketId, PassengerDetails);
                WriteData.AddTicketDetails(TicketId, TrainInfo[0], SeparateUserDetail[0], TrainInfo[1], Source, TrainInfo[3], TrainInfo[4], Seatvalue.ToString());
                Logger.LogTicketBooking(SeparateUserDetail[0], Seatvalue.ToString(), TrainInfo[1], TicketId);
                PrintUtils.InputSuccess("Go back to main menu"); ;
                Console.ReadKey();
                return;
            }
            else
            {
                PrintUtils.InputFailure("No Trains Available from or To This station");
                Console.ReadKey();
                return;
            }
        }

    }
}
