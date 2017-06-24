using Himineu_system;
using System;
using System.Collections.Generic;
using System.Text;
using vp_himineu.Abstract;
using vp_himineu.Concrete;

namespace vp_himineu.Concrete
{
    class VehiclePark : IVehiclePark
    {
        public Layout Layout { get; set; }
        public VehiclePark(int numberOfSectors, int placesPerSector)
        { 
            Layout = new Layout(numberOfSectors, placesPerSector);
        }

        public string InsertVehicle(IVehicle vehicle, int sector, int place)
        {
            if (sector > Layout.Sectors)
            {
                return $"There is no sector {sector} in the park";
            }
            if (place > Layout.PlacesPerSector)
            {
                return $"There is no place {place} in sector {sector}";
            }
            if (Layout.IsSpotFilled(sector, place))
            {
                return $"The spot ({sector}, {place}) is occupied";
            }
            if (Layout.Database.VehiclesInPark.ContainsKey(vehicle.LicensePlate))
            {
                return $"There is already a vehicle with license plate {vehicle.LicensePlate} in the park";
            }

            ParkedVehicle parkedVehicle = new ParkedVehicle(vehicle, new ParkingSpot
            {
                Sector = sector,
                Spot = place
            });
            Layout.Database.VehiclesInPark.Add(vehicle.LicensePlate, parkedVehicle);

            if (Layout.Database.OwnerVehicles.ContainsKey(vehicle.Owner))
            {
                if (Layout.Database.OwnerVehicles[vehicle.Owner] == null)
                {
                    Layout.Database.OwnerVehicles[vehicle.Owner] = new List<ParkedVehicle>();
                }
            }
            else
            {
                Layout.Database.OwnerVehicles.Add(vehicle.Owner, new List<ParkedVehicle>());
            }
            Layout.Database.OwnerVehicles[vehicle.Owner].Add(parkedVehicle);
            Layout.FillParkingSpot(sector, place);
            return string.Format("{0} parked successfully at place ({1},{2})", vehicle.GetType().Name, sector, place);
            //switch (vehicleType)
            //{
            //    case VehicleType.Car:
            //        message = InsertCar(vehicle as Car, sector, place);
            //        break;
            //    case VehicleType.Motorbike:
            //        message = InsertMotorbike(vehicle as Motorbike, sector, place);
            //        break;
            //    case VehicleType.Truck:
            //        message = InsertTruck(vehicle as Truck, sector, place);
            //        break;
            //    default:
            //        break;
            //};
            //return message;

        }

        //public string InsertCar(Car car, int sector, int place) {

        //    //Add the vehicle to list
        //    ParkedVehicle parkedVehicle = new ParkedVehicle(car, new ParkingSpot
        //    {
        //        Sector = sector,
        //        Spot = place
        //    });
        //    Layout.Database.VehiclesInPark.Add(car.LicensePlate, parkedVehicle);

        //    if (Layout.Database.OwnerVehicles.ContainsKey(car.Owner))
        //    {
        //        if (Layout.Database.OwnerVehicles[car.Owner] == null)
        //        {
        //            Layout.Database.OwnerVehicles[car.Owner] = new List<ParkedVehicle>();
        //        }
        //    }
        //    else {
        //        Layout.Database.OwnerVehicles.Add(car.Owner, new List<ParkedVehicle>());
        //    }
        //    Layout.Database.OwnerVehicles[car.Owner].Add(parkedVehicle);
        //    Layout.FillParkingSpot(sector, place);
        //    return string.Format("{0} parked successfully at place ({1},{2})", car.GetType().Name, sector, place);
        //}

        //public string InsertMotorbike(Motorbike motorbike, int sector, int place)
        //{

        //    //Add the vehicle to list
        //    ParkedVehicle parkedVehicle = new ParkedVehicle(motorbike, new ParkingSpot
        //    {
        //        Sector = sector,
        //        Spot = place
        //    });
        //    Layout.Database.VehiclesInPark.Add(motorbike.LicensePlate, parkedVehicle);

