/// <summary>
/// Contains all the interfaces the system uses
/// </summary>
namespace Vp_himineu.Abstract
{
    using System;
    using System.Collections.Generic;
    using Concrete.VehicleParking;
    using Concrete.Vehicles;

    // Don't touch - I like it centered!

    /// <summary>
    /// Interface to communicate with the user
    /// </summary>
    public interface IUserInterface
    {
        /// <summary>
        /// Reads a command from the console
        /// </summary>
        /// <returns>The command that is read</returns>
        ICommand ReadLine();

        /// <summary>
        /// Writes text to the console
        /// </summary>
        /// <param name="text">The text to be written</param>
        void WriteLine(string text);
    }

    /// <summary>
    /// Interface for the parking engine.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Executes the command specified in the <paramref name="c"/>
        /// </summary>
        /// <param name="c">The command to be executed</param>
        /// <returns>The result of the command</returns>
        string ExecuteCommand(ICommand c);
    }

    /// <summary>
    /// Interface used to interact with the vehicle park.
    /// </summary>
    /// <remarks>
    /// The properties for each vehicle are actualy the same. Only their values differ. 
    /// It would be better to eliminate the vehicle specific insert methods and have only one Insert method that takes IVehicle as a parameter however, 
    /// the requirements specifically say not to do this so I left it as is.
    /// </remarks>
    public interface IVehiclePark
    {
        // PERFORMANCE: The methods to insert a vehicle are performance bottlenecks. 
        // This is because they need to make sure that there is no other vehicle in the same parking spot and searching all the items in a dictionary one by one is expensive.
        // SOLUTION: have a two dimensional array in the Layout class. 
        // Rows represent sectors, columns represent spots in each sector. 
        // If a spot is taken, the value of the element in the array is true. If not it is false.

        /////// <summary>
        /////// Inserts a vehicle in the specified spot.
        /////// </summary>
        /////// <param name="vehicle">Vehicle to be inserted.</param>
        /////// <param name="sector">The sector the car will be inserted into.</param>
        /////// <param name="placeNumber">The parking spot inside the sector the car will be inserted into.</param>
        /////// <param name="entryTime">Time the vehicle entered the parking lot.</param>
        /////// <returns>A message with the vehicle's parking spot details.</returns>
        /////// <remarks>Use this method because it validates the data!</remarks>
        ////string InsertVehicle(IVehicle vehicle, int sector, int placeNumber, DateTime entryTime);

        /// <summary>
        /// Inserts a car into the the specified spot.
        /// </summary>
        /// <param name="car">Car to be inserted.</param>
        /// <param name="sector">The sector the car will be inserted into.</param>
        /// <param name="placeNumber">The parking spot inside the sector the car will be inserted into.</param>
        /// <param name="startTime">The time the car entered the parking lot.</param>
        /// <returns>A message with the car's parking spot details.</returns>
        string InsertCar(Car car, int sector, int placeNumber, DateTime startTime);

        /// <summary>
        /// Inserts a motorbike into the the specified spot.
        /// </summary>
        /// <param name="motorbike">Motorbike to be inserted.</param>
        /// <param name="sector">The sector the motorbike will be inserted into.</param>
        /// <param name="placeNumber">The parking spot inside the sector the motorbike will be inserted into.</param>
        /// <param name="startTime">The time the motorbike entered the parking lot.</param>
        /// <returns>A message with the motorbike's parking spot details.</returns>
        string InsertMotorbike(Motorbike motorbike, int sector, int placeNumber, DateTime startTime);

        /// <summary>
        /// Inserts a truck into the the specified spot.
        /// </summary>
        /// <param name="truck">Truck to be inserted.</param>
        /// <param name="sector">The sector the truck will be inserted into.</param>
        /// <param name="placeNumber">The parking spot inside the sector the truck will be inserted into.</param>
        /// <param name="startTime">The time the truck entered the parking lot.</param>
        /// <returns>A message with the truck's parking spot details.</returns>
        string InsertTruck(Truck truck, int sector, int placeNumber, DateTime startTime);

        /// <summary>
        /// Exits a vehicle from the parking spot
        /// </summary>
        /// <param name="licensePlate">License plate of the vehicle to be exited</param>
        /// <param name="amountPaid">Amount that is paid</param>
        /// <param name="exitTime">Time of exit</param>
        /// <returns>The parking ticket.</returns>
        string ExitVehicle(string licensePlate, decimal amountPaid, DateTime exitTime);

        // PERFORMANCE: it has to check the entire dictionary of vehicles and keep track of the spots and sectors they have taken
        // SOLUTION : Add an array to keep track of the number of fileld out spots in the parking lot

        /// <summary>
        /// Gets the status of the parking lot.
        /// </summary>
        /// <returns>A string indicating the occupancy of each sector in the parking lot</returns>
        string GetStatus();

        /// <summary>
        /// Finds a vehicle from it's license plate
        /// </summary>
        /// <param name="licensePlate">The license plate of the vehicle to retrieve</param>
        /// <returns>A string with the vehicle information.</returns>
        string FindVehicle(string licensePlate);

        // PERFORMANCE: The system has to go through every object in the parking lot to see who is the owner. 
        // SOLUTION: Add a dictionary with the owners as key and a list of vehicles as value.

        /// <summary>
        /// Gets all the vehicles with a particular owner
        /// </summary>
        /// <param name="owner">The name of the owner</param>
        /// <returns>A string with the information for every vehicle.</returns>
        string FindVehiclesByOwner(string owner);
    }

    /// <summary>
    /// Interface for the different commands
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Name of the command
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Command paramaters.
        /// </summary>
        IDictionary<string, string> Parameters { get; }

        /// <summary>
        /// Excecutes the command on the specified vehicle park.
        /// </summary>
        /// <param name="vehiclePark">The vehicle park upon which the command should be excecuted</param>
        /// <returns>A string representing the result of the command</returns>
        string ExcecuteCommand(ref IVehiclePark vehiclePark);
    }

    public interface IMechanism
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

    public interface ILayout
    {
        int Sectors { get; set; }

        int PlacesPerSector { get; set; }

        IDatabase Database { get; set; }

        /// <summary>
        /// Fills a parking spot.
        /// </summary>
        /// <param name="sector">Sector number</param>
        /// <param name="spot">Spot in the sector</param>
        void FillParkingSpot(int sector, int spot);

        /// <summary>
        /// Checks if a parking spot is filled.
        /// </summary>
        /// <param name="sector">Sector number</param>
        /// <param name="spot">Spot in the sector</param>
        /// <returns><c>True</c> if the spot is filled, <c>False</c> otherwise</returns>
        bool IsSpotFilled(int sector, int spot);

        /// <summary>
        /// Empties a parking spot
        /// </summary>
        /// <param name="sector">Sector number</param>
        /// <param name="spot">Spot in the sector</param>
        void EmptyParkingSpot(int sector, int spot);

        /// <summary>
        /// Gets the status of the parking lot
        /// </summary>
        /// <returns>A string with the status information.</returns>
        string GetParkingLotStatus();
    }

    public interface IDatabase
    {
        IDictionary<string, ParkedVehicle> VehiclesInPark { get; set; }

        IDictionary<string, List<ParkedVehicle>> OwnerVehicles { get; set; }

        int[] SpotsTakenPerSector { get; set; }
    }
}