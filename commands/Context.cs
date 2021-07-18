using System.Collections.Generic;
using Todo.Tasks;

namespace Todo.Commands
{
    internal class Context
    {
        private ICommand? _command;

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public void HandleCommand(string[] args, ref List<Task> tasks)
        {
            _command?.Execute(args, ref tasks);
        }
    }
}