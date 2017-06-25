namespace Vp_himineu.VehicleParkEngine.Commands
{
    using System;
    using System.Collections.Generic;
    using Abstract;
    using Concrete.Vehicles;

    public class ParkCommand : CommandBase, ICommand
    {
        private IVehicle vehicle;
        private int sector;
        private int spot;
        private DateTime entryTime;

        public ParkCommand(string name, IDictionary<string, string> parameters) : base(name, parameters)
        {
            string entryTimeStr;
            string spotStr;
            string sectorStr;

            // Check if all the required parameters are supplied.
            if (!parameters.TryGetValue("time", out entryTimeStr))
            {
                throw new ArgumentNullException("You must specify an entry time");
            }

            if (!parameters.TryGetValue("place", out spotStr))
            {
                throw new ArgumentNullException("You must specify a place");
            }

            if (!parameters.TryGetValue("sector", out sectorStr))
            {
                throw new ArgumentNullException("You must specify a sector");
            }
           
            // Parse the code into the correct formats
            DateTime entryTime;
            int sector, spot;
            if (!DateTime.TryParse(parameters["time"], out entryTime))
            {
                throw new FormatException("Invalid date format for parameter 'time'");
            }

            if (!int.TryParse(sectorStr, out sector))
            {
                throw new FormatException("'sector' must be an integer");
            }

            if (!int.TryParse(spotStr, out spot))
            {
                throw new FormatException("'place' must be an integer");
            }

            this.EntryTime = entryTime;
            this.Sector = sector;
            this.Spot = spot;
            this.Vehicle = VehicleFactory.GetVehicle(parameters);  
        }

        public IVehicle Vehicle
        {
            get
            {
                return this.vehicle;
            }

            set
            {
                this.vehicle = value;
            }
        }

        public int Spot
        {
            get
            {
                return this.spot;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The spot must be a positive number");
                }

                this.spot = value;
            }
        }

        public int Sector
        {
            get
            {
                return this.sector;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The sector number must be a positive number");
                }

                this.sector = value;
            }
        }

        public DateTime EntryTime
        {
            get
            {
                return this.entryTime;
            }

            set
            {
                this.entryTime = value;
            }
        }

        public string ExcecuteCommand(ref IVehiclePark vehiclePark)
        {
            try
            {
                this.ValidateEnvironment(vehiclePark);
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }

            if (this.Vehicle.GetType().Equals(typeof(Car)))
            {
                // Vehicle is a car
                return vehiclePark.InsertCar(this.Vehicle as Car, this.Sector, this.Spot, this.EntryTime);
            }
            else if (this.Vehicle.GetType().Equals(typeof(Motorbike)))
            {
                // Vehicle is a motorbike
                return vehiclePark.InsertMotorbike(this.Vehicle as Motorbike, this.Sector, this.Spot, this.EntryTime);
            }
            else
            {
                // Vehicle is a truck
                return vehiclePark.InsertTruck(this.Vehicle as Truck, this.Sector, this.Spot, this.EntryTime);
            }
        }
    }
}
