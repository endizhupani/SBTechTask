namespace VehicleParkSystem
{
    using System.Globalization;
    using System.Threading;
    using Vp_himineu.Abstract;
    using Vp_himineu.Concrete;

    public static class Program
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var parkingMechanism = new ParkingMechanism(new Engine(), new UserInterface());
            parkingMechanism.Run();
        }
    }
}