namespace Vp_himineu.Concrete.VehicleParking
{
    using System.Collections.Generic;

    public class Database
    {
        public Database(int sectors)
        {
            this.SpotsTakenPerSector = new int[sectors];
            this.VehiclesInPark = new Dictionary<string, ParkedVehicle>();
            this.OwnerVehicles = new Dictionary<string, List<ParkedVehicle>>();
        }

        public IDictionary<string, ParkedVehicle> VehiclesInPark { get; set; }

        public IDictionary<string, List<ParkedVehicle>> OwnerVehicles { get; set; }

        public int[] SpotsTakenPerSector { get; set; }

        #region Old messy implementation
        ////public Data(int numberOfSectors)
        ////{
        ////    CarsInParking = new Dictionary<IVehicle, string>();
        ////    park = new Dictionary<string, IVehicle>();
        ////    números = new Dictionary<string, IVehicle>();
        ////    d = new Dictionary<IVehicle, DateTime>();
        ////    ow = new MultiDictionary<string, IVehicle>(false);
        ////    count = new int[numberOfSectors];
        ////}
        ////public Dictionary<IVehicle, string> CarsInParking { get; set; }
        ////public Dictionary<string, IVehicle> park { get; set; }
        ////public Dictionary<string, IVehicle> números { get; set; }
        ////public Dictionary<IVehicle, DateTime> d { get; set; }
        ////public MultiDictionary<string, IVehicle> ow { get; set; }
        ////public int[] count { get; set; }
        #endregion
    }
}
