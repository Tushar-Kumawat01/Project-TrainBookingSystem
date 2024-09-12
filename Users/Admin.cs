using System;
using TrainBookingSystem.Trains;
using TrainBookingSystem.Utility;

namespace TrainBookingSystem.Users
{
    internal class Admin
    {
        private static Account account = new Account();
        private static TrainManagementSystem train = new TrainManagementSystem();
        internal static void AdminMenu()
        {
            Console.Clear();

            PrintUtils.ProjectSubHeader("Admin menu");
            Console.WriteLine("1.Add a train");
            Console.WriteLine("2.Delete a train");
            Console.WriteLine("3.Update train details");
            Console.WriteLine("4.View all trains ");
            Console.WriteLine("5.Block/Un-block a user");
            Console.WriteLine("6.My Account ");
            Console.Write("Enter your choice : ");
            string option = Console.ReadLine().Trim();

            if (!PrintUtils.NullCheck(option)) AdminMenu();

            if (!int.TryParse(option, out int id))
            {
                PrintUtils.InputFailure("Enter a valid integer input");
                AdminMenu();
            }

            switch (id)
            {
                case 1:
                    train.AddTrain();
                    AdminMenu();
                    break;
                case 2:
                    train.DeleteTrain();
                    AdminMenu();
                    break;
                case 3:
                    train.UpdateTrain();
                    AdminMenu();
                    break;
                case 4:
                    train.ViewAllTrains();
                    AdminMenu();
                    break;
                case 5:
                    train.BlockUser();
                    AdminMenu();
                    break;
                case 6:
                    account.AccountMenu();
                    AdminMenu();
                    break;
                default:
                    PrintUtils.InputFailure("Invalid choice");
                    AdminMenu();
                    break;
            }
            PrintUtils.InputSuccess("Go Back to admin menu");
            AdminMenu() ;
        }
    }
}
