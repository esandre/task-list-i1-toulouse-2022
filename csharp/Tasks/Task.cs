using Tasks.Types;

namespace Tasks;

internal class Task
{
    public required TaskIdentifier Identifier { get; init; }

    public required TaskDescription Description { get; init; }

    public required Done Done { get; set; }
}