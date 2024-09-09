namespace TasksThreadHWHillel
{
    class RandomWordTaskProcessor : RandomTaskProcessor<string>
    {
        public RandomWordTaskProcessor(string[] array, int taskCount, CancellationToken token)
            : base(array, taskCount, token)
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

                int wordLength = random.Next(3, 6);
                _array[i] = GenerateRandomWord(wordLength, random);
            }
        }

        private string GenerateRandomWord(int length, Random random)
        {
            char[] word = new char[length];
            for (int i = 0; i < length; i++)
            {
                word[i] = (char)random.Next('a', 'z' + 1); 
            }
            return new string(word);
        }
    }
}
