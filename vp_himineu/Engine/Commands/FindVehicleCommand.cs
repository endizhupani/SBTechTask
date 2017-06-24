namespace Vp_himineu.VehicleParkEngine.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Abstract;

    public class FindVehicleCommand : CommandBase, ICommand
    {
        private string licensePlate;

        public FindVehicleCommand(string name, IDictionary<string, string> parameters)
            : base(name, parameters)
        {
            string licensePlate;
            if (!parameters.TryGetValue("licensePlate", out licensePlate))
            {
                throw new ArgumentNullException("You must specify a license plate");
            }

            this.LicensePlate = licensePlate;
        }

        public string LicensePlate
        {
            get
            {
                return this.licensePlate;
            }

            set
            {
                if (!Regex.IsMatch(value, @"^[A-Z]{1}\d{3}[A-Z]{2,}$"))
                {
                    throw new ArgumentException("The license plate number is invalid.");
                }

                this.licensePlate = value;
            }
        }

        public string ExcecuteCommand(IVehiclePark vehiclePark)
        {
            try
            {
                this.ValidateEnvironment(vehiclePark);
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }

            return vehiclePark.FindVehicle(this.LicensePlate);
        }
    }
}
