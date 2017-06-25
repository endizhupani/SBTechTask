namespace Vp_himineu
{
    using System;
    using System.Text;
    using Concrete.VehicleParking;

    public class Ticket
    {
        public Ticket(ParkedVehicle parkedVehicle, DateTime exitTime, decimal amountPaid)
        {
            ParkedVehicle = parkedVehicle;
            this.ExitTime = exitTime;
            this.AmountPaid = amountPaid;
        }

        public ParkedVehicle ParkedVehicle { get; set; }

        public DateTime ExitTime { get; set; }

        public decimal AmountPaid { get; set; }
       
        public override string ToString()
        {
            decimal price = this.CalculatePrice();
            StringBuilder sb = new StringBuilder(600);
            sb.Append("*******************");
            sb.Append(Environment.NewLine);
            sb.Append(ParkedVehicle.ToString());
            sb.Append(Environment.NewLine);
            sb.Append("Rate: ");
            sb.Append(ParkedVehicle.Vehicle.RegularRate.ToString("0.##"));
            sb.Append(Environment.NewLine);
            sb.Append("Overtime rate: ");
            sb.Append(ParkedVehicle.Vehicle.OvertimeRate.ToString("0.##"));
            sb.Append(Environment.NewLine);
            sb.Append("--------------------");
            sb.Append(Environment.NewLine);

            sb.Append("Total: ");
            sb.Append(price.ToString("0.##"));
            sb.Append(Environment.NewLine);

            sb.Append("Paid: ");
            sb.Append(this.AmountPaid.ToString("0.##"));
            sb.Append(Environment.NewLine);

            sb.Append("Change: ");
            sb.Append((this.AmountPaid - price).ToString("0.##"));
            sb.Append(Environment.NewLine);
            sb.Append("*******************");

            return sb.ToString();
        }

        /// <summary>
        /// Calculates the price of parking. 
        /// </summary>
        /// <remarks>The number of hours spent is always one larger than the difference in hours for the entry and exit time. This is to handle cases when a vehicle has exeeded it's stay by even a second. In these cases a full extra hour is calculated.</remarks>
        /// <returns></returns>
        public decimal CalculatePrice()
        {
            int hoursOvertime = 0;
            if (this.ExitTime > ParkedVehicle.EntryTime.AddHours(ParkedVehicle.Vehicle.ReservedHours))
            {
                hoursOvertime = 1 + (this.ExitTime - ParkedVehicle.EntryTime.AddHours(ParkedVehicle.Vehicle.ReservedHours)).Hours;
            }

            return (ParkedVehicle.Vehicle.RegularRate * ParkedVehicle.Vehicle.ReservedHours) + (hoursOvertime * ParkedVehicle.Vehicle.OvertimeRate);
        }
    }
}
