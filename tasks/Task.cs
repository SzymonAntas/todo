using System;
using System.Collections.Generic;

namespace Todo.Tasks
{
    public class Task
    {
        public string Name {get; private set; }
        public DateTime? DueDate { get; private set; }
        public int Priority { get; private set; }
        public List<string> Assignees { get; }
        public List<string> Tags { get; }
        public TaskStatus Status { get; private set; }
        
        public Task(string taskName, TaskStatus status, DateTime? dueDate, int priority, List<string> assignees, List<string> tags)
        {
            Name = taskName;
            DueDate = dueDate;
            Priority = priority;
            Assignees = assignees;
            Tags = tags;
            Status = status;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeStatus(TaskStatus status)
        {
            Status = status;
        }

        public void AssignTo(string person)
        {
            Assignees.Add(person);
        }

        public void Unassign(string person)
        {
            Assignees.Remove(person);
        }

        public void TagWith(string tag)
        {
            Tags.Add(tag);
        }

        public void RemoveTag(string tag)
        {
            Tags.Remove(tag);
        }

        public void DueTo(DateTime due)
        {
            DueDate = due;
        }
        
        public void SetPriority(int priority)
        {
            Priority = priority;
        }
    }
}