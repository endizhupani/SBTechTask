using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using vp_himineu.Abstract;

namespace vp_himineu.VehicleParkEngine.Commands
{
    public class ExitCommand : CommandBase, ICommand
    {
        private string _licensePlate;
        public string LicensePlate {
            get {
                return _licensePlate;
            }
            set {
                if (!Regex.IsMatch(value, @"^[A-Z]{1}\d{3}[A-Z]{2,}$"))
                {
                    throw new ArgumentException("The license plate number is invalid.");
                }
                _licensePlate = value;
            }
        }

        private DateTime _exitTime;
        public DateTime ExitTime {
            get {
                return _exitTime;
            }
            set {
                _exitTime = value;
            }
        }

        private decimal _amountPaid;

        public decimal AmountPaid {
            get {
                return _amountPaid;
            }
            set {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format("The amount paid must be non-negative."));
                }
            }
        }

        /// <summary>
        /// Constructor that sets up the parameters of the command
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="parameters">Parameters of the command</param>
        public ExitCommand(string name, IDictionary<string, string> parameters) : base(name, parameters)
        {
            //TODO: set the rest of the values
            DateTime exitTime;
            if (!DateTime.TryParse(parameters["time"], out exitTime))
            {
                throw new ArgumentException("Invalid date format for parameter 'time'");
            }
            ExitTime = exitTime;
            decimal amountPaid = -1;
            if (!Decimal.TryParse(parameters["paid"], out amountPaid))
            {
                throw new ArgumentException("Invalid format for parameter 'paid'");
            }
            AmountPaid = amountPaid;

            LicensePlate = parameters["licensePlate"];
        }

        /// <summary>
        /// Excecutes the command on the specified vehicle park.
        /// </summary>
        /// <param name="vehiclePark"></param>
        /// <returns></returns>
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
            return vehiclePark.ExitVehicle(LicensePlate, AmountPaid, ExitTime);
        }
    }
}
