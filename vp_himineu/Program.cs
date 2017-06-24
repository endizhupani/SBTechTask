using System.Globalization;
using System.Threading;
using vp_himineu.Abstract;
using vp_himineu.Concrete;
using vp_himineu.VehicleParkEngine;

namespace VehicleParkSystem
{
    static class Program
    {
        static IMechanism parkingMechanism; 
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            parkingMechanism = new ParkingMechanism(new Engine());
            parkingMechanism.Run();
        }
    }
}