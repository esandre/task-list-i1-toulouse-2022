using System;
using Tasks.Types;

namespace Tasks;

internal class Task : IEquatable<TaskIdentifier>
{
    public Task(TaskIdentifier identifier, TaskDescription description)
    {
        _identifier = identifier;
        _data = new TaskData(description, Done.No);
    }

    private readonly TaskIdentifier _identifier;
    private readonly TaskData _data;

    public void MakeUndone() => _data.MakeUndone();
    public void MakeDone() => _data.MakeDone();

    public void PrintInto(IConsole console)
    {
        console.Write("    [");
        _data.PrintDoneInto(console);
        console.Write($"] {_identifier}: ");
        _data.PrintDescriptionInto(console);
        console.WriteLine();
    }

    /// <inheritdoc />
    public bool Equals(TaskIdentifier? other) 
        => _identifier.Equals(other);
}