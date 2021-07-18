using System.Collections.Generic;
using Todo.Tasks;

namespace Todo.Commands
{
    internal interface ICommand
    {
        void Execute(string[] args, ref List<Task> tasks);
    }
}