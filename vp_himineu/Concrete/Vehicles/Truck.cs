namespace Vp_himineu.Concrete.Vehicles
{
    /// <summary>
    /// Object definition representing a truck
    /// </summary>
    public class Truck : VehicleBase
    {
        public Truck(string licensePlate, string owner, int reservedHours)
            : base(licensePlate, owner, reservedHours, 4.57M, 6.2M)
        {
        }
    }
}
