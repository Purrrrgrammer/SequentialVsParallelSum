using BenchmarkDotNet.Attributes;

namespace SequentialVsParallelSum;

public class SumBenchMark
{
    [Params(100_000, 1_000_000, 10_000_000)]
    public int N { get; set; }

    private long[] _numbers;
    
    [GlobalSetup]
    public void Setup()
    {
        var rand = new Random();
        _numbers = Enumerable
            .Range(1, N)
            .Select(x => (long)rand.Next(0, int.MaxValue))
            .ToArray();
    }
    
    [Benchmark(Description = "Sequential")]
    public long SumSequential()
    {
        long sum = 0;
        for (int i = 0; i < _numbers.Length; i++)
            sum += _numbers[i];
        
        return sum;
    }
    
    [Benchmark(Description = "PLINQ")]
    public long SumWithPLINQ()
    {
        return _numbers
            .AsParallel()
            .WithDegreeOfParallelism(Environment.ProcessorCount)
            .Sum(x => x);
    }
    
    [Benchmark(Description = "Thread")]
    public long SumWithThread()
    {
        List<Thread> threads = new List<Thread>();
        var degreeOfParallelism = Environment.ProcessorCount;
        var chunkSize = _numbers.Length/degreeOfParallelism;
        int maxValue = _numbers.Length;
        var chunkSums = new long[degreeOfParallelism];
        
        for (int i = 0; i < degreeOfParallelism; i++)
        {
            int sumIndex = i;
            int begin = i * chunkSize;
            int end = i * chunkSize + chunkSize;
            
            Thread thread = new Thread(() =>
            {
                for (int j = begin; j < end && j < maxValue; j++)
                    chunkSums[sumIndex] += _numbers[j];
            });
            
            threads.Add(thread);
            thread.Start();
        }

        foreach (var thread in threads)
            thread.Join();
        
        return chunkSums.Sum();
    }
}