namespace Vp_himineu.Concrete.VehicleParking
{
    using System;
    using System.Text;

    public class Layout
    {
        private bool[,] parkingSpots;

        public Layout(int numberOfSectors, int placesPerSector)
        {
            if (numberOfSectors <= 0)
            {
                throw new ArgumentException("The number of sectors must be positive.");
            }

            this.Sectors = numberOfSectors;

            if (placesPerSector <= 0)
            {
                throw new ArgumentException("The number of places per sector must be positive.");
            }

            this.Database = new Database(numberOfSectors);
            this.PlacesPerSector = placesPerSector;
            this.parkingSpots = new bool[numberOfSectors, placesPerSector];
        }

        public int Sectors { get; set; }

        public int PlacesPerSector { get; set; }

        public Database Database { get; set; }

        /// <summary>
        /// Fills a parking spot.
        /// </summary>
        /// <param name="sector">Sector number</param>
        /// <param name="spot">Spot in the sector</param>
        public void FillParkingSpot(int sector, int spot)
        {
            if (!this.IsSpotFilled(sector, spot))
            {
                this.parkingSpots[sector - 1, spot - 1] = true;
                this.Database.SpotsTakenPerSector[sector - 1]++;
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
            return this.parkingSpots[sector - 1, spot - 1];
        }

        /// <summary>
        /// Empties a parking spot
        /// </summary>
        /// <param name="sector">Sector number</param>
        /// <param name="spot">Spot in the sector</param>
        public void EmptyParkingSpot(int sector, int spot)
        {
            if (this.parkingSpots[sector - 1, spot - 1])
            {
                this.parkingSpots[sector - 1, spot - 1] = false;
                this.Database.SpotsTakenPerSector[sector - 1]--;
            }
        }

        /// <summary>
        /// Gets the status of the parking lot
        /// </summary>
        /// <returns>A string with the status information.</returns>
        public string GetParkingLotStatus()
        {
            StringBuilder sb = new StringBuilder(200);
            for (int i = 0; i < this.Database.SpotsTakenPerSector.Length; i++)
            {
                decimal percentageFilled = (decimal)this.Database.SpotsTakenPerSector[i] / this.PlacesPerSector;
                sb.Append("Sector ");
                sb.Append(i + 1);
                sb.Append(": ");
                sb.Append(this.Database.SpotsTakenPerSector[i]);
                sb.Append(" / ");
                sb.Append(this.PlacesPerSector);
                sb.Append(" (");
                sb.Append($"{percentageFilled:P2}");
                sb.Append("% full)");
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
