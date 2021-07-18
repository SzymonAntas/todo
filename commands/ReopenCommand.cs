using System;
using System.Collections.Generic;
using Todo.Tasks;

namespace Todo.Commands
{
    public class ReopenCommand : ICommand
    {
        public void Execute(string[] args, ref List<Task> tasks)
        {
            try
            {
                if (!int.TryParse(args[0], out var id)) throw new Exception();
                if (id >= tasks.Count) throw new Exception();

                tasks[id].ChangeStatus(TaskStatus.Todo);

                Console.WriteLine($"Task \"{tasks[id].Name}\" (id:{id}) has been reopened.");
            }
            catch (Exception)
            {
                Console.WriteLine("Task not found.");
            }
        }
    }
}