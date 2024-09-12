
namespace TrainBookingSystem.Trains
{
    internal class Train
    {
        public string TrainId { get; set; }
        public string TrainName { get; set; }
        public string OnBoarding { get; set; }
        public string Destination { get; set; }
        public string NoOfSeats { get; set; }

        public Train() { }
        public Train(string id,string name,string source,string destination,string seats)
        {
            TrainId = id;
            TrainName = name;
            OnBoarding = source;
            Destination = destination;
            NoOfSeats = seats;
        }
    }
}
