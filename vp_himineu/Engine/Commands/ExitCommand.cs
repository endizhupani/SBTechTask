namespace Vp_himineu.VehicleParkEngine.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Abstract;

    public class ExitCommand : CommandBase, ICommand
    {
        private string licensePlate;
        private DateTime exitTime;
        private decimal amountPaid;

        /// <summary>
        /// Constructor that sets up the parameters of the command
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="parameters">Parameters of the command</param>
        public ExitCommand(string name, IDictionary<string, string> parameters) : base(name, parameters)
        {
            DateTime exitTime;
            if (!DateTime.TryParse(parameters["time"], out exitTime))
            {
                throw new ArgumentException("Invalid date format for parameter 'time'");
            }

            this.ExitTime = exitTime;
            decimal amountPaid = -1;
            if (!decimal.TryParse(parameters["paid"], out amountPaid))
            {
                throw new ArgumentException("Invalid format for parameter 'paid'");
            }

            this.AmountPaid = amountPaid;

            this.LicensePlate = parameters["licensePlate"];
        }

        public string LicensePlate
        {
            get
            {
                return this.licensePlate;
            }

            set
            {
                if (!Regex.IsMatch(value, @"^([A-Z]|[a-z]){1,2}\d{2,}([A-Z]|[a-z]){2}$"))
                {
                    throw new ArgumentException("The license plate number is invalid.");
                }

                this.licensePlate = value;
            }
        }

        public DateTime ExitTime
        {
            get
            {
                return this.exitTime;
            }

            set
            {
                this.exitTime = value;
            }
        }

        public decimal AmountPaid
        {
            get
            {
                return this.amountPaid;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format("The amount paid must be non-negative."));
                }

                this.amountPaid = value;
            }
        }

        /// <summary>
        /// Excecutes the command on the specified vehicle park.
        /// </summary>
        /// <param name="vehiclePark"></param>
        /// <returns></returns>
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

            return vehiclePark.ExitVehicle(this.LicensePlate, this.AmountPaid, this.ExitTime);
        }
    }
}
