namespace Vp_himineu.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Vp_himineu;
    using Vp_himineu.Abstract;
    using Vp_himineu.Concrete;
    using Vp_himineu.Concrete.VehicleParking;
    using Vp_himineu.Concrete.Vehicles;
    using Vp_himineu.VehicleParkEngine.Commands;
    
    [TestClass]
    public class UnitTests
    {
        #region Insert vehicle tests
        /// <summary>
        /// Insert a vehicle into the parking lot
        /// </summary>
        [TestMethod]
        public void TestInsertCarSuccess()
        {
            // Arrage
            VehiclePark vehiclePark = new VehiclePark(new Layout(10, 20, new Database(10)));
            IVehicle vehicle = new Car("CA1001HH", "Endi", 2);

            // Act
            string message = vehiclePark.InsertVehicle(vehicle, 5, 13, DateTime.Now);

            // Assert
            Assert.AreEqual("Car parked successfully at place (5,13)", message);
            Assert.IsTrue(vehiclePark.Layout.IsSpotFilled(5, 13));
            Assert.IsNotNull(vehiclePark.Layout.Database.VehiclesInPark["CA1001HH"]);
            Assert.AreEqual(1, vehiclePark.Layout.Database.OwnerVehicles["Endi"].Count);
        }

        [TestMethod]
        public void TestInsertSecondVehicleForOwner()
        {
            // Arrage
            VehiclePark vehiclePark = new VehiclePark(new Layout(10, 20, new Database(10)));
            IVehicle vehicle = new Car("CA1001HH", "Endi", 2);
            IVehicle vehicle2 = new Truck("CA1002HH", "Endi", 2);

            // Act
            vehiclePark.InsertVehicle(vehicle, 5, 13, DateTime.Now);
            vehiclePark.InsertVehicle(vehicle2, 6, 13, DateTime.Now);

            // Assert
            Assert.IsTrue(vehiclePark.Layout.IsSpotFilled(5, 13));
            Assert.IsTrue(vehiclePark.Layout.IsSpotFilled(6, 13));
            Assert.IsNotNull(vehiclePark.Layout.Database.VehiclesInPark["CA1001HH"]);
            Assert.IsNotNull(vehiclePark.Layout.Database.VehiclesInPark["CA1002HH"]);
            Assert.AreEqual(2, vehiclePark.Layout.Database.OwnerVehicles["Endi"].Count);
        }

        /// <summary>
        /// Tries to insert a vehicle that already exists
        /// </summary>
        [TestMethod]
        public void TestInsertCarAlreadyExists()
        {
            // Arrage
            VehiclePark vehiclePark = new VehiclePark(new Layout(10, 20, new Database(10)));
            IVehicle vehicle = new Car("CA1001HH", "Endi", 2);

            // Act
            vehiclePark.InsertVehicle(vehicle, 5, 13, DateTime.Now);
            string message = vehiclePark.InsertVehicle(vehicle, 6, 13, DateTime.Now);

            // Assert
            Assert.AreEqual("There is already a vehicle with license plate CA1001HH in the park", message);
            Assert.IsTrue(vehiclePark.Layout.IsSpotFilled(5, 13));
            Assert.IsFalse(vehiclePark.Layout.IsSpotFilled(6, 13));
            Assert.IsNotNull(vehiclePark.Layout.Database.VehiclesInPark["CA1001HH"]);
            Assert.AreEqual(1, vehiclePark.Layout.Database.OwnerVehicles["Endi"].Count);
        }

        /// <summary>
        /// Tries to insert a vehicle with an invalid license plate
        /// </summary>
        [TestMethod]
        public void TestInsertCarInvalidPlate()
        {
            // Arrage
            string message;
            VehiclePark vehiclePark = new VehiclePark(new Layout(10, 20, new Database(10)));
            try
            {
                IVehicle vehicle = new Car("CAAA01HH", "Endi", 2);

                // Act
                message = vehiclePark.InsertVehicle(vehicle, 6, 13, DateTime.Now);
            }
            catch (ArgumentException e)
            {
                message = e.Message;
            }

            // Assert
            Assert.AreEqual("The license plate number is invalid.", message);
            Assert.IsFalse(vehiclePark.Layout.IsSpotFilled(6, 13));
            Assert.IsFalse(vehiclePark.Layout.Database.VehiclesInPark.ContainsKey("CA01HH"));
            Assert.IsFalse(vehiclePark.Layout.Database.OwnerVehicles.ContainsKey("Endi"));
        }

        /// <summary>
        /// Tries to insert a vehicle into an occupied place
        /// </summary>
        [TestMethod]
        public void TestInsertVehiclePlaceOccupied()
        {
            // Arrage
            VehiclePark vehiclePark = new VehiclePark(new Layout(10, 20, new Database(10)));
            IVehicle vehicle = new Car("CA1001HH", "Endi", 2);
            IVehicle vehicle2 = new Motorbike("CA1021HH", "Endi", 2);

            // Act
            vehiclePark.InsertVehicle(vehicle, 5, 13, DateTime.Now);
            string message = vehiclePark.InsertVehicle(vehicle2, 5, 13, DateTime.Now);

            // Assert
            Assert.AreEqual("The place (5,13) is occupied", message);
            Assert.IsFalse(vehiclePark.Layout.Database.VehiclesInPark.ContainsKey("CA1021HH"));
            Assert.AreEqual(1, vehiclePark.Layout.Database.OwnerVehicles["Endi"].Count);
        }

        /// <summary>
        /// Tries to insert a vehicle into a sector that does not exist
        /// </summary>
        [TestMethod]
        public void TestInsertCarOutOfRangeSector()
        {
            // Arrage
            VehiclePark vehiclePark = new VehiclePark(new Layout(10, 20, new Database(10)));
            IVehicle vehicle = new Car("CA1001HH", "Endi", 2);

            // Act
            string message = vehiclePark.InsertVehicle(vehicle, 11, 13, DateTime.Now);

            // Assert
            Assert.AreEqual("There is no sector 11 in the park", message);
            Assert.IsFalse(vehiclePark.Layout.Database.VehiclesInPark.ContainsKey("CA1001HH"));
            Assert.IsFalse(vehiclePark.Layout.Database.OwnerVehicles.ContainsKey("Endi"));
        }

        /// <summary>
        /// Tries to insert a vehicle into a place that does not exist
        /// </summary>
        [TestMethod]
        public void TestInsertCarOutOfRangePlace()
        {
            // Arrage
            VehiclePark vehiclePark = new VehiclePark(new Layout(10, 20, new Database(10)));
            IVehicle vehicle = new Car("CA1001HH", "Endi", 2);

            // Act
            string message = vehiclePark.InsertVehicle(vehicle, 5, 21, DateTime.Now);

            // Assert
            Assert.AreEqual("There is no place 21 in sector 5", message);
            Assert.IsFalse(vehiclePark.Layout.Database.VehiclesInPark.ContainsKey("CA1001HH"));
            Assert.IsFalse(vehiclePark.Layout.Database.OwnerVehicles.ContainsKey("Endi"));
        }

        /// <summary>
        /// Tries to insert a car when there is no parking lot.
        /// </summary>
        [TestMethod]
        public void TestInsertCarNoParkingLot()
        {
            // Arrage
            IEngine engine = new Engine();
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("type", "car");
            parameters.Add("time", "2015-05-04T10:30:00.0000000");
            parameters.Add("sector", "1");
            parameters.Add("place", "5");
            parameters.Add("licensePlate", "CA1001HH");
            parameters.Add("owner", "Endi");
            parameters.Add("hours", "1");

            ICommand parkCommand = new ParkCommand("park", parameters);

            // Act
            string message = engine.ExecuteCommand(parkCommand);

            // Assert
            Assert.AreEqual("The vehicle park has not been set up", message);
        }
        #endregion

        #region Exit vehicle tests

        /// <summary>
        /// Tests if a valid vehicle is exited successfully
        /// </summary>
        [TestMethod]
        public void TestExitVehicleSuccess()
        {
            // Arrange
            DateTime nowTime = DateTime.Now;
            VehiclePark vehiclePark = new VehiclePark(new Layout(10, 20, new Database(10)));
            IVehicle vehicle = new Car("CA1001HH", "Endi", 1);
            vehiclePark.InsertVehicle(vehicle, 5, 5, nowTime.AddHours(-2));
            var parkedVehicle = vehiclePark.Layout.Database.VehiclesInPark["CA1001HH"];
            Ticket expectedTicket = new Ticket(parkedVehicle, nowTime.AddHours(1), 50);
            
            // Act
            string actualTicketString = vehiclePark.ExitVehicle("CA1001HH", 50, nowTime.AddHours(1));

            // Assert
            Assert.AreEqual(expectedTicket.ToString(), actualTicketString);
            Assert.IsFalse(vehiclePark.Layout.IsSpotFilled(5, 5));
            Assert.IsFalse(vehiclePark.Layout.Database.VehiclesInPark.ContainsKey("CA1001HH"));
            Assert.IsFalse(vehiclePark.Layout.Database.OwnerVehicles.ContainsKey("Endi"), "Endi still exists in the database");
        }

        [TestMethod]
        public void TestExitNonExistingVehicle()
        {
            // Arrange
            DateTime nowTime = DateTime.Now;
            VehiclePark vehiclePark = new VehiclePark(new Layout(10, 20, new Database(10)));

            // Act
            string message = vehiclePark.ExitVehicle("CA1001HH", 50, nowTime.AddHours(1));

            // Assert
            Assert.AreEqual("There is no vehicle with license plate CA1001HH in the park", message);
        }

        [TestMethod]
        public void TestPriceCalculation()
        {
            // Arrange
            DateTime nowTime = DateTime.Now;
            IVehicle vehicle = new Car("CA1001HH", "Endi", 1);
            ParkedVehicle parkedVehicle = new ParkedVehicle(
                vehicle, 
                new ParkingSpot
                {
                    Sector = 1,
                    Spot = 1
                }, 
                nowTime.AddHours(-2));
            Ticket ticket = new Ticket(parkedVehicle, nowTime.AddHours(1), 50);
            
            // Act
            decimal actualPrice = ticket.CalculatePrice();

            // Assert
            Assert.AreEqual(12.5M, actualPrice);
        }
        #endregion

        #region Get status tests

        /// <summary>
        /// Tests the GetStatus method of vehicle park
        /// </summary>
        [TestMethod]
        public void TestGetStatus()
        {
            // Arrage
            VehiclePark vehiclePark = new VehiclePark(new Layout(2, 6, new Database(2)));
            IVehicle vehicle = new Car("CA1001HH", "Endi", 2);
            vehiclePark.InsertVehicle(vehicle, 1, 2, DateTime.Now);
            StringBuilder sb = new StringBuilder();
            sb.Append("Sector 1: 1 / 6 (17 % full)");
            sb.Append(Environment.NewLine);
            sb.Append("Sector 2: 0 / 6 (0 % full)");
            sb.Append(Environment.NewLine);
            string expectedStatus = sb.ToString();

            // Act
            string message = vehiclePark.GetStatus();

            // Assert
            Assert.AreEqual(expectedStatus, message);
        }

        #endregion

        #region Find Vehicle Tests

        [TestMethod]
        public void TestFindVehicleSuccess()
        {
            // Arrange
            IVehiclePark vehiclePark = new VehiclePark(this.CreateMockLayout());
            StringBuilder sb = new StringBuilder();
            sb.Append("Car [FF00021AC], owned by Endi");
            sb.Append(Environment.NewLine);
            sb.Append("Parked at (1, 1)");
            string expectedMessage = sb.ToString();

            // Act
            string message = vehiclePark.FindVehicle("FF00021AC");

            // Assert
            Assert.AreEqual(expectedMessage, message);
        }

        [TestMethod]
        public void TestFindVehicleFail()
        {
            // Arrange
            IVehiclePark vehiclePark = new VehiclePark(this.CreateMockLayout());
            string expectedMessage = "There is no vehicle with license plate FF00021AB in the park";

            // Act
            string message = vehiclePark.FindVehicle("FF00021AB");

            // Assert
            Assert.AreEqual(expectedMessage, message);
        }

        [TestMethod]
        public void TestFindVehicleByOwnerSuccess()
        {
            // Arrange
            IVehiclePark vehiclePark = new VehiclePark(this.CreateMockLayout());
            StringBuilder sb = new StringBuilder();
            sb.Append("Car [FF00021AC], owned by Endi");
            sb.Append(Environment.NewLine);
            sb.Append("Parked at (1, 1)");
            sb.Append(Environment.NewLine);
            string expectedMessage = sb.ToString();

            // Act
            string message = vehiclePark.FindVehiclesByOwner("Endi");

            // Assert
            Assert.AreEqual(expectedMessage, message);
        }

        [TestMethod]
        public void TestFindVehicleByOwnerFail()
        {
            // Arrange
            IVehiclePark vehiclePark = new VehiclePark(this.CreateMockLayout());
            string expectedMessage = "No vehicles by Doesn't Exist";

            // Act
            string message = vehiclePark.FindVehiclesByOwner("Doesn't Exist");

            // Assert
            Assert.AreEqual(expectedMessage, message);
        }

        private Database CreateMockDatabase()
        {
            DateTime now = DateTime.Now;

            Database db = new Database(5);

            ParkedVehicle parkedVehicle1 = new ParkedVehicle(
                    new Car("FF00021AC", "Endi", 2),
                    new ParkingSpot
                    {
                        Sector = 1,
                        Spot = 1
                    },
                    now.AddHours(-1));

            db.VehiclesInPark.Add("FF00021AC", parkedVehicle1);
            db.OwnerVehicles.Add("Endi", new List<ParkedVehicle> { parkedVehicle1 });
            db.SpotsTakenPerSector[0]++;
            ParkedVehicle parkedVehicle2 = new ParkedVehicle(
                    new Car("FF00021DE", "John", 2),
                    new ParkingSpot
                    {
                        Sector = 1,
                        Spot = 2
                    },
                    now.AddHours(-2));
            db.VehiclesInPark.Add("FF00021DE", parkedVehicle2);
            db.OwnerVehicles.Add(
                "John", 
                new List<ParkedVehicle>
                {
                    parkedVehicle2
                });
            db.SpotsTakenPerSector[0]++;
            return db;
        }

        private Layout CreateMockLayout()
        {
            Layout layout = new Layout(5, 5, this.CreateMockDatabase());
            layout.FillParkingSpot(1, 1);
            layout.FillParkingSpot(1, 2);
            return layout;
        }
        #endregion
    }
}
