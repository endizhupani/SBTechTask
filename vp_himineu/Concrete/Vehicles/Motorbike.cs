namespace Vp_himineu.Concrete.Vehicles
{
    /// <summary>
    /// Object definition representing a motorbike
    /// </summary>
    public class Motorbike : VehicleBase
    {
        public Motorbike(string licensePlate, string owner, int reservedHours)
            : base(licensePlate, owner, reservedHours, 1.35M, 3M)
        {
        }
    }
}
