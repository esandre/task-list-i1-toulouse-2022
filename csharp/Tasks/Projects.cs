using System.Collections.Generic;
using System.Linq;
using Tasks.Types;

namespace Tasks;

internal class Projects
{
    private readonly IDictionary<ProjectName, Project> _projects = new Dictionary<ProjectName, Project>();
    private TaskIdentifier _lastIdentifier = TaskIdentifier.First();

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

    private TaskIdentifier NextIdentifier
    {
        get
        {
            var identifier = _lastIdentifier;
            _lastIdentifier = _lastIdentifier.Next();
            return identifier;
        }
    }

    public void AddTaskToProject(ProjectName projectName, TaskDescription description, IConsole console)
    {
        if (!_projects.TryGetValue(projectName, out var project))
        {
            console.WriteLine($"Could not find a project with the name \"{projectName}\".");
            return;
        }

        project.Add(new Task { Description = description, Done = Done.No, Identifier = NextIdentifier });
    }

    public void SetTaskDone(TaskIdentifier taskIdentifier, Done done, IConsole console)
    {
        foreach (var project in _projects.Values)
            project.SetDoneIfExists(taskIdentifier, done, console);
    }
}