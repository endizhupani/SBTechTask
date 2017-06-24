using System;
using System.Collections.Generic;
using vp_himineu.Abstract;
using vp_himineu.Concrete;

namespace vp_himineu.VehicleParkEngine
{
    public static class VehicleFactory
    {

        private static int GetReservedHours(this IDictionary<string, string> parameters) {
            int reservedHours = 0;
            if (!Int32.TryParse(parameters["reservedHours"], out reservedHours) || reservedHours <= 0)
            {
                throw new ArgumentException("The parameter reserved hours must be a positive integer");
            }
            return reservedHours;
        }

        private static VehicleType GetVehicleType(this IDictionary<string, string> parameters) {
            string typeName;
            if (!parameters.TryGetValue("type", out typeName))
            {
                throw new ArgumentNullException("You have to specify a vehicle type");
            }
            switch (typeName.ToLower())
            {
                case "car":
                    return VehicleType.Car;
                case "motorbike":
                    return VehicleType.Motorbike;
                case "truck":
                    return VehicleType.Truck;
                default:
                    throw new ArgumentException("The type of the vehicle must be 'car', 'motorbike' or 'truck'");

            };
        }
        public static IVehicle GetVehicle(IDictionary<string, string> parameters) {

            switch (parameters.GetVehicleType())
            {
                case VehicleType.Car:
                    return new Car(parameters["licensePlate"], parameters["owner"], parameters.GetReservedHours());
                case VehicleType.Motorbike:
                    return new Motorbike(parameters["licensePlate"], parameters["owner"], parameters.GetReservedHours());
                case VehicleType.Truck:
                    return new Truck(parameters["licensePlate"], parameters["owner"], parameters.GetReservedHours());
                default:
                    throw new Exception("Vehicle could not be created");
            }
        }
    }
}