        //    if (Layout.Database.OwnerVehicles.ContainsKey(motorbike.Owner))
        //    {
        //        if (Layout.Database.OwnerVehicles[motorbike.Owner] == null)
        //        {
        //            Layout.Database.OwnerVehicles[motorbike.Owner] = new List<ParkedVehicle>();
        //        }
        //    }
        //    else
        //    {
        //        Layout.Database.OwnerVehicles.Add(motorbike.Owner, new List<ParkedVehicle>());
        //    }
        //    Layout.Database.OwnerVehicles[motorbike.Owner].Add(parkedVehicle);
        //    Layout.FillParkingSpot(sector, place);
        //    return string.Format("{0} parked successfully at place ({1},{2})", motorbike.GetType().Name, sector, place);
        //}

        //public string InsertTruck(Truck truck, int sector, int place)
        //{

        //    //Add the vehicle to list
        //    ParkedVehicle parkedVehicle = new ParkedVehicle(truck, new ParkingSpot
        //    {
        //        Sector = sector,
        //        Spot = place
        //    });
        //    Layout.Database.VehiclesInPark.Add(truck.LicensePlate, parkedVehicle);

        //    if (Layout.Database.OwnerVehicles.ContainsKey(truck.Owner))
        //    {
        //        if (Layout.Database.OwnerVehicles[truck.Owner] == null)
        //        {
        //            Layout.Database.OwnerVehicles[truck.Owner] = new List<ParkedVehicle>();
        //        }
        //    }
        //    else
        //    {
        //        Layout.Database.OwnerVehicles.Add(truck.Owner, new List<ParkedVehicle>());
        //    }
        //    Layout.Database.OwnerVehicles[truck.Owner].Add(parkedVehicle);
        //    Layout.FillParkingSpot(sector, place);
        //    return string.Format("{0} parked successfully at place ({1},{2})", truck.GetType().Name, sector, place);
        //}

        public string ExitVehicle(string licencePlate, decimal amountPaid, DateTime exitTime)
        {
            ParkedVehicle parkedVehicle;
            if (!Layout.Database.VehiclesInPark.TryGetValue(licencePlate, out parkedVehicle))
            {
                return $"There is no vehicle with license plate {licencePlate} in the park";
            }
            Layout.Database.VehiclesInPark.Remove(licencePlate);
            Layout.Database.OwnerVehicles[parkedVehicle.Vehicle.Owner].RemoveAll(pv => pv.Vehicle.LicensePlate == licencePlate);
            Layout.EmptyParkingSpot(parkedVehicle.ParkingSpot.Sector, parkedVehicle.ParkingSpot.Spot);
            return new Ticket(parkedVehicle, exitTime, amountPaid).ToString();
        }

        public string GetStatus()
        {
            return Layout.GetParkingLotStatus();
        }

        public string FindVehicle(string licensePlate) {
            ParkedVehicle parkedVehicle;
            if (!Layout.Database.VehiclesInPark.TryGetValue(licensePlate, out parkedVehicle))
            {
                return $"There is no vehicle with license plate {licensePlate} in the park";
            }
            return parkedVehicle.ToString();
        }

