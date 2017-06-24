namespace Vp_himineu.VehicleParkEngine.Commands
{
    using System;
    using System.Collections.Generic;
    using Vp_himineu.Abstract;
    using Vp_himineu.Concrete;

    public class SetupParkCommand : CommandBase, ICommand
    {
        private int sectors;
        private int placesPerSector;
        
        public SetupParkCommand(string name, IDictionary<string, string> parameters) 
            : base(name, parameters)
        {
            string numberOfSectorsStr, placesPerSectorStr;
            if (!parameters.TryGetValue("sectors", out numberOfSectorsStr))
            {
                throw new ArgumentNullException("You must specify the number of sectors");
            }

            if (!parameters.TryGetValue("placesPerSector", out placesPerSectorStr))
            {
                throw new ArgumentNullException("You must specify the number of places per sector");
            }

            int sectors, placesPerSector;
            if (!int.TryParse(numberOfSectorsStr, out sectors))
            {
                throw new FormatException("The number of sectors must be a string");
            }

            if (!int.TryParse(placesPerSectorStr, out placesPerSector))
            {
                throw new FormatException("The number of places per sector must be a string");
            }

            this.PlacesPerSector = placesPerSector;
            this.Sectors = sectors;
        }

        public int Sectors
        {
            get
            {
                return this.sectors;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The number of sectors must be larger than 0");
                }

                this.sectors = value;
            }
        }

        public int PlacesPerSector
        {
            get
            {
                return this.placesPerSector;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The number of places per sector must be larger than 0");
                }

                this.placesPerSector = value;
            }
        }

        public string ExcecuteCommand(IVehiclePark vehiclePark)
        {
            vehiclePark = new VehiclePark(this.Sectors, this.PlacesPerSector);
            return "Vehicle park created!";
        }
    }
}
