using System;
using System.Text;
using System.Text.RegularExpressions;
using vp_himineu.Abstract;

namespace vp_himineu.Concrete
{
    /// <summary>
    /// The base class for all types of vehicles.
    /// </summary>
    public class VehicleBase : IVehicle
    {
        #region Private fields
        private string _licensePlate;
        private string _owner;
        private decimal _regularRate;
        private decimal _overtimeRate;
        private int _reservedHours;
        #endregion

        #region Public properties
        /// <summary>
        /// The license plate for the vehicle
        /// </summary>
        /// <remarks>
        /// Must match the regex: ^[A-Z]{1}\d{3}[A-Z]{2,}$
        /// </remarks>
        /// <exception cref="ArgumentException">If the value does not conform to the regular expression</exception>
        public string LicensePlate
        {
            get
            {
                return _licensePlate;
            }
            private set
            {
                if (!Regex.IsMatch(value, @"^[A-Z]{1}\d{3}[A-Z]{2,}$"))
                {
                    throw new ArgumentException("The license plate number is invalid.");
                }
                _licensePlate = value;
            }
        }

        /// <summary>
        /// The owner of the vehicle
        /// </summary>
        /// <exception cref="ArgumentException">If the value is null or empty</exception>
        public string Owner
        {
            get
            {
                return _owner;
            }
            private set
            {
                if (value == null && value == "")
                {
                    throw new ArgumentException("The owner is required.");
                }
                _owner = value;
            }
        }

        /// <summary>
        /// The regular rate for the vehicle
        /// </summary>
        /// <remarks>
        /// Must be non-negative
        /// </remarks>
        /// <exception cref="ArgumentException">If the value is less than 0</exception>
        public decimal RegularRate
        {
            get
            {
                return _regularRate;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format("The regular rate must be non-negative."));
                }
                _regularRate = value;
            }
        }

        /// <summary>
        /// The overtime rate for the vehicle
        /// </summary>
        /// <remarks>
        /// Must be non-negative
        /// </remarks>
        /// <exception cref="ArgumentException">If the value is less than 0</exception>
        public decimal OvertimeRate
        {
            get
            {
                return _overtimeRate;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format("The overtime rate must be non-negative."));
                }
                _overtimeRate = value;
            }
        }

        /// <summary>
        /// Number of hours the vehicle reserved spot for
        /// </summary>
        /// <remarks>
        /// Must be non-negative
        /// </remarks>
        /// <exception cref="ArgumentException">If the value is less than 0</exception>
        public int ReservedHours
        {
            get
            {
                return _reservedHours;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("The reserved hours must be positive.");
                }
                _reservedHours = value;
            }
        }
        #endregion

        #region Constructors
        public VehicleBase(string licensePlate, string owner, int reservedHours, decimal regularRate, decimal overtimeRate)
        {
            LicensePlate = licensePlate;
            Owner = owner;
            ReservedHours = reservedHours;
            RegularRate = regularRate;
            OvertimeRate = overtimeRate;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get the string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{GetType().Name}; License plate [{LicensePlate}]; Owned by: {Owner}; Reserved spot for {ReservedHours} hours.";
        #endregion
    }
}
