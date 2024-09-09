namespace TasksThreadHWHillel
{
    using System.Collections.Concurrent;

    class CharFrequencyTaskProcessor : TaskProcessorBase<string>
    {
        public ConcurrentDictionary<char, int> Frequency { get; private set; } = new ConcurrentDictionary<char, int>();

        public CharFrequencyTaskProcessor(string[] array, int threadCount, CancellationToken token)
            : base(array, threadCount, token)
        {
        }
        protected override void Process(int num)
        {
            if (_token.IsCancellationRequested) return;

            var itemsByThread = _array.Length / _taskCount;
            var span = GetWorkSpan(num);

            foreach (var str in span)
            {
                foreach (var ch in str)
                {
                    if (_token.IsCancellationRequested) return;

                    Frequency.AddOrUpdate(ch, 1, (key, oldValue) => oldValue + 1);
                }
            }
        }
    }

}
