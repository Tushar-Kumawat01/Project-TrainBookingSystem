using System;
using System.IO;
using System.Data;
using TrainBookingSystem.Constants;
namespace TrainBookingSystem.DataLog
{
    internal class Logger
    {
        private static FileInfo fileInfo = new FileInfo(Paths.Logger);

        public static void LogAuthException(string Activity, string message)
        {
            using (StreamWriter sw = fileInfo.AppendText())
            {
                sw.WriteLine(String.Join(",", Activity, DateTime.Now, message));
            }
        }
        public static void LogFileException(string path)
        {
            using (StreamWriter sw = fileInfo.AppendText())
            {
                sw.WriteLine(String.Join(",", "File Exception", DateTime.Now, "Unable To Fetch required Data From File" + path));
            }
        }

        public static void LogTicketBooking(string UserName, string Seats, string TrainName, string TicketId)
        {
            using (StreamWriter sw = fileInfo.AppendText())
            {
                sw.WriteLine(String.Join(",", "Ticket Booked :", UserName, "Booked ", Seats, "Seats in Train Name:", TrainName, "with Ticket Id", TicketId));
            }
        }

        public static void LogTicketCancellation(string UserName, string TrainName, string TicketId)
        {
            using (StreamWriter sw = fileInfo.AppendText())
            {
                sw.WriteLine(String.Join(",", "Ticket Cancelled :", UserName, "Deleted Ticket with TicketId :", TicketId, "In Train Name :", TrainName));
            }
        }

        public static void LogDeleteAccount(string username)
        {
            using (StreamWriter sw = fileInfo.AppendText())
            {
                sw.WriteLine(String.Join(",", "Account", username, " has been deleted "));
            }
        }

        public static void LogAddTrain(string TrainId, string TrainName)
        {
            using (StreamWriter sw = fileInfo.AppendText())
            {
                sw.WriteLine(String.Join(",", "A new Train", TrainId, " : ", TrainName, "added"));
            }
        }

        public static void LogCancelTrain(string TrainId, string TrainName)
        {
            using (StreamWriter sw = fileInfo.AppendText())
            {
                sw.WriteLine(String.Join(",", " Train", TrainId, " : ", TrainName, "has been cancelled"));
            }
        }
    }
}
