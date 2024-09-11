namespace TasksThreadHWHillel
{
    class AverageTaskProcessor : SumTaskProcessor
    {
        private readonly int _count;
        public double AverageValue { get; private set; }

        public AverageTaskProcessor(int[] array, int threadCount, CancellationToken token)
            : base(array, threadCount, token)
        {
            _count = array.Length;
        }

        protected override void Process(int num)
        {
            if (_token.IsCancellationRequested) return;
            base.Process(num);
            AverageValue = (double)SumValue / _count;
        }
    }
}
