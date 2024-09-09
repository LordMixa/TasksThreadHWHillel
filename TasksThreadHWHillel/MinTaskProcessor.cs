namespace TasksThreadHWHillel
{
    class MinTaskProcessor : ExtremumTaskProcessor
    {
        public MinTaskProcessor(int[] array, int threadCount, CancellationToken token)
            : base(array, threadCount, token, int.MaxValue, (x, y) => Math.Min(x, y))
        {
        }
    }
}
