using System.Collections.Generic;
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
        foreach (var (projectName, project) in _projects)
        {
            console.WriteLine(projectName.ToString()!);
            project.PrintInto(console);
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

        project.Add(new Task(NextIdentifier, description));
    }

    public void MakeTaskDone(TaskIdentifier taskIdentifier)
    {
        foreach (var project in _projects.Values)
            project.MakeDoneIfExists(taskIdentifier);
    }

    public void MakeTaskUndone(TaskIdentifier taskIdentifier)
    {
        foreach (var project in _projects.Values)
            project.MakeUndoneIfExists(taskIdentifier);
    }
}