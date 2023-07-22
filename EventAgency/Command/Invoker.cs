using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAgency.Command
{
    public class Invoker
    {
        private List<IEventCommand> _commands = new List<IEventCommand>();

        public void AddCommand(IEventCommand command)
        {
            _commands.Add(command);
        }

        public void Run()
        {
            foreach (var command in _commands)
            {
                command.Execute();
            }
            _commands.Clear();
        }
    }
}
