namespace TasksThreadHWHillel
{
    class MaxTaskProcessor : ExtremumTaskProcessor
    {
        public MaxTaskProcessor(int[] array, int threadCount, CancellationToken token)
            : base(array, threadCount, token, int.MinValue, (x, y) => Math.Max(x, y))
        {
        }
    }
}
