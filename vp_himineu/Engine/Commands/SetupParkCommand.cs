using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vp_himineu.Abstract;

namespace vp_himineu.VehicleParkEngine.Commands
{
    public class SetupParkCommand : CommandBase, ICommand
    {
        public SetupParkCommand(string name, IDictionary<string, string> parameters) 
            : base(name, parameters)
        {
            //TODO: set the rest of the values
        }

        public string ExcecuteCommand(IVehiclePark vehiclePark)
        {
            try
            {
                base.ValidateEnvironment(vehiclePark);
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }
            throw new NotImplementedException();
        }
    
        
    }
}
