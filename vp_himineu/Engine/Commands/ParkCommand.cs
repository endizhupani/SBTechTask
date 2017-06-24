using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vp_himineu.Abstract;

namespace vp_himineu.VehicleParkEngine.Commands
{
    public class ParkCommand : CommandBase, ICommand
    {
        private IVehicle _vehicle;
        private int _sector;
        private int _spot;
        private DateTime _entryTime;

        public IVehicle Vehicle {
            get {
                return _vehicle;
            }
            set {
                _vehicle = value;
            }
        }

        public int Spot {
            get {
                return _spot;
            }
            set {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The spot must be a positive number");
                }
                _spot = value;
            }
        }

        public int Sector {
            get {
                return _sector;
            }
            set {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The sector number must be a positive number");
                }
            }
        }

        public DateTime EntryTime {
            get {
                return _entryTime;
            }
            set {
                _entryTime = value;
            }
        }

        public ParkCommand(string name, IDictionary<string, string> parameters) : base(name, parameters)
        {
            string entryTimeStr;
            string spotStr;
            string sectorStr;
            string licensePlate;
            string type;
            string owner;
            string hoursStr;

            #region Check if parameters are suplied
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
            if (!parameters.TryGetValue("type", out type))
            {
                throw new ArgumentNullException("You must specify a vehicle type");
            }
            if (!parameters.TryGetValue("licensePlate", out licensePlate))
            {
                throw new ArgumentNullException("You must specify a license plate");
            }
            if (!parameters.TryGetValue("owner", out owner))
            {
                throw new ArgumentNullException("You must specify an owner");
            }
            if (!parameters.TryGetValue("hours", out hoursStr))
            {
                throw new ArgumentNullException("You must specify the reserved hours");
            }
            #endregion

            #region Parse into the correct formats
            DateTime entryTime;
            int sector, spot, reservedHours;
            if (!DateTime.TryParse(parameters["time"], out entryTime))
            {
                throw new FormatException("Invalid date format for parameter 'time'");
            }
            if (!Int32.TryParse(sectorStr, out sector))
            {
                throw new FormatException("'sector' must be an integer");
            }
            if (!Int32.TryParse(spotStr, out spot))
            {
                throw new FormatException("'place' must be an integer");
            }
            #endregion

            EntryTime = entryTime;
            Sector = sector;
            Spot = spot;
            Vehicle = VehicleFactory.GetVehicle(parameters);
            
        }

        public string ExcecuteCommand(IVehiclePark vehiclePark)
        {
            try
            {
                base.ValidateEnvironment(vehiclePark);
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }
            return vehiclePark.InsertVehicle(Vehicle, Sector, Spot);
        }
    }
}
