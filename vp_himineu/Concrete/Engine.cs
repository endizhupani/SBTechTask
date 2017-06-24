namespace Vp_himineu.Concrete
{
    using System;
    using Vp_himineu.Abstract;

    public class Engine : IEngine
    {
        private IVehiclePark vehiclePark;

        public IVehiclePark VehiclePark
        {
            get
            {
                return this.vehiclePark;
            }

            set
            {
                this.vehiclePark = value;
            }
        }

        public string ExecuteCommand(ICommand c)
        {
            return c.ExcecuteCommand(this.VehiclePark);
        }
    }
}