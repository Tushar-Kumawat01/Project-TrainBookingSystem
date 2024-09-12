using System;
using System.Text;

namespace TrainBookingSystem.Utility
{
    internal class PrintUtils
    {
        internal static void ProjectHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("====================");
            Console.WriteLine("Train Booking System");
            Console.WriteLine("====================");
            Console.ForegroundColor = ConsoleColor.White;
        }
        internal static void ProjectSubHeader(string SubHeader)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("================================");
            Console.WriteLine(SubHeader);
            Console.WriteLine("================================");
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static void TakeInput(string Write)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Write);
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }

        internal static void InputFailure(string PrintString)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(PrintString);
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }

        internal static void InputSuccess(string PrintString)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(PrintString);
            Console.ReadKey();
        }

        internal static bool NullCheck(string InputData)
        {
            if (String.IsNullOrWhiteSpace(InputData))
            {
                Console.WriteLine();
                PrintUtils.InputFailure("Invalid Input");
                Console.ReadKey();
                return false;
            }
            return true;
        }

        internal static void PrintTicket(params string[] InputData)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Ticket Id : ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(InputData[0]);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Train Id : ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(InputData[1]);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t Train Name :");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(InputData[2]);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t Date :");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(DateTime.Today.ToShortDateString());
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Seats :");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(InputData[4]);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t OnBoarding :");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(InputData[5]);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t Destination :");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(InputData[6]);
            Console.WriteLine();
        }

        internal static void PrintTicket1(params string[] InputData)
        {
            Console.WriteLine();
            Console.WriteLine();
        }
        internal static void PrintTrains(params string[] InputData)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Train Id : ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(InputData[0]);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t Train Name :");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(InputData[1]);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t Date :");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(DateTime.Today.ToShortDateString());
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Seats :");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(InputData[5]);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t\t OnBoarding :");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(InputData[2]);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\t\t\t Destination :");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(InputData[3]);
            Console.WriteLine();
        }

        internal static void PrintCheckTrains(params string[] InputData)
        {
            Console.WriteLine();
            // Console.BackgroundColor = ConsoleColor.White;
            //Console.BufferWidth = Console.WindowWidth;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(InputData[1].ToUpper() + "( " + InputData[0] + " )");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(InputData[2]);
            Console.Write(" | " + DateTime.Today.ToShortDateString());
            Console.Write("\t\t--  --\t\t");
            Console.Write(InputData[3]);
            Console.WriteLine(" | " + DateTime.Today.ToShortDateString());
            Console.WriteLine("Available Seats : " + InputData[5]);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }
        internal static void PrintPassengers(params string[] InputData)
        {
            Console.WriteLine();
            Console.Write("Seat No.: " + InputData[1]);
            Console.Write("\t  Name : " + InputData[2]);
            Console.Write("\t  Age.: " + InputData[3]);
            Console.Write("\t  Gender.: " + InputData[4]);
            Console.Write("\t  Coach : "+InputData[5]);
        }
        internal static string HidePassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key != ConsoleKey.Backspace)
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
                else if (password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            return password.ToString();
        }

        internal static void FileOpenException()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("File already in use , Close the file");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Environment.Exit(0);
        }

        internal static bool IntInputValiadation(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            else if (!int.TryParse(value, out int val) && val <=0)
            {
              return false ;
            }
            return true;
        }
    }
}
