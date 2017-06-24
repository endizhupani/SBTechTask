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
            sb.Append(ParkedVehicle.Vehicle.RegularRate.ToString("{0:n2}"));
            sb.Append(Environment.NewLine);
            sb.Append("Overtime rate: ");
            sb.Append(ParkedVehicle.Vehicle.OvertimeRate.ToString("{0:n2}"));
            sb.Append(Environment.NewLine);
            sb.Append("--------------------");
            sb.Append(Environment.NewLine);

            sb.Append("Total: ");
            sb.Append(price.ToString("{0:n2}"));
            sb.Append(Environment.NewLine);

            sb.Append("Paid: ");
            sb.Append(this.AmountPaid.ToString("{0:n2}"));
            sb.Append(Environment.NewLine);

            sb.Append("Change: ");
            sb.Append((price - this.AmountPaid).ToString("{0:n2}"));
            sb.Append("*******************");

            return sb.ToString();
        }

        private decimal CalculatePrice()
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
