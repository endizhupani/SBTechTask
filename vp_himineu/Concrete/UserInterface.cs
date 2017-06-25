namespace Vp_himineu.Concrete
{
    using System;
    using Abstract;
    using VehicleParkEngine.Commands;

    public class UserInterface : IUserInterface
    {
        public ICommand ReadLine()
        {
            string commandLine = Console.ReadLine();
            if (commandLine == null)
            {
                throw new ArgumentNullException("Please enter a command");
            }

            commandLine.Trim();
            if (string.IsNullOrEmpty(commandLine))
            {
                throw new ArgumentNullException("Please enter a valid command");
            }

            return CommandFactory.GetCommand(commandLine);
        }
            
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