        public string FindVehiclesByOwner(string owner) {
            List<ParkedVehicle> parkedVehicles;
            if (Layout.Database.OwnerVehicles.TryGetValue(owner, out parkedVehicles))
            {
                return $"No vehicles by {owner}";
            }
            StringBuilder sb = new StringBuilder(500);
            foreach (var pVehicle in parkedVehicles)
            {
                sb.Append(pVehicle.ToString());
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        #region Old messy implementation
        //public string InsertCar(VehiclePark3.Car carro, int sector, int place)
        //{
        //    if (sector > Layout.Sectors)
        //    {
        //        return $"There is no sector {sector} in the park";
        //    }
        //    if (place > Layout.PlacesPerSector)
        //    {
        //        return $"There is no place {place} in sector {sector}";
        //    }
        //    if (Data.park.ContainsKey(string.Format("({0},{1})", sector, place)))
        //    {
        //        return string.Format("The place ({0},{1}) is occupied", sector, place);
        //    }
        //    if (Data.números.ContainsKey(carro.LicensePlate))
        //    {
        //        return string.Format("There is already a vehicle with license plate {0} in the park", carro.LicensePlate);
        //    }
        //    Data.CarsInParking[carro] = string.Format("({0},{1})", sector, place); 
        //    Data.park[string.Format("({0},{1})", sector, place)] = carro;
        //    Data.números[carro.LicensePlate] = carro;
        //    Data.d[carro] = time;
        //    Data.ow[carro.Owner].Add(carro);
        //    Data.count[sector - 1]--;
        //    return string.Format("{0} parked successfully at place ({1},{2})", carro.GetType().Name, sector, place);
        //}

        //public string InsertMotorbike(VehiclePark3.Motorbike moto, int s, int p)
        //{
        //    if (s > Layout.Sectors) return string.Format("There is no sector {0} in the park", s);
        //    if (p > Layout.PlacesPerSector) return string.Format("There is no place {0} in sector {1}", p, s);
        //    if (Data.park.ContainsKey(string.Format("({0},{1})", s, p))) return string.Format("The place ({0},{1}) is occupied", s, p);
        //    if (Data.números.ContainsKey(moto.LicensePlate)) return string.Format("There is already a vehicle with license plate {0} in the park", moto.LicensePlate);
        //    Data.CarsInParking[moto] = string.Format("({0},{1})", s, p);
        //    Data.park[string.Format("({0},{1})", s, p)] = moto;
        //    Data.números[moto.LicensePlate] = moto;
        //    Data.d[moto] = t;
        //    Data.ow[moto.Owner].Add(moto);
        //    Data.count[s - 1]++;
        //    return string.Format("{0} parked successfully at place ({1},{2})", moto.GetType().Name, s, p);
        //}

        //public string InsertTruck(VehiclePark3.Truck caminhão, int s, int p)
        //{
        //    if (s > Layout.Sectors) return string.Format("There is no sector {0} in the park", s);
        //    if (p > Layout.PlacesPerSector) return string.Format("There is no place {0} in sector {1}", p, s);
        //    if (Data.park.ContainsKey(string.Format("({0},{1})", s, p))) return string.Format("The place ({0},{1}) is occupied", s, p);
        //    if (Data.números.ContainsKey(caminhão.LicensePlate)) return string.Format("There is already a vehicle with license plate {0} in the park", caminhão.LicensePlate);
        //    Data.CarsInParking[caminhão] = string.Format("({0},{1})", s, p);
        //    Data.park[string.Format("({0},{1})", s, p)] = caminhão;
        //    Data.números[caminhão.LicensePlate] = caminhão;
        //    Data.d[caminhão] = t;
        //    Data.ow[caminhão.Owner].Add(caminhão);
        //    return string.Format("{0} parked successfully at place ({1},{2})", caminhão.GetType().Name, s, p);
        //}

        //public string ExitVehicle(string l_pl, DateTime end, decimal money)
        //{
        //    var vehicle = (Data.números.ContainsKey(l_pl)) ? Data.números[l_pl] : null;
        //    if (vehicle == null)
        //        return string.Format("There is no vehicle with license plate {0} in the park", l_pl);

        //    var start = Data.d[vehicle];
        //    int endd = (int)Math.Round((end - start).TotalHours);
        //    var ticket = new StringBuilder();
        //    ticket.AppendLine(new string('*', 20)).AppendFormat("{0}", vehicle.ToString()).AppendLine().AppendFormat("at place {0}", Data.CarsInParking[vehicle]).AppendLine().AppendFormat("Rate: ${0:F2}", (vehicle.ReservedHours * vehicle.RegularRate)).AppendLine().AppendFormat("Overtime rate: ${0:F2}", (endd > vehicle.ReservedHours ? (endd - vehicle.ReservedHours) * vehicle.OvertimeRate : 0)).AppendLine().AppendLine(new string('-', 20)).AppendFormat("Total: ${0:F2}", (vehicle.ReservedHours * vehicle.RegularRate + (endd > vehicle.ReservedHours ? (endd - vehicle.ReservedHours) * vehicle.OvertimeRate : 0))).AppendLine().AppendFormat("Paid: ${0:F2}", money).AppendLine().AppendFormat("Change: ${0:F2}", money - ((vehicle.ReservedHours * vehicle.RegularRate) + (endd > vehicle.ReservedHours ? (endd - vehicle.ReservedHours) * vehicle.OvertimeRate : 0))).AppendLine().Append(new string('*', 20));
        //    //DELETE
        //    int sector = int.Parse(Data.CarsInParking[vehicle].Split(new[] { "(", ",", ")" }, StringSplitOptions.RemoveEmptyEntries)[0]);
        //    Data.park.Remove(Data.CarsInParking[vehicle]);
        //    Data.CarsInParking.Remove(vehicle);
        //    Data.números.Remove(vehicle.LicensePlate);
        //    Data.d.Remove(vehicle);
        //    Data.ow.Remove(vehicle.Owner, vehicle);
        //    Data.count[sector - 1]--;
        //    //END OF DELETE
        //    return ticket.ToString();
        //}
        //public string GetStatus()
        //{
        //    var places = Data.count.Select((s, i) => string.Format("Sector {0}: {1} / {2} ({2}% full)",
        //                                                            i + 1,
        //                                                            s,
        //                                                            Layout.PlacesPerSector,
        //                                                            Math.Round((double)s / Layout.PlacesPerSector * 100)));

        //    return string.Join(Environment.NewLine, places);
        //}

        //public string FindVehicle(string l_pl)
        //{
        //    var vehicle = (Data.números.ContainsKey(l_pl)) ? Data.números[l_pl] : null;
        //    if (vehicle == null)
        //        return string.Format("There is no vehicle with license plate {0} in the park", l_pl);
        //    else return Input(new[] { vehicle });
        //}

        //public string FindVehiclesByOwner(string owner)
        //{
        //    if (!Data.park.Values.Where(v => v.Owner == owner).Any())
        //        return string.Format("No vehicles by {0}", owner);
        //    else
        //    {
        //        var found = Data.park.Values.ToList();
        //        var res = found;
        //        foreach (var f in found)
        //        {
        //            res = res.Where(v => v.Owner == owner).ToList();
        //        }
        //        return string.Join(Environment.NewLine, Input(res));
        //    }
        //}

        //private string Input(IEnumerable<IVehicle> carros)
        //{
        //    return string.Join(Environment.NewLine, carros.Select(vehicle => string.Format("{0}{1}Parked at {2}", vehicle.ToString(), Environment.NewLine, Data.CarsInParking[vehicle])));
        //}
        #endregion
    }

    public class ParkedVehicle {
        
        public IVehicle Vehicle { get; set; }
        public ParkingSpot ParkingSpot { get; set; }
        public DateTime EntryTime { get; set; }

        public ParkedVehicle(IVehicle vehicle, ParkingSpot parkingSpot)
        {
            Vehicle = vehicle;
            ParkingSpot = parkingSpot;
            EntryTime = DateTime.Now;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(200);
            sb.Append(Vehicle.GetType().Name);
            sb.Append(" [");
            sb.Append(Vehicle.LicensePlate);
            sb.Append("], owned by ");
            sb.Append(Vehicle.Owner);
            sb.Append(Environment.NewLine);
            sb.Append("Parked at (");
            sb.Append(ParkingSpot.Sector);
            sb.Append(", ");
            sb.Append(ParkingSpot.Spot);
            sb.Append(")");
            return sb.ToString();
        }
    }
    public struct ParkingSpot
    {
        public int Sector { get; set; }
        public int Spot { get; set; }
    }


}
namespace Himineu_system
{
    class Database
    {
        public IDictionary<string, ParkedVehicle> VehiclesInPark { get; set; }
        public IDictionary<string, List<ParkedVehicle>> OwnerVehicles { get; set; }

