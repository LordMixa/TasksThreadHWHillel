namespace TasksThreadHWHillel
{
    class SumTaskProcessor : TaskProcessorBase<int>
    {
        private long _sumValue;
        public long SumValue => _sumValue;

        public SumTaskProcessor(int[] array, int threadCount, CancellationToken token)
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

            long localSum = 0; 
            foreach (var value in span)
            {
                if (_token.IsCancellationRequested) return;
                localSum += value;
            }

            Interlocked.Add(ref _sumValue, localSum);
        }
    }
}
