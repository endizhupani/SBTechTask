namespace Vp_himineu.VehicleParkEngine.Commands
{
    using System;
    using System.Collections.Generic;
    using Vp_himineu.Abstract;

    public class VehicleByOwnerCommand : CommandBase, ICommand
    { 
        public VehicleByOwnerCommand(string name, IDictionary<string, string> parameters)
            : base(name, parameters)
        {
            string owner;
            if (!parameters.TryGetValue("owner", out owner))
            {
                throw new ArgumentNullException("You must specify the name of the owner");
            }

            this.Owner = owner;
        }

        public string Owner { get; set; }

        public string ExcecuteCommand(ref IVehiclePark vehiclePark)
        {
            try
            {
                this.ValidateEnvironment(vehiclePark);
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }

            return vehiclePark.FindVehiclesByOwner(this.Owner);
        }
    }
}
