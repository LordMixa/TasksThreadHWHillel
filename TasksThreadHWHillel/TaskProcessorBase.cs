namespace TasksThreadHWHillel
{
    abstract class TaskProcessorBase<T>
    {
        protected readonly T[] _array;
        protected readonly int _taskCount;
        protected readonly CancellationToken _token;

        public TaskProcessorBase(T[] array, int taskCount, CancellationToken token)
        {
            _array = array;
            _taskCount = taskCount;
            _token = token;
        }

        public virtual Task Run()
        {
            var tasks = new Task[_taskCount];
            for (int i = 0; i < tasks.Length; i++)
            {
                var num = i;
                tasks[i] = Task.Run(() => Process(num), _token);
            }

            return Task.WhenAll(tasks);
        }
        protected Span<T> GetWorkSpan(int num)
        {
            var itemsByThread = _array.Length / _taskCount;
            return num == _taskCount - 1
                ? _array[(num * itemsByThread)..]
                : _array.AsSpan(num * itemsByThread, itemsByThread);
        }
        protected abstract void Process(int num);
    }
}
