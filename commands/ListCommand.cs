using System;
using System.Collections.Generic;
using System.Text;
using Todo.Tasks;

namespace Todo.Commands
{
    public class ListCommand : ICommand
    {
        public void Execute(string[] args, ref List<Task> tasks)
        {
            // done & !done
            if (tasks.Count == 0)
            {
                Console.WriteLine("Task list is empty.");
                return;
            }
            
            for (var i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                
                if (args.Length != 0 &&
                    (!args[0].Equals("done", StringComparison.InvariantCultureIgnoreCase) ||
                     task.Status != TaskStatus.Done) &&
                    (!args[0].Equals("!done", StringComparison.InvariantCultureIgnoreCase) ||
                     task.Status != TaskStatus.Todo)) continue;
                
                var assignees = task.Assignees.Count > 0
                    ? new StringBuilder().AppendJoin(", ", task.Assignees).ToString()
                    : "none";
                
                var tags = task.Tags.Count > 0
                    ? new StringBuilder().AppendJoin(", ", task.Tags).ToString()
                    : "none";

                var due = task.DueDate != null ? task.DueDate.ToString()! : "unknown";

                Console.WriteLine("\n- - -");
                Console.WriteLine($"{i}. [{task.Status.ToString().ToUpper()}] \"{task.Name}\"");
                Console.WriteLine($"due: {due}");
                Console.WriteLine($"priority: {task.Priority}");
                Console.WriteLine($"assigned to: {assignees}");
                Console.WriteLine($"tags: {tags}\n");
            }
            
            // TODO: overdue, !overdue
        }
    }
}