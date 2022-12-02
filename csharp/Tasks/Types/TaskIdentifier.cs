using System;

namespace Tasks.Types
{
    internal class TaskIdentifier : IEquatable<string>
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
        public bool Equals(string? identifierAsString) 
            => _identifier.Equals(long.Parse(identifierAsString ?? throw new NotSupportedException()));

        /// <inheritdoc />
        public override string ToString() 
            => _identifier.ToString();
    }
}
