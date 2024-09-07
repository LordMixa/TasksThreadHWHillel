namespace TasksThreadHWHillel
{
    class WordFrequencyTaskProcessor : TaskProcessorBase<string>
    {
        public Dictionary<string, int> Frequency { get; private set; } = new Dictionary<string, int>();

        public WordFrequencyTaskProcessor(string[] array, int threadCount, CancellationToken token)
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

            foreach (var word in span)
            {
                if (_token.IsCancellationRequested) return;

                lock (Frequency)
                {
                    if (Frequency.ContainsKey(word))
                    {
                        Frequency[word]++;
                    }
                    else
                    {
                        Frequency[word] = 1;
                    }
                }
            }
        }
    }
}
