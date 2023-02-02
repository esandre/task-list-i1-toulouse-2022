namespace Tasks.Types;

internal class TaskData
{
    private readonly TaskDescription _description;
    private Done _status;

    public TaskData(TaskDescription description, Done status)
    {
        _description = description;
        _status = status;
    }

    public void MakeUndone() => _status = Done.No;

    public void MakeDone() => _status = Done.Yes;

    public void PrintDoneInto(IConsole console) => _status.PrintInto(console);

    public void PrintDescriptionInto(IConsole console) => _description.PrintInto(console);
}