# SequentialVsParallelSum
**Technologies**
- .NET 10
- BenchmarkDotNet

This project compares the performance of three summation algorithms:
- **Sequential Sum** - Traditional single-threaded approach
- **Parallel Thread Sum** - Multi-threaded implementation using `Thread` and `List<Thread>`
- **Parallel LINQ Sum** - Using PLINQ's `AsParallel()` and `Sum()` methods