using System;
using System.Collections.Generic;
using System.Text;
using Todo.Tasks;

namespace Todo.Commands
{
    internal class AddCommand : ICommand
    {
     public void Execute(string[] args, ref List<Task> tasks)
        {
            const string assignPerson = "@", setPriority = "!", setDueDate = "_", addTag = "#";

            if (args.Length == 0)
            {
                Console.WriteLine("Invalid arguments.");
                return;
            }

            var taskName = new StringBuilder();
            var assignees = new List<string>();
            var tags = new List<string>();
            var priority = 0;
            DateTime? dueDate = null;

            foreach (var arg in args)
            {
                if (arg.StartsWith(assignPerson)) 
                {
                    assignees.Add(arg[1..]);
                }
                
                else if (arg.StartsWith(addTag)) 
                {
                    tags.Add(arg[1..]);
                }
                
                else if (arg.StartsWith(setPriority))
                {
                    if (int.TryParse(arg[1..], out priority)) continue;
                    
                    Console.WriteLine("Priority level must be a number.");
                    return;
                }
                
                else if (arg.StartsWith(setDueDate)) 
                {
                    try
                    {
                        var dateTime = arg[1..].Split(",");
                        if (dateTime.Length != 2) throw new Exception();

                        var date = dateTime[0].Split("-");
                        if (date.Length != 3) throw new Exception();

                        var time = dateTime[1].Split(":");
                        if (time.Length != 2) throw new Exception();

                        if (!int.TryParse(date[0], out var year)
                            || !int.TryParse(date[1], out var month)
                            || !int.TryParse(date[2], out var day)
                            || !int.TryParse(time[0], out var hour)
                            || !int.TryParse(time[1], out var minute)) 
                            throw new Exception();
                        
                        dueDate = new DateTime(year, month, day, hour, minute, 0);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Wrong due date format. Use YYYY-MM-DD,HH:mm.");
                        return;
                    }
                }

                else
                {
                    taskName.Append($"{arg} ");
                }
            }

            if (taskName.ToString().Equals(string.Empty))
            {
                Console.WriteLine("Missing task name.");
                return;
            }

            var task = new Task(taskName.ToString().TrimEnd(), TaskStatus.Todo, dueDate, priority, assignees, tags);
            tasks.Add(task);

            var message = new StringBuilder();
            
            message.Append($"Added a new task \"{task.Name}\" ");
            message.Append($"(id: {tasks.Count - 1})");
            if (task.DueDate != null) message.Append($" due {task.DueDate.ToString()}");
            if (task.Assignees.Count > 0) message.Append(", assigned to:");
            foreach (var assignee in task.Assignees)
            {
                message.Append($" {assignee},");
            }
            message.Append($" with priority level {task.Priority}");

            Console.WriteLine(message.ToString());
        }
    }
}