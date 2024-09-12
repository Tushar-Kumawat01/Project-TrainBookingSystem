using TrainBookingSystem.Auth;

namespace TrainBookingSystem
{
    internal class Program
    {
        static void Main()
        {
            Authentication authentication = new Authentication();
            authentication.AuthMenu();
        }
    }
}
