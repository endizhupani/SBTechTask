namespace Vp_himineu.VehicleParkEngine.Commands
{
    using System;
    using System.Collections.Generic;
    using Abstract;

    public class StatusCommand : CommandBase, ICommand
    {
        public StatusCommand(string name, IDictionary<string, string> parameters) : base(name, parameters)
        {
        }

        public string ExcecuteCommand(IVehiclePark vehiclePark)
        {
            try
            {
                this.ValidateEnvironment(vehiclePark);
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }

            return vehiclePark.GetStatus();
        }
    }
}
