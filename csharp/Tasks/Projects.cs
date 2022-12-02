using System.Collections.Generic;
using System.Linq;
using Tasks.Types;

namespace Tasks;

internal class Projects
{
    private readonly IDictionary<ProjectName, Project> _projects = new Dictionary<ProjectName, Project>();

    public void Add(ProjectName name)
    {
        _projects.Add(name, new Project());
    }

    public void PrintInto(IConsole console)
    {
        foreach (var project in _projects)
        {
            console.WriteLine(project.Key.ToString()!);
            project.Value.PrintInto(console);
            console.WriteLine();
        }
    }

    public void AddTaskToProject(ProjectName projectName, Task task, IConsole console)
    {
        if (!_projects.TryGetValue(projectName, out var project))
        {
            console.WriteLine($"Could not find a project with the name \"{projectName}\".");
            return;
        }

        project.Add(task);
    }

    public void SetTaskDone(TaskIdentifier taskIdentifier, Done done, IConsole console)
    {
        foreach (var project in _projects.Values)
            project.SetDoneIfExists(taskIdentifier, done, console);
    }
}