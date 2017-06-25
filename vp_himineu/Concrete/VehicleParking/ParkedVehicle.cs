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
        private IVehicle vehicle;
        private ParkingSpot parkingSpot;
        private DateTime entryTime;

        public ParkedVehicle(IVehicle vehicle, ParkingSpot parkingSpot, DateTime entryTime)
        {
            this.vehicle = vehicle;
            this.parkingSpot = parkingSpot;
            this.entryTime = entryTime;
        }

        public IVehicle Vehicle
        {
            get
            {
                return this.vehicle;
            }
        }

        public DateTime EntryTime
        {
            get
            {
                return this.entryTime;
            }
        }

        public ParkingSpot ParkingSpot
        {
            get
            {
                return this.parkingSpot;
            }
        }

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
