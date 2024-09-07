namespace TasksThreadHWHillel
{
    class MaxTaskProcessor : TaskProcessorBase<int>
    {
        private int _maxValue = int.MinValue;
        public int MaxValue => _maxValue;

        public MaxTaskProcessor(int[] array, int threadCount, CancellationToken token)
            : base(array, threadCount, token)
        {
        }

        protected override void Process(int num)
        {
            if (_token.IsCancellationRequested) return;

            var itemsByThread = _array.Length / _taskCount;
            var span = num == _taskCount - 1
                ? _array[(num * itemsByThread)..]
                : _array.AsSpan(num * itemsByThread, itemsByThread);

            foreach (var value in span)
            {
                if (_token.IsCancellationRequested) return;

                int oldValue, newValue;
                do
                {
                    oldValue = _maxValue;
                    newValue = Math.Max(oldValue, value);
                } while (Interlocked.CompareExchange(ref _maxValue, newValue, oldValue) != oldValue);
            }
        }
    }
}
