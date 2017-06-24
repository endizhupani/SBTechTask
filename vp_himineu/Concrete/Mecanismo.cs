using System;
using vp_himineu.Abstract;
using vp_himineu.VehicleParkEngine;
using vp_himineu.VehicleParkEngine.Commands;

namespace vp_himineu.Concrete
{
    class ParkingMechanism : IMechanism
    {
        private IEngine _engine;
        public ParkingMechanism (IEngine engine)
        {
            this._engine = engine;
        }

        //public ParkingMechanism () 
        //    : this (new Engine())
        //{
        //}
        public void Run()
        {
            while (true)
            {
                string commandLine = Console.ReadLine();
                if (commandLine == null)
                    break;

                commandLine.Trim();
                if (!string.IsNullOrEmpty(commandLine))
                    continue;
                try
                {
                    ICommand command = CommandFactory.GetCommand(commandLine);
                    string commandResult = _engine.ExecuteCommand(command);
                    Console.WriteLine(commandResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }
    }
}