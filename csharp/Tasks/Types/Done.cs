namespace Tasks.Types
{
    internal class Done
    {
        private readonly char _representation;

        public static readonly Done Yes = new ('x');
        public static readonly Done No = new (' ');
        
        private Done(char representation)
        {
            _representation = representation;
        }

        /// <inheritdoc />
        public override string ToString() => _representation.ToString();
    }
}
