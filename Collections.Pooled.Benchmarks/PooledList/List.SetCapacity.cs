using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Collections.Generic;

namespace Collections.Pooled.Benchmarks.PooledList
{
    [SimpleJob(RuntimeMoniker.Net472)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [MemoryDiagnoser]
    public class List_Capacity : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListSetCapacity()
        {
            for (int i = 0; i < 100; i++)
            {
                // Capacity set back and forth between size+1 and size+2
                list.Capacity = N + (i % 2) + 1;
            }
        }

        [Benchmark]
        public void PooledSetCapacity()
        {
            for (int i = 0; i < 100; i++)
            {
                // Capacity set back and forth between size+1 and size+2
                pooled.Capacity = N + (i % 2) + 1;
            }
        }

        private List<int> list;
        private PooledList<int> pooled;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            list = CreateList(N);
            pooled = CreatePooled(N);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
