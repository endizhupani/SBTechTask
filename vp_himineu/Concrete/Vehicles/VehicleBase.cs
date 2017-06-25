namespace Vp_himineu.Concrete.Vehicles
{
    using System;
    using System.Text.RegularExpressions;
    using Abstract;

    public enum VehicleType
    {
        Car = 1,
        Motorbike = 2,
        Truck = 3
    }

    /// <summary>
    /// The base class for all types of vehicles.
    /// </summary>
    public class VehicleBase : IVehicle
    {
        #region Private fields
        private string licensePlate;
        private string owner;
        private decimal regularRate;
        private decimal overtimeRate;
        private int reservedHours;
        #endregion

        #region Constructors
        public VehicleBase(string licensePlate, string owner, int reservedHours, decimal regularRate, decimal overtimeRate)
        {
            this.LicensePlate = licensePlate;
            this.Owner = owner;
            this.ReservedHours = reservedHours;
            this.RegularRate = regularRate;
            this.OvertimeRate = overtimeRate;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// The license plate for the vehicle
        /// </summary>
        /// <remarks>
        /// Must match the regex: ^([A-Z]|[a-z]){1,2}\d{2,}([A-Z]|[a-z]){2}$
        /// </remarks>
        /// <exception cref="ArgumentException">If the value does not conform to the regular expression</exception>
        public string LicensePlate
        {
            get
            {
                return this.licensePlate;
            }

            private set
            {
                if (!Regex.IsMatch(value, @"^([A-Z]|[a-z]){1,2}\d{2,}([A-Z]|[a-z]){2}$"))
                {
                    throw new ArgumentException("The license plate number is invalid.");
                }

                this.licensePlate = value;
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
                return this.owner;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("The owner is required.");
                }

                this.owner = value;
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
                return this.regularRate;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format("The regular rate must be non-negative."));
                }

                this.regularRate = value;
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
                return this.overtimeRate;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format("The overtime rate must be non-negative."));
                }

                this.overtimeRate = value;
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
                return this.reservedHours;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("The reserved hours must be positive.");
                }

                this.reservedHours = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get the string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{this.GetType().Name}; License plate [{this.LicensePlate}]; Owned by: {this.Owner}; Reserved spot for {this.ReservedHours} hours.";
        #endregion
    }
}