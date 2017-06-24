namespace Vp_himineu.Concrete.Vehicles
{ 
    /// <summary>
    /// Object definition representing a Car
    /// </summary>
    public class Car : VehicleBase
    {
        public Car(string licensePlate, string owner, int reservedHours)
            : base(licensePlate, owner, reservedHours, 2M, 3.5M)
        {
        }
    }
}
