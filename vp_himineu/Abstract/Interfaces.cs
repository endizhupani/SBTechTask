using System;
using System.Collections.Generic;
using vp_himineu.Concrete;

namespace vp_himineu.Abstract
{
    // Don't touch - I like it centered!
    interface IUserInterface
    {
        string ReadLine();
        void WriteLine(string format, params string[] args);
    }

    public interface IEngine {
        IVehiclePark VehiclePark { get; set; }

        /// <summary>
        /// Excecutes the command specified in the <paramref name="c"/>
        /// </summary>
        /// <param name="c">The command to be excecuted</param>
        /// <returns>The result of the command</returns>
        string ExecuteCommand(ICommand c);
    }

    /// <summary>
    /// Interface used to interact with the vehicle park.
    /// </summary>
    public interface IVehiclePark
    {
        // PERFORMANCE: The methods to insert a vehicle are performance bottlenecks. 
        // This is because they need to make sure that there is no other vehicle in the same parking spot and searching all the items in a dictionary one by one is expensive.
        // SOLUTION: have a two dimensional array in the Layout class. 
        // Rows represent sectors, columns represent spots in each sector. 
        // If a spot is taken, the value of the element in the array is the license plate. If not it is an empty string.


        /// <summary>
        /// Inserts a vehicle in the specified spot.
        /// </summary>
        /// <param name="vehicle">Vehicle to be inserted.</param>
        /// <param name="sector">The sector the car will be inserted into.</param>
        /// <param name="placeNumber">The parking spot inside the sector the car will be inserted into.</param>
        /// <returns>A message with the vehicle's parking spot details.</returns>
        /// <remarks>Use this method because it validates the data!</remarks>
        string InsertVehicle(IVehicle vehicle, int sector, int placeNumber);

        ///// <summary>
        ///// Inserts a car into the the specified spot.
        ///// </summary>
        ///// <param name="car">Car to be inserted.</param>
        ///// <param name="sector">The sector the car will be inserted into.</param>
        ///// <param name="placeNumber">The parking spot inside the sector the car will be inserted into.</param>
        ///// <returns>A message with the car's parking spot details.</returns>
        //string InsertCar(Car car, int sector, int placeNumber);

        ///// <summary>
        ///// Inserts a motorbike into the the specified spot.
        ///// </summary>
        ///// <param name="motorbike">Motorbike to be inserted.</param>
        ///// <param name="sector">The sector the motorbike will be inserted into.</param>
        ///// <param name="placeNumber">The parking spot inside the sector the motorbike will be inserted into.</param>
        ///// <returns>A message with the motorbike's parking spot details.</returns>
        //string InsertMotorbike(Motorbike motorbike, int sector, int placeNumber);

        ///// <summary>
        ///// Inserts a truck into the the specified spot.
        ///// </summary>
        ///// <param name="truck">Truck to be inserted.</param>
        ///// <param name="sector">The sector the truck will be inserted into.</param>
        ///// <param name="placeNumber">The parking spot inside the sector the truck will be inserted into.</param>

        ///// <returns>A message with the truck's parking spot details.</returns>
        //string InsertTruck(Truck truck, int sector, int placeNumber);

        /// <summary>
        /// Exits a vehicle from the parking spot
        /// </summary>
        /// <param name="licensePlate">License plate of the vehicle to be exited</param>
        /// <param name="amountPaid">Amount that is paid</param>
        /// <param name="exitTime">Time of exit</param>
        /// <returns>The parking ticket.</returns>
        string ExitVehicle(string licensePlate, decimal amountPaid, DateTime exitTime);


        // PERFORMANCE: it has to check the entire dictionary of vehicles and keep track of the spots and sectors they have taken
        // SOLUTION : in addition to the 
        /// <summary>
        /// Gets the status of the parking lot.
        /// </summary>
        /// <returns>A string indicating the occupancy of each sector in the parking lot</returns>
        string GetStatus();

        /// <summary>
        /// Finds a vehicle from it's license plate
        /// </summary>
        /// <param name="licensePlate">The licence plate of the vehicle to retrieve</param>
        /// <returns>A string with the vehicle information.</returns>
        string FindVehicle(string licensePlate);

        // PERFORMANCE: The system has to go through every object in the parking lot to see who is the owner. 
        // SOLUTION: Maybe add a dictionary with the owners as key and a list of vehicles.
        /// <summary>
        /// Gets all the vehicles with a particular owner
        /// </summary>
        /// <param name="owner">The name of the owner</param>
        /// <returns>A string with the information for every vehicle.</returns>
        string FindVehiclesByOwner(string owner);
    }

    public interface ICommand
    {
        string Name { get; }
        IDictionary<string, string> Parameters { get; }

        /// <summary>
        /// Excecutes the command on the specified vehicle park.
        /// </summary>
        /// <param name="vehiclePark">The vehicle park upon which the command should be excecuted</param>
        /// <returns>A string representing the result of the command</returns>
        string ExcecuteCommand(IVehiclePark vehiclePark);
    }

    interface IMechanism
    {
        void Run();
    }

    public interface IVehicle
    {
        string LicensePlate { get; }
        string Owner { get; }
        decimal RegularRate { get; }
        decimal OvertimeRate { get; }
        int ReservedHours { get; }
    }
}
