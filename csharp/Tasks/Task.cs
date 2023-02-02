using Tasks.Types;

namespace Tasks;

internal class Task
{
    public Task(TaskIdentifier identifier, TaskDescription description)
    {
        Identifier = identifier;
        _data = new TaskData(description, Done.No);
    }

    public TaskIdentifier Identifier { get; }
    private readonly TaskData _data;

    public void MakeUndone()
    {
        _data.MakeUndone();
    }

    public void MakeDone()
    {
        _data.MakeDone();
    }

    public void PrintInto(IConsole console)
    {
        console.Write("    [");
        _data.PrintDoneInto(console);
        console.Write($"] {Identifier}: ");
        _data.PrintDescriptionInto(console);
        console.WriteLine();
    }
}