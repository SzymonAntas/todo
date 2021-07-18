#nullable enable
using System;
using Todo.Commands;

/* COMMAND FORMAT:
         * todo add Ask what to do now @JohnDoe _2020-07-18,12:22 !1 #management #hierarchy
         * [app] [command] [task] @[assignee|optional] _[due date|optional] ![priority|optional] #[tag|optional]
         * RETURN:
         * Created new task [id: 112]: "Ask what to do now", assigned to JohnDoe, due 2020-07-18 12:22
         *
         * todo list finished
         * [app] [command] [filter]
         * RETURN:
         * 
         *
         * todo done 112
         * [app] [command] [task_id]
         * RETURN:
         * Task "Ask what to do now" is now finished.
         *
         * todo reopen 112
         * [app] [command] [task_id] @[assignee|optional] _[due date|optional] ![priority|optional] #[tag|optional]
         * RETURN:
         * Task "Ask what to do now" has been reopened with previous due date and assignees.
         * 
         * todo remove 112
         * [app] [command] [task_id]
         * RETURN:
         * Task "Ask what to do now" has been removed completely.
         */

namespace Todo
{
    internal static class Program
    {

        private static void Main(string[] args)
        {
            // ARGS: [command] [task] @[assignee|optional] _[due date|optional] ![priority|optional] #[tag|optional]

            if (args.Length == 0)
            {
                Console.WriteLine("Missing command.");
                return;
            }

            var data = new Data();
            var context = new Context();
            
            data.LoadState();

            var commandName = args[0];
            var commandArgs = args[1..];

            switch (commandName)
            {
                case var c when c.Equals("add", StringComparison.InvariantCultureIgnoreCase):
                    context.SetCommand(new AddCommand());
                    break;
                    
                case var c when c.Equals("done", StringComparison.InvariantCultureIgnoreCase):
                    context.SetCommand(new DoneCommand());
                    break;
                
                case var c when c.Equals("reopen", StringComparison.InvariantCultureIgnoreCase):
                    context.SetCommand(new ReopenCommand());
                    break;
                
                case var c when c.Equals("remove", StringComparison.InvariantCultureIgnoreCase):
                    context.SetCommand(new RemoveCommand());
                    break;
                
                default:
                    Console.WriteLine($"Invalid Command \"{commandName}\".");
                    return;
            }
            
            context.HandleCommand(commandArgs, ref data.Tasks);

            data.SaveState();
            
            #if DEBUG
            Console.ReadKey();
            #endif
        }
    }
}