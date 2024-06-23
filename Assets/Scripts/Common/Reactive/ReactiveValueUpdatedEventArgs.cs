namespace Common.Reactive
{
    public struct ReactiveValueUpdatedEventArgs
    {
        public readonly int after;
        public readonly int before;
        public readonly int max;

        public ReactiveValueUpdatedEventArgs(int before, int after, int max)
        {
            this.before = before;
            this.after = after;
            this.max = max;
        }
    }
}