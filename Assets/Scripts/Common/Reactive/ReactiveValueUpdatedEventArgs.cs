namespace Common.Reactive
{
    public struct ReactiveValueUpdatedEventArgs
    {
        public readonly float after;
        public readonly float before;
        public readonly float max;

        public ReactiveValueUpdatedEventArgs(float before, float after, float max)
        {
            this.before = before;
            this.after = after;
            this.max = max;
        }
    }
}