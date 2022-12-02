using System.Collections.Generic;
using System.Linq;
using Tasks.Types;

namespace Tasks
{
    internal class Project
    {
        private readonly IList<Task> _tasks = new List<Task>();

        public void Add(Task task)
        {
            _tasks.Add(task);
        }

        public void PrintInto(IConsole console)
        {
            foreach (var task in _tasks)
            {
                console.WriteLine($"    [{task.Done}] {task.Identifier}: {task.Description}");
            }
        }

        public void SetDoneIfExists(string identifier, Done done, IConsole console)
        {
            var identifiedTask = _tasks
                .FirstOrDefault(task => task.Identifier.Equals(identifier));

            if (identifiedTask != null) identifiedTask.Done = done;
        }
    }
}
