using System;
using System.Collections.Generic;
using System.Text;
using Todo.Tasks;

namespace Todo.Commands
{
    internal class ModifyCommand : ICommand
    {
        public void Execute(string[] args, ref List<Task> tasks)
        {
            const string remove = "-", assignPerson = "@", setPriority = "!", setDueDate = "_", addTag = "#";

            if (args.Length == 0)
            {
                Console.WriteLine("Invalid argument.");
                return;
            }
            
            try
            {
                if (!int.TryParse(args[0], out var id)) throw new Exception();
                if (id >= tasks.Count) throw new Exception();

                Task task = tasks[id];
                var newTaskName = new StringBuilder();

                // MODIFY
                foreach (var arg in args)
                {
                    if (arg.StartsWith("-"))
                    {
                        if (arg[1..].StartsWith(assignPerson))
                        {
                            task.Unassign(arg[2..]);
                        }

                        else if (arg[1..].StartsWith(addTag))
                        {
                            task.RemoveTag(arg[2..]);
                        }

                        else
                        {
                            Console.WriteLine($"Wrong use of remove prefix. Use \"{remove}\" before \"{assignPerson}\" and \"{addTag}\" only.");
                            return;
                        }
                    }
                    else
                    {
                        if (arg.StartsWith(assignPerson))
                        {
                            task.AssignTo(arg[1..]);
                        }

                        else if (arg.StartsWith(addTag))
                        {
                            task.TagWith(arg[1..]);
                        }

                        else if (arg.StartsWith(setPriority))
                        {
                            if (int.TryParse(arg[1..], out var priority)) task.SetPriority(priority);

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

                                var dueDate = new DateTime(year, month, day, hour, minute, 0);
                                
                                task.DueTo(dueDate);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Wrong due date format. Use YYYY-MM-DD,HH:mm.");
                                return;
                            }

                        }

                        else
                        {
                            newTaskName.Append($"{arg} ");
                        }
                    }
                }

                if (!newTaskName.ToString().Equals(string.Empty)) task.Rename(newTaskName.ToString());

                Console.WriteLine($"Task, with id:{id}, has been modified.");
            }
            catch (Exception)
            {
                Console.WriteLine("Task not found.");
            }
        }
    }
}