using System;
using vp_himineu.Abstract;
using vp_himineu.Concrete;

namespace vp_himineu.Concrete
{
    public class Engine : IEngine
    {
        private IVehiclePark _vehiclePark;
        public IVehiclePark VehiclePark
        {
            get
            {
                return _vehiclePark;
            }

            set
            {
                _vehiclePark = value;
            }
        }

        
        public string ExcecuteCommand(ICommand c)
        {
            return c.ExcecuteCommand(VehiclePark);
            
            //switch (c.Name)
            //{

            //    case "SetupPark":
            //        //This doesnot work!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //        // I donot know why!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!



            //        //VehiclePark=new VehiclePark(c.Parameters["sectors"]+1,c.Parameters["placesPerSector"]);
            //        return "Vehicle park created";
            //    case "Рark":
            //        switch (c.Parameters["type"])
            //        {
            //            case "car":
            //                return VehiclePark.InsertCar(c.Parameters.GetCar(), int.Parse(c.Parameters["sector"]), int.Parse(c.Parameters["place"]), DateTime.Parse(c.Parameters["time"], null, System.Globalization.DateTimeStyles.RoundtripKind));//why round trip kind??
            //            case "motorbike": return VehiclePark.InsertMotorbike(new VehiclePark3.Motorbike(c.Parameters["licensePlate"], c.Parameters["owner"], int.Parse(c.Parameters["hours"])), int.Parse(c.Parameters["sector"]), int.Parse(c.Parameters["place"]), DateTime.Parse(c.Parameters["time"], null, System.Globalization.DateTimeStyles.RoundtripKind));//stack overflow says this
            //            case "truck": return VehiclePark.InsertTruck(new VehiclePark3.Truck(c.Parameters["licensePlate"], c.Parameters["owner"], int.Parse(c.Parameters["hours"])), int.Parse(c.Parameters["sector"]), int.Parse(c.Parameters["place"]), DateTime.Parse(c.Parameters["time"], null, System.Globalization.DateTimeStyles.RoundtripKind));//I wanna know
            //        }
            //        break;

            //    case "Exit":
            //        return VehiclePark.ExitVehicle(c.Parameters["licensePlate"], DateTime.Parse(c.Parameters["time"], null, System.Globalization.DateTimeStyles.RoundtripKind), decimal.Parse(c.Parameters["money"]));
            //    case "Status":
            //        return VehiclePark.GetStatus();
            //    case "FindVehicle":
            //        return VehiclePark.FindVehicle(c.Parameters["licensePlate"]);
            //    case "VehiclesByOwner":
            //        return VehiclePark.FindVehiclesByOwner(c.Parameters["owner"]);
            //    default:
            //        throw new IndexOutOfRangeException("Invalid command.");
            //}
            //return "";
        }

        public string ExecuteCommand(ICommand c)
        {
            throw new NotImplementedException();
        }
    }
}















