using System.Collections.Generic;
using Todo.Commands;

namespace Todo.Tasks
{
    internal class Context
    {
        private ICommand _command;
        private List<Task> _tasks;

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