using System;
using System.Text;
using vp_himineu.Concrete;

namespace vp_himineu
{
    public class Ticket
    {
        public ParkedVehicle ParkedVehicle { get; set; }
        public DateTime ExitTime { get; set; }
        public decimal AmountPaid { get; set; }
        public Ticket(ParkedVehicle parkedVehicle, DateTime exitTime, decimal amountPaid)
        {
            ParkedVehicle = parkedVehicle;
            ExitTime = exitTime;
            AmountPaid = amountPaid;
        }

        private decimal CalculatePrice() {

            int hoursOvertime = 0;
            if (ExitTime > ParkedVehicle.EntryTime.AddHours(ParkedVehicle.Vehicle.ReservedHours))
            {
                hoursOvertime = 1 + (ExitTime - ParkedVehicle.EntryTime.AddHours(ParkedVehicle.Vehicle.ReservedHours)).Hours;
            }
            return ParkedVehicle.Vehicle.RegularRate * ParkedVehicle.Vehicle.ReservedHours + hoursOvertime * ParkedVehicle.Vehicle.OvertimeRate;
        }
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
            sb.Append(AmountPaid.ToString("{0:n2}"));
            sb.Append(Environment.NewLine);

            sb.Append("Change: ");
            sb.Append((price - AmountPaid).ToString("{0:n2}"));
            sb.Append("*******************");

            return sb.ToString();
        }
    }
}
