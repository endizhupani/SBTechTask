namespace Vp_himineu.Concrete
{
    using System;
    using Vp_himineu.Abstract;

    public class Engine : IEngine
    {
        private IVehiclePark vehiclePark;

        public string ExecuteCommand(ICommand c)
        {
            return c.ExcecuteCommand(ref this.vehiclePark);
        }
    }
}