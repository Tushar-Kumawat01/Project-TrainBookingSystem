using System;
using TrainBookingSystem.Tickets;
using TrainBookingSystem.DataUtility;
using TrainBookingSystem.Utility;
namespace TrainBookingSystem.Users
{
    internal class Passenger 
    {
        private static Account account = new Account(); 
        private static FirstClassTicket firstClassticket =new FirstClassTicket();
        private static GeneralTicket generalTicket =new GeneralTicket();
        public static void PassengerMenu()
        {
            Passenger passenger = new Passenger();
            string CurrentUser = WriteData.GetCurrentUserName();
            Console.Clear();
            PrintUtils.ProjectHeader();

            Console.WriteLine("Hello, {0} ", CurrentUser);
            Console.WriteLine();
            Console.WriteLine("1. Book a ticket");
            Console.WriteLine("2. View all trains");
            Console.WriteLine("3. Cancel ticket");
            Console.WriteLine("4. My tickets");
            Console.WriteLine("5. My account");
            Console.Write("Enter your choice :");
            string option = Console.ReadLine().Trim();

            if (String.IsNullOrWhiteSpace(option)) PassengerMenu();

            if (!int.TryParse(option, out int id))
            {
                PrintUtils.InputFailure("Enter a valid integer input");
                PassengerMenu();
            }
            passenger.GetMenuChoice(id);
        }
        public void GetMenuChoice(int id)
        {
            switch (id)
            {
                case 1:
                    Console.WriteLine();
                    Console.WriteLine("1. Book First Class Ticket :");
                    Console.WriteLine("2. Book General Ticket :");
                    Console.Write("Enter your choice : ");
                    string option = Console.ReadLine().Trim();
                    if (!String.IsNullOrWhiteSpace(option))
                    {
                        if (int.TryParse(option, out int value))
                        {
                            switch(value)
                            {
                                case 1:
                                    firstClassticket.BookTickets();
                                    break;
                                case 2:
                                    generalTicket.BookTickets(); break;
                                default:
                                    PrintUtils.InputFailure("Invalid Choice");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        else
                        {
                            PrintUtils.InputFailure("Invalid Choice");
                            Console.ReadKey();
                            PassengerMenu();
                        }
                    }
                    else
                    {
                        PrintUtils.InputFailure("Invalid choice");
                    }
                    PassengerMenu();
                    break;
                case 2:
                    firstClassticket.ViewAllTrains();
                    PassengerMenu();
                    break;
                case 3:
                    firstClassticket.CancelTicket();
                    PassengerMenu();
                    break;
                case 4:
                    firstClassticket.BookedTickets();
                    PassengerMenu();
                    break;
                case 5:
                    account.AccountMenu();
                    PassengerMenu();
                    break;
                default:
                    PrintUtils.InputFailure("Invalid choice");
                    PassengerMenu();
                    break ;
            }
        }

       
    }
}
