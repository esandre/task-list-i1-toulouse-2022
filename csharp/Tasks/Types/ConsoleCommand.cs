using System;

namespace Tasks.Types;

internal class ConsoleCommand
{
    private readonly string _verb;
    private readonly Action<Projects, IConsole> _action;

    private ConsoleCommand(string verb, Action<Projects, IConsole> action)
    {
        _verb = verb;
        _action = action;
    }

    public void Execute(Projects projects, IConsole console) 
        => _action(projects, console);

    /// <inheritdoc />
    public override string ToString() => _verb;

    public static readonly ConsoleCommand Show = new ("show",
        (projects, console) => projects.PrintInto(console)
    );

    public static ConsoleCommand Error(string command)
        => new (command,
            (_, console) => console.WriteLine($"I don't know what the command \"{command}\" is.")
        );

    public static ConsoleCommand Check(string argument)
        => new("check",
            (projects, console) => projects.SetTaskDone(TaskIdentifier.Parse(argument), Done.Yes, console)
        );

    public static ConsoleCommand Uncheck(string argument)
        => new("uncheck",
            (projects, console) => projects.SetTaskDone(TaskIdentifier.Parse(argument), Done.No, console)
        );

    public static ConsoleCommand Add(string argument)
        => new("uncheck",
            (projects, console) =>
            {
                var subcommandRest = argument.Split(' ', 2);
                var subcommand = subcommandRest[0];

                if (subcommand == "project")
                {
                    projects.Add(new ProjectName(subcommandRest[1]));
                    return;
                }

                if (subcommand == "task")
                {
                    var projectTask = subcommandRest[1].Split(' ', 2);

                    var projectName = new ProjectName(projectTask[0]);
                    var description = new TaskDescription(projectTask[1]);

                    projects.AddTaskToProject(projectName, description, console);
                }
            }
        );

    public static ConsoleCommand Help
        => new("help",
            (_, console) =>
            {
                console.WriteLine("Commands:");
                console.WriteLine("  show");
                console.WriteLine("  add project <project name>");
                console.WriteLine("  add task <project name> <task description>");
                console.WriteLine("  check <task ID>");
                console.WriteLine("  uncheck <task ID>");
                console.WriteLine();
            }
        );
}