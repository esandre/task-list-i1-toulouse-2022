using System;

namespace Tasks.Types;

internal class TaskIdentifier : IEquatable<TaskIdentifier>
{
    public static TaskIdentifier First() => new (1);

    private readonly long _identifier;

    private TaskIdentifier(long identifier)
    {
        _identifier = identifier;
    }

    public TaskIdentifier Next() 
        => new TaskIdentifier(_identifier + 1);
    
    /// <inheritdoc />
    public override string ToString() 
        => _identifier.ToString();

    public static TaskIdentifier Parse(string identifierAsString)
        => new (long.Parse(identifierAsString));

    /// <inheritdoc />
    public bool Equals(TaskIdentifier? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _identifier == other._identifier;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TaskIdentifier)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode() => _identifier.GetHashCode();

    public static bool operator ==(TaskIdentifier? left, TaskIdentifier? right) => Equals(left, right);
    public static bool operator !=(TaskIdentifier? left, TaskIdentifier? right) => !Equals(left, right);
}