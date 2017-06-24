using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using vp_himineu.Abstract;

namespace vp_himineu.VehicleParkEngine.Commands
{
    public class FindVehicleCommand : CommandBase, ICommand
    {
        private string _licensePlate;
        public string LicensePlate {
            get {
                return _licensePlate;
            }
            set {
                if(!Regex.IsMatch(value, @"^[A-Z]{1}\d{3}[A-Z]{2,}$"))
                {
                    throw new ArgumentException("The license plate number is invalid.");
                }
                _licensePlate = value;
            }
        }
        public FindVehicleCommand(string name, IDictionary<string, string> parameters): base (name, parameters)
        {
            string licensePlate;
            if (!parameters.TryGetValue("licensePlate", out licensePlate))
            {
                throw new ArgumentNullException("You must specify a license plate");
            }
            LicensePlate = licensePlate;
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
            return vehiclePark.FindVehicle(LicensePlate);
        }
    }
}