        public int[] SpotsTakenPerSector { get; set; }
        public Database(int sectors)
        {
            SpotsTakenPerSector = new int[sectors];
            VehiclesInPark = new Dictionary<string, ParkedVehicle>();
            OwnerVehicles = new Dictionary<string, List<ParkedVehicle>>();
        }

        

        #region Old messy implementation
        //public Data(int numberOfSectors)
        //{
        //    CarsInParking = new Dictionary<IVehicle, string>();
        //    park = new Dictionary<string, IVehicle>();
        //    números = new Dictionary<string, IVehicle>();
        //    d = new Dictionary<IVehicle, DateTime>();
        //    ow = new MultiDictionary<string, IVehicle>(false);
        //    count = new int[numberOfSectors];
        //}
        //public Dictionary<IVehicle, string> CarsInParking { get; set; }
        //public Dictionary<string, IVehicle> park { get; set; }
        //public Dictionary<string, IVehicle> números { get; set; }
        //public Dictionary<IVehicle, DateTime> d { get; set; }
        //public MultiDictionary<string, IVehicle> ow { get; set; }
        //public int[] count { get; set; }
        #endregion
    }
}

class Layout
{
    public int Sectors { get; set; }
    public int PlacesPerSector { get; set; }

    public Database Database { get; set; }

