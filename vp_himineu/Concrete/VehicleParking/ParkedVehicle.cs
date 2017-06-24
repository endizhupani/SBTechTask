namespace Vp_himineu.Concrete.VehicleParking
{
    using System;
    using System.Text;
    using Abstract;
    
    public struct ParkingSpot
    {
        public int Sector { get; set; }

        public int Spot { get; set; }
    }

    public class ParkedVehicle
    {
        public ParkedVehicle(IVehicle vehicle, ParkingSpot parkingSpot)
        {
            this.Vehicle = vehicle;
            this.ParkingSpot = parkingSpot;
            this.EntryTime = DateTime.Now;
        }

        public IVehicle Vehicle { get; set; }

        public ParkingSpot ParkingSpot { get; set; }

        public DateTime EntryTime { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(200);
            sb.Append(this.Vehicle.GetType().Name);
            sb.Append(" [");
            sb.Append(this.Vehicle.LicensePlate);
            sb.Append("], owned by ");
            sb.Append(this.Vehicle.Owner);
            sb.Append(Environment.NewLine);
            sb.Append("Parked at (");
            sb.Append(this.ParkingSpot.Sector);
            sb.Append(", ");
            sb.Append(this.ParkingSpot.Spot);
            sb.Append(")");
            return sb.ToString();
        }
    }
}
