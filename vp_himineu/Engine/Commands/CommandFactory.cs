namespace Vp_himineu.VehicleParkEngine.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;
    using Abstract;

    public static class CommandFactory
    {
        public static ICommand GetCommand(string commandString)
        {
            string commandName = commandString.Substring(0, commandString.IndexOf(' ')).ToLower();
            IDictionary<string, string> parameters = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(commandString.Substring(commandString.IndexOf(' ') + 1));

            switch (commandName)
            {
                case "park":
                    return new ParkCommand(commandName, parameters);
                case "exit":
                    return new ExitCommand(commandName, parameters);
                case "findvehicle":
                    return new FindVehicleCommand(commandName, parameters);
                case "vehiclesbyowner":
                    return new VehicleByOwnerCommand(commandName, parameters);
                case "status":
                    return new StatusCommand(commandName, parameters);
                case "setuppark":
                    return new SetupParkCommand(commandName, parameters);
                default:
                    throw new InvalidOperationException("This command is not supported by the system.");
            }
        }
    }
}
