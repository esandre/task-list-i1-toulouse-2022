using System.Collections.Generic;
using System.Linq;
using Tasks.Types;

namespace Tasks;

internal class Project
{
    private readonly IList<Task> _tasks = new List<Task>();
    
    public void Add(Task task) => _tasks.Add(task);

    public void PrintInto(IConsole console)
    {
        foreach (var task in _tasks)
            task.PrintInto(console);
    }

    public void MakeDoneIfExists(TaskIdentifier identifier)
    {
        var identifiedTask = _tasks
            .FirstOrDefault(task => task.Equals(identifier));

        identifiedTask?.MakeDone();
    }

    public void MakeUndoneIfExists(TaskIdentifier identifier)
    {
        var identifiedTask = _tasks
            .FirstOrDefault(task => task.Equals(identifier));

        identifiedTask?.MakeUndone();
    }
}