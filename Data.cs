using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Todo.Tasks;

namespace Todo
{
    internal class Data
    {
        public List<Task> Tasks = new();
        
        private readonly string? _path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        
        public void LoadState()
        {
            try
            {
                Tasks.Clear();
                
                using var target = File.Open($@"{_path}\todo.txt", FileMode.OpenOrCreate);
                using var reader = new StreamReader(target);

                var line = reader.ReadLine();

                while (line != null)
                {
                    //"{ID} | {NAME} | {STATUS} |  {DUE_DATE} | {PRIORITY} | {assignees} | {tags}");

                    var elements = line.Split(" | ");
                    var assignees = new List<string>(elements[5].Split(","));
                    var tags = new List<string>(elements[6].Split(","));
                    DateTime? due = null;
                    if (!int.TryParse(elements[2], out var value)) throw new Exception("Status Error");
                    var status = (TaskStatus)value;

                    if (!int.TryParse(elements[4], out var priority)) throw new Exception("Priority Error");
                    if (DateTime.TryParse(elements[3], out var dueDate)) due = dueDate;
                    
                    Tasks.Add(new Task(elements[1], status, due, priority, assignees, tags));
                    
                    line = reader.ReadLine();
                }

                reader.Close();
            }
            catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        
        public void SaveState()
        {
            try
            {
                using var target = File.Create($@"{_path}\todo.txt");
                using var writer = new StreamWriter(target);

                for (var index = 0; index < Tasks.Count; index++)
                {
                    var assignees = new StringBuilder().AppendJoin(",", Tasks[index].Assignees);;
                    var tags = new StringBuilder().AppendJoin(",", Tasks[index].Tags);
                    writer.WriteLine($"{index} | {Tasks[index].Name} | {(int)Tasks[index].Status} | {Tasks[index].DueDate} | {Tasks[index].Priority} | {assignees} | {tags}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}