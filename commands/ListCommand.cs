using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todo.Tasks;

namespace Todo.Commands
{
    public class ListCommand : ICommand
    {
        public void Execute(string[] args, ref List<Task> tasks)
        {
            // done
            // !done
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

                Console.WriteLine(
                    $"{i}. {task.Name}, due: {due}, priority: {task.Priority}, assigned to: {assignees}, tags: {tags}");
            }
            
            // TODO: overdue, !overdue
        }
    }
}