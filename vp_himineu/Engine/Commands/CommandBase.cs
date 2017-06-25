namespace Vp_himineu.VehicleParkEngine.Commands
{
    using System;
    using System.Collections.Generic;
    using Abstract;

    public class CommandBase
    {
        private string name;
        private IDictionary<string, string> parameters;
       
        public CommandBase(string name, IDictionary<string, string> parameters)
        {
            this.name = name;
            this.parameters = parameters;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public IDictionary<string, string> Parameters
        {
            get
            {
                return this.parameters;
            }
        }

        protected void ValidateEnvironment(IVehiclePark vehiclePark)
        {
            if (this.Name != "SetupPark" && vehiclePark == null)
            {
                throw new InvalidOperationException("The vehicle park has not been set up");
            }
        }
    }
}
