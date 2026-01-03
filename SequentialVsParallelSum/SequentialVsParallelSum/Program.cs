using System;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Running;
using SequentialVsParallelSum;

namespace SequentialVsParallelSum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // SumBenchMark sumBenchMark = new SumBenchMark() { N = 100000};
            // sumBenchMark.Setup();
            //
            // Console.WriteLine($"sequential {sumBenchMark.SumSequential()}");
            // Console.WriteLine($"PLINQ {sumBenchMark.SumWithPLINQ()}");
            // Console.WriteLine($"Thread {sumBenchMark.SumWithThread()}");
            
            BenchmarkRunner.Run<SumBenchMark>();

            Console.ReadKey();
        }
    }
}