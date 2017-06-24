using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using vp_himineu.Abstract;

namespace vp_himineu.VehicleParkEngine.Commands
{
    public class CommandBase
    {
        private string _name;
        private IDictionary<string, string> _parameters;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public IDictionary<string, string> Parameters
        {
            get
            {
                return _parameters;
            }
        }
        public CommandBase(string name, IDictionary<string, string> parameters)
        {
            _name = name;
            _parameters = parameters;
        }

        protected void ValidateEnvironment(IVehiclePark vehiclePark)
        {
            if (Name != "SetupPark" && vehiclePark == null)
            {
                throw new InvalidOperationException("The park is not setup yet");
            }
        }
    }
}