    private bool[,] _parkingSpots;
    
    public Layout(int numberOfSectors, int placesPerSector)
    {
        if (numberOfSectors <= 0)
        {
            throw new ArgumentException("The number of sectors must be positive.");
        }
        Sectors = numberOfSectors;
        

        if (placesPerSector <= 0)
        {
            throw new ArgumentException("The number of places per sector must be positive.");
        }
        Database = new Database(numberOfSectors);
        PlacesPerSector = placesPerSector;
        _parkingSpots = new bool[numberOfSectors, placesPerSector];
    }

    /// <summary>
    /// Fills a parking spot.
    /// </summary>
    /// <param name="sector">Sector number</param>
    /// <param name="spot">Spot in the sector</param>
    public void FillParkingSpot(int sector, int spot)
    {
        if (!IsSpotFilled(sector, spot))
        {
            _parkingSpots[sector - 1, spot - 1] = true;
            Database.SpotsTakenPerSector[sector - 1]++;
        }
    }

    /// <summary>
    /// Checks if a parking spot is filled.
    /// </summary>
    /// <param name="sector">Sector number</param>
    /// <param name="spot">Spot in the sector</param>
    /// <returns><c>True</c> if the spot is filled, <c>False</c> otherwise</returns>
    public bool IsSpotFilled(int sector, int spot)
    {
        return _parkingSpots[sector - 1, spot - 1];
    }

    /// <summary>
    /// Empties a parking spot
    /// </summary>
    /// <param name="sector">Sector number</param>
    /// <param name="spot">Spot in the sector</param>
    public void EmptyParkingSpot(int sector, int spot)
    {
        if (_parkingSpots[sector - 1, spot - 1])
        {
            _parkingSpots[sector - 1, spot - 1] = false;
            Database.SpotsTakenPerSector[sector - 1]--;
        }
    }

    /// <summary>
    /// Gets the status of the parking lot
    /// </summary>
    /// <returns>A string with the status information.</returns>
    public string GetParkingLotStatus() {
        StringBuilder sb = new StringBuilder(200);
        for (int i = 0; i < Database.SpotsTakenPerSector.Length; i++)
        {
            decimal percentageFilled = (decimal)Database.SpotsTakenPerSector[i] / PlacesPerSector;
            sb.Append("Sector ");
            sb.Append(i + 1);
            sb.Append(": ");
            sb.Append(Database.SpotsTakenPerSector[i]);
            sb.Append(" / ");
            sb.Append(PlacesPerSector);
            sb.Append(" (");
            sb.Append($"{percentageFilled:P2}");
            sb.Append("% full)");
            sb.Append(Environment.NewLine);
        }
        return sb.ToString();
    }
}