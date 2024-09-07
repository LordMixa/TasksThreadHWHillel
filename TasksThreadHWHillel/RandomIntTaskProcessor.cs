namespace TasksThreadHWHillel
{
    class RandomIntTaskProcessor : RandomTaskProcessor<int>
    {
        public RandomIntTaskProcessor(int[] array, int threadCount, CancellationToken token)
            : base(array, threadCount, token)
        {
        }

        protected override void Process(int num)
        {
            if (_token.IsCancellationRequested) return;

            var itemsByThread = _array.Length / _taskCount;
            var start = num * itemsByThread;
            var end = num == _taskCount - 1 ? _array.Length : start + itemsByThread;

            var random = _randoms[num];

            for (int i = start; i < end; i++)
            {
                if (_token.IsCancellationRequested) return;
                _array[i] = random.Next(1, 1000000);
            }
        }
    }
}
