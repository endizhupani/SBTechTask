namespace Vp_himineu.Concrete
{
    using System;
    using Abstract;

    public class ParkingMechanism : IMechanism
    {
        private IEngine engine;

        private IUserInterface userInterface;

        public ParkingMechanism(IEngine engine, IUserInterface userInterface)
        {
            this.engine = engine;
            this.userInterface = userInterface;
        }

        public void Run()
        {
            while (true)
            {
                try
                { 
                    ICommand command = this.userInterface.ReadLine();
                    string commandResult = this.engine.ExecuteCommand(command);
                    this.userInterface.WriteLine(commandResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }   
            }
        }
    }
}