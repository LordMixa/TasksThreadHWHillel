namespace TasksThreadHWHillel
{
    abstract class RandomTaskProcessor<T> : TaskProcessorBase<T>
    {
        protected readonly Random[] _randoms;

        protected RandomTaskProcessor(T[] array, int taskCount, CancellationToken token)
            : base(array, taskCount, token)
        {
            var random = new Random();
            _randoms = new Random[taskCount];
            for (int i = 0; i < taskCount; i++)
            {
                _randoms[i] = new Random(random.Next());
            }
        }
    }
}
