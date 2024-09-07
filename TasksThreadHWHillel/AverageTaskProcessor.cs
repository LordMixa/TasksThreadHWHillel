namespace TasksThreadHWHillel
{
    class AverageTaskProcessor : TaskProcessorBase<int>
    {
        private long _sumValue;  
        private readonly int _count;
        public double AverageValue { get; private set; }

        public AverageTaskProcessor(int[] array, int threadCount, CancellationToken token)
            : base(array, threadCount, token)
        {
            _count = array.Length;
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

        public override async Task Run()
        {
            await base.Run();
            AverageValue = (double)_sumValue / _count;
        }
    }
}
