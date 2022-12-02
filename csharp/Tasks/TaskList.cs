using System.Linq;
using System.Threading;
using Tasks.Types;

namespace Tasks;

public sealed class TaskList
{
    private const string Quit = "quit";

    private readonly Projects _projects = new ();
    private readonly IConsole _console;

    public static void Main()
    {
        new TaskList(new RealConsole()).Run(CancellationToken.None);
    }

    public TaskList(IConsole console)
    {
        this._console = console;
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
        var commandRest = commandLine.Split(' ', 2);
        var commandVerb = commandRest[0];

        var command =
            commandVerb switch
            {
                "show"    => ConsoleCommand.Show,
                "add"     => ConsoleCommand.Add(commandRest.Last()),
                "check"   => ConsoleCommand.Check(commandRest.Last()),
                "uncheck" => ConsoleCommand.Uncheck(commandRest.Last()),
                "help"    => ConsoleCommand.Help,
                _         => ConsoleCommand.Error(commandVerb)
            };

        command.Execute(_projects, _console);
    }
}