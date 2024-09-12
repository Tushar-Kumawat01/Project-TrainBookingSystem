using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainBookingSystem.Constants;
using TrainBookingSystem.DataLog;
using TrainBookingSystem.DataUtility;
using TrainBookingSystem.Users;
using TrainBookingSystem.Utility;

namespace TrainBookingSystem.Tickets
{
    internal class Ticket
    {
        
        internal virtual string TicketPreference()
        {
            return "GEN";
        }
             
            
        protected string GetTicketId()
        {
            Random random = new Random();
            StringBuilder TicketId = new StringBuilder();
            int length = 5;
            const String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            for (int i = 0; i < length; i++)
            {
                char randomChar = chars[random.Next(chars.Length)];
                TicketId.Append(randomChar);
            }

            return TicketId.ToString();
        }

        internal void CancelTicket()
        {
            Console.Clear();
            List<string> Details = SearchData.SearchTicketDetails();
            PrintUtils.ProjectSubHeader("Cancel tickets");
            if (Details.Count > 0)
            {
                for (int i = 0; i < Details.Count; i++)
                {
                    string[] str = Details[i].Split(',');
                    List<string> list = SearchData.SearchBookedUser(str[0]);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("==============================================================================================");
                    PrintUtils.PrintTicket(str[0], str[1], str[3], str[6], str[7], str[4], str[5]);
                    for (int j = 0; j < list.Count; j++)
                    {
                        string[] newStr = list[j].Split(',');
                        PrintUtils.PrintPassengers(newStr);
                        Console.WriteLine();
                        // Console.WriteLine(newStr[1] + " " + newStr[2] + " " + newStr[3] + " " + newStr[4]);
                    }
                    Console.WriteLine("==============================================================================================");
                }
                List<string> str1 = SearchData.FetchAllDetails(Paths.TicketPath);
                Console.WriteLine();
                Console.Write("Enter ticket id : ");
                string TicketId = Console.ReadLine();

                if (!PrintUtils.NullCheck(TicketId)) return;
                bool IsNotEmpty = SearchData.SearchTicketDetails(TicketId);
                if (!IsNotEmpty)
                {
                    PrintUtils.InputFailure("Invalid ticket id");
                    return;
                }


                string[] TicketDetails = SearchData.SearchTicketDetails(TicketId, 1);
                string TrainDetails = SearchData.SearchTrainDetails(int.Parse(TicketDetails[1]));
                string[] check = TrainDetails.Split(',');
                int RemainingSeats = int.Parse(check[5]) + int.Parse(TicketDetails[7]);
                List<string> BookedTicket = SearchData.GetBookedTickets(TicketId);
                string[] checkTicket = BookedTicket[0].Split(',');
                if (checkTicket[checkTicket.Length - 1] == "1AC")
                {
                    DeleteData.DeleteTicketDetails(TicketId);
                    UpdateData.updateRemainingSeats(check[0], RemainingSeats.ToString());
                }
                else
                {
                    DeleteData.DeleteTicketDetails(TicketId);
                }
                PrintUtils.InputSuccess("Deleted successfully");
            }
            else
            {
                PrintUtils.InputSuccess("You haven't booked a ticket yet !!!");
            }

            PrintUtils.InputSuccess("Go back to main menu");
            Console.ReadKey();
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

        
        private void SearchTrain()
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


        internal void BookedTickets()
        {
            Console.Clear();
            List<string> Details = SearchData.SearchTicketDetails();
            PrintUtils.ProjectSubHeader("Booked tickets");
            if (Details.Count > 0)
            {
                for (int i = 0; i < Details.Count; i++)
                {
                    string[] str = Details[i].Split(',');
                    List<string> list = SearchData.SearchBookedUser(str[0]);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("==============================================================================================");
                    PrintUtils.PrintTicket(str[0], str[1], str[3], str[6], str[7], str[4], str[5], str[6]);
                    for (int j = 0; j < list.Count; j++)
                    {
                        string[] newStr = list[j].Split(',');
                        PrintUtils.PrintPassengers(newStr);
                        Console.WriteLine();
                    }
                    Console.WriteLine("==============================================================================================");
                }
            }
            else
            {
                PrintUtils.InputSuccess("You haven't booked a ticket yet !!!");
            }
            PrintUtils.InputSuccess("Go back to main menu");
            Console.ReadKey();
            return;
        }
    }
}
