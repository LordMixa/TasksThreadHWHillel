namespace TasksThreadHWHillel
{
    abstract class ExtremumTaskProcessor : TaskProcessorBase<int>
    {
        protected int _extremumValue;
        protected Func<int, int, int> _comparisonFunc;

        public int ExtremumValue => _extremumValue;

        protected ExtremumTaskProcessor(int[] array, int threadCount, CancellationToken token, int initialValue, Func<int, int, int> comparisonFunc)
            : base(array, threadCount, token)
        {
            _extremumValue = initialValue;
            _comparisonFunc = comparisonFunc;
        }

        protected override void Process(int num)
        {
            if (_token.IsCancellationRequested) return;

            var span = GetWorkSpan(num);

            foreach (var value in span)
            {
                if (_token.IsCancellationRequested) return;
                int oldValue, newValue;
                do
                {
                    oldValue = _extremumValue;
                    newValue = _comparisonFunc(oldValue, value);
                } while (Interlocked.CompareExchange(ref _extremumValue, newValue, oldValue) != oldValue);
            }
        }
    }
}
