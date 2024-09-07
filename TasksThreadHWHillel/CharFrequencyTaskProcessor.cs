namespace TasksThreadHWHillel
{
    class CharFrequencyTaskProcessor : TaskProcessorBase<char>
    {
        public Dictionary<char, int> Frequency { get; private set; } = new Dictionary<char, int>();

        public CharFrequencyTaskProcessor(char[] array, int threadCount, CancellationToken token)
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

            foreach (var ch in span)
            {
                if (_token.IsCancellationRequested) return;

                lock (Frequency)
                {
                    if (Frequency.ContainsKey(ch))
                    {
                        Frequency[ch]++;
                    }
                    else
                    {
                        Frequency[ch] = 1;
                    }
                }
            }
        }
    }
}
