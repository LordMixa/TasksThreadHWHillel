using System.Diagnostics;

namespace TasksThreadHWHillel
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.WriteLine("Run int generator!");
            var array = new int[100_000_000];

            Console.Write("Enter number of threads: ");
            int threadCount = int.Parse(Console.ReadLine());

            var randomProc = new RandomIntTaskProcessor(array, threadCount, cts.Token);
            await randomProc.Run();

            var sw = Stopwatch.StartNew();
            var minProcessor = new MinTaskProcessor(array, threadCount, cts.Token);
            await minProcessor.Run();
            Console.WriteLine($"Min: {minProcessor.ExtremumValue}");

            var maxProcessor = new MaxTaskProcessor(array, threadCount, cts.Token);
            await maxProcessor.Run();
            Console.WriteLine($"Max: {maxProcessor.ExtremumValue}");

            var sumProcessor = new SumTaskProcessor(array, threadCount, cts.Token);
            await sumProcessor.Run();
            Console.WriteLine($"Sum: {sumProcessor.SumValue}");

            var avgProcessor = new AverageTaskProcessor(array, threadCount, cts.Token);
            await avgProcessor.Run();
            Console.WriteLine($"Average: {avgProcessor.AverageValue}");

            string[] words = new string[1000000];
            var wordGenerator = new RandomWordTaskProcessor(words, 4, cts.Token);
            await wordGenerator.Run();

            var charFrequencyProcessor = new CharFrequencyTaskProcessor(words, threadCount, cts.Token);
            await charFrequencyProcessor.Run();
            Console.WriteLine($"Character frequency: {string.Join(", ", charFrequencyProcessor.Frequency.Select(kv => $"{kv.Key}: {kv.Value}"))}");
            Console.WriteLine("\n\n\n");

            var wordFrequencyProcessor = new WordFrequencyTaskProcessor(words, threadCount, cts.Token);
            await wordFrequencyProcessor.Run();
            Console.WriteLine($"Word frequency: {string.Join(", ", wordFrequencyProcessor.Frequency.Select(kv => $"{kv.Key}: {kv.Value}"))}");

            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
        }
    }
}
