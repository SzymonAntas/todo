using System;
using System.Collections.Generic;
using Todo.Tasks;

namespace Todo.Commands
{
    internal class DoneCommand : ICommand
    {
        public void Execute(string[] args, ref List<Task> tasks)
        {
            try
            {
                if (!int.TryParse(args[0], out var id)) throw new Exception("ID not a number.");
                if (id >= tasks.Count) throw new Exception("ID too big.");

                tasks[id].ChangeStatus(TaskStatus.Done);

                Console.WriteLine($"Task \"{tasks[id].Name}\" (id:{id}) has been finished. Current status: {tasks[id].Status.ToString().ToUpper()}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Task not found. {e.Message}");
            }
        }
    }
}