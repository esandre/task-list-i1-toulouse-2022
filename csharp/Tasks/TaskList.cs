using System.Threading;
using Tasks.Types;

namespace Tasks;

public sealed class TaskList
{
    private const string Quit = "quit";

    private readonly Projects _projects = new ();
    private readonly IConsole _console;

    private TaskIdentifier _lastIdentifier;

    public static void Main()
    {
        new TaskList(new RealConsole()).Run(CancellationToken.None);
    }

    public TaskList(IConsole console)
    {
        this._console = console;
        this._lastIdentifier = TaskIdentifier.First();
    }

    public void Run(CancellationToken token)
    {
        while (RunOnce())
        {
            token.ThrowIfCancellationRequested();
        }
    }

    private bool RunOnce()
    {
        _console.Write("> ");
        var command = _console.ReadLine();
        if (command == Quit) return false;

        Execute(command);

        return true;
    }

    private void Execute(string commandLine)
    {
        var commandRest = commandLine.Split(" ".ToCharArray(), 2);
        var command = commandRest[0];
        switch (command) {
            case "show":
                Show();
                return;
            case "add":
                Add(commandRest[1]);
                return;
            case "check":
                Check(TaskIdentifier.Parse(commandRest[1]));
                return;
            case "uncheck":
                Uncheck(TaskIdentifier.Parse(commandRest[1]));
                return;
            case "help":
                Help();
                return;
        }

        Error(command);
    }

    private void Show() => _projects.PrintInto(_console);

    private void Add(string commandLine)
    {
        var subcommandRest = commandLine.Split(" ".ToCharArray(), 2);
        var subcommand = subcommandRest[0];

        if (subcommand == "project") {
            AddProject(new ProjectName(subcommandRest[1]));
            return;
        }
            
        if (subcommand == "task") {
            var projectTask = subcommandRest[1].Split(" ".ToCharArray(), 2);
            AddTask(new ProjectName(projectTask[0]), projectTask[1]);
        }
    }

    private void AddProject(ProjectName name) => _projects.Add(name);

    private void AddTask(ProjectName project, string description)
    {
        _projects.AddTaskToProject(project,
            new Task { Identifier = _lastIdentifier, Description = description, Done = Done.No },
            _console
        );

        _lastIdentifier = _lastIdentifier.Next();
    }

    private void Check(TaskIdentifier taskIdentifier)
    {
        SetDone(taskIdentifier, Done.Yes);
    }

    private void Uncheck(TaskIdentifier taskIdentifier)
    {
        SetDone(taskIdentifier, Done.No);
    }

    private void SetDone(TaskIdentifier taskIdentifier, Done done) 
        => _projects.SetTaskDone(taskIdentifier, done, _console);

    private void Help()
    {
        _console.WriteLine("Commands:");
        _console.WriteLine("  show");
        _console.WriteLine("  add project <project name>");
        _console.WriteLine("  add task <project name> <task description>");
        _console.WriteLine("  check <task ID>");
        _console.WriteLine("  uncheck <task ID>");
        _console.WriteLine();
    }

    private void Error(string command)
    {
        _console.WriteLine($"I don't know what the command \"{command}\" is.");
    }
}