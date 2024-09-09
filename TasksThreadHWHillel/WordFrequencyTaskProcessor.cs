namespace TasksThreadHWHillel
{
    using System.Collections.Concurrent;

    class WordFrequencyTaskProcessor : TaskProcessorBase<string>
    {
        public ConcurrentDictionary<string, int> Frequency { get; private set; } = new ConcurrentDictionary<string, int>();

        public WordFrequencyTaskProcessor(string[] array, int threadCount, CancellationToken token)
            : base(array, threadCount, token)
        {
        }

        protected override void Process(int num)
        {
            if (_token.IsCancellationRequested) return;

            var itemsByThread = _array.Length / _taskCount;
            var span = GetWorkSpan(num);

            foreach (var word in span)
            {
                if (_token.IsCancellationRequested) return;

                Frequency.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);
            }
        }
    }
}
