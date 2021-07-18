using System.Collections.Generic;
using Todo.Commands;
using Todo.Tasks;

namespace Todo.Commands
{
    internal class Context
    {
        private ICommand _command;
        private List<Task> _tasks = new();

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public void HandleCommand(string[] args)
        {
            _command.Execute(args, ref _tasks);
        }
    }
}