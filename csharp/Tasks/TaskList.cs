﻿using System.Linq;
using System.Threading;
using Tasks.Types;

namespace Tasks;

public sealed class TaskList
{
    private const string Quit = "quit";

    private readonly Projects _projects = new ();
    private readonly IConsole _console;

    public TaskList(IConsole console)
    {
        _console = console;
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
        var commandParts = commandLine.Split(' ', 2);
        var commandVerb = commandParts.First();
        var commandRest = commandParts.Last();

        var command =
            commandVerb switch
            {
                "show"    => ConsoleCommand.Show,
                "add"     => ConsoleCommand.Add(commandRest),
                "check"   => ConsoleCommand.Check(commandRest),
                "uncheck" => ConsoleCommand.Uncheck(commandRest),
                "help"    => ConsoleCommand.Help,
                _         => ConsoleCommand.Error(commandVerb)
            };

        command.Execute(_projects, _console);
    }
}