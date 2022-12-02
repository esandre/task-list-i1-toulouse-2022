using Tasks.Types;

namespace Tasks;

internal class Task
{
    public TaskIdentifier Identifier { get; init; }

    public TaskDescription Description { get; init; }

    public Done Done { get; set; }
}