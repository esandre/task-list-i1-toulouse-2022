using System;

namespace Tasks.Types;

internal class ProjectName : IEquatable<ProjectName>
{
    private readonly string _name;

    public ProjectName(string name)
    {
        _name = name;
    }

    /// <inheritdoc />
    public override string ToString() => _name;

    /// <inheritdoc />
    public bool Equals(ProjectName? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _name == other._name;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ProjectName)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode() => _name.GetHashCode();
}