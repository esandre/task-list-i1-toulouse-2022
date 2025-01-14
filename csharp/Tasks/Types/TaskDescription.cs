﻿namespace Tasks.Types;

internal class TaskDescription
{
    private readonly string _description;

    public TaskDescription(string description)
    {
        _description = description;
    }

    /// <inheritdoc />
    public override string ToString() => _description;

    public void PrintInto(IConsole console) => console.Write(_description);
}