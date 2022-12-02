using Tasks.Types;

namespace Tasks;

internal class Task
{
    public TaskIdentifier Identifier { get; init; }

    public string Description { get; init; }

    public Done Done { get; set; }
}