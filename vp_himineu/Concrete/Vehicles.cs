namespace vp_himineu.Concrete
{

    public enum VehicleType{
        Car = 1,
        Motorbike = 2,
        Truck = 3
    }

    /// <summary>
    /// Object definition representing a Car
    /// </summary>
    public class Car : VehicleBase
    {
        #region No interesting
        //private string licenseplate; private string person; private decimal regularrate; private decimal morerate; private int hh;
        //public string LicensePlate { get { return licenseplate; } set { if (!Regex.IsMatch(value, @"^[A-Z]{1}\d{3}[A-Z]{2,}$")) { throw new ArgumentException("The license plate number is invalid."); } licenseplate = value; } }
        //public string Owner { get { return person; } set { if (value == null && value == "") { throw new InvalidCastException("The owner is required."); } person = value; } }
        //public decimal RegularRate { get { return regularrate; } set { if (value < 0) { throw new InvalidTimeZoneException(string.Format("The regular rate must be non-negative.")); } regularrate = value; } }
        //public decimal OvertimeRate { get { return morerate; } set { if (value < 0) { throw new IndexOutOfRangeException(string.Format("The overtime rate must be non-negative.")); } morerate = value; } }
        //public int ReservedHours { get { return hh; } set { if (value <= 0) { throw new ArgumentException("The reserved hours must be positive."); } hh = value; } }
        //public override string ToString() { var vehicle = new StringBuilder(); vehicle.AppendFormat("{0} [{2}], owned by {1}", GetType().Name, LicensePlate, Owner); return vehicle.ToString(); }
        #endregion
        public Car(string licensePlate, string owner, int reservedHours)
            : base(licensePlate, owner, reservedHours, 2M, 3.5M) { }
    }

    /// <summary>
    /// Object definition representing a truck
    /// </summary>
    public class Truck : VehicleBase
    {
        #region No interesting
        //private string licenseplate;
        //private string person;
        //private decimal regularrate;
        //private decimal morerate;
        //private int hh;

        //public string LicensePlate {
        //    get {
        //        return licenseplate;
        //    }
        //    set {
        //        if (!Regex.IsMatch(value, @"^[A-Z]{1}\d{3}[A-Z]{2,}$"))
        //        {
        //            throw new ArgumentException("The license plate number is invalid.");
        //        }
        //        licenseplate = value;
        //    }
        //}

        //public string Owner {
        //    get {
        //        return person;
        //    }
        //    set {
        //        if (value == null && value == "")
        //        {
        //            throw new ArgumentException("The owner is required.");
        //        }
        //        person = value;
        //    }
        //}

        //public decimal RegularRate {
        //    get {
        //        return regularrate;
        //    }
        //    set {
        //        if (value < 0)
        //        {
        //            throw new ArgumentException(string.Format("The regular rate must be non-negative."));
        //        }
        //        regularrate = value;
        //    }
        //}

        //public decimal OvertimeRate {
        //    get {
        //        return morerate;
        //    }
        //    set {
        //        if (value < 0)
        //        {
        //            throw new ArgumentException(string.Format("The overtime rate must be non-negative."));
        //        }
        //        morerate = value;
        //    }
        //}
        //public int ReservedHours {
        //    get {
        //        return hh;
        //    }
        //    set {
        //        if (value <= 0)
        //        {
        //            throw new ArgumentException("The reserved hours must be positive.");
        //        }
        //        hh = value;
        //    }
        //}
        //public override string ToString() { var vehicle = new StringBuilder(); vehicle.AppendFormat("{0} [{2}], owned by {1}", GetType().Name, LicensePlate, Owner); return vehicle.ToString(); }
        #endregion
        public Truck(string licensePlate, string owner, int reservedHours)
            : base(licensePlate, owner, reservedHours, 4.57M, 6.2M) { }
    }

    /// <summary>
    /// Object definition representing a motorbike
    /// </summary>
    public class Motorbike : VehicleBase
    {
        public Motorbike(string licensePlate, string owner, int reservedHours)
            : base(licensePlate, owner, reservedHours, 1.35M, 3M) { }
    }
}