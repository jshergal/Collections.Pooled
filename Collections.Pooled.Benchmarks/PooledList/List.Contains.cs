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
    public class List_Contains : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListContains()
        {
            for (int i = 0; i < 500; i++)
            {
                list.Contains(containedList); list.Contains(containedList); list.Contains(containedList); list.Contains(containedList);
                list.Contains(containedList); list.Contains(containedList); list.Contains(containedList); list.Contains(containedList);
                list.Contains(containedList); list.Contains(containedList); list.Contains(containedList); list.Contains(containedList);
            }
        }

        [Benchmark]
        public void PooledContains()
        {
            for (int i = 0; i < 500; i++)
            {
                pooled.Contains(containedPooled); pooled.Contains(containedPooled); pooled.Contains(containedPooled); pooled.Contains(containedPooled);
                pooled.Contains(containedPooled); pooled.Contains(containedPooled); pooled.Contains(containedPooled); pooled.Contains(containedPooled);
                pooled.Contains(containedPooled); pooled.Contains(containedPooled); pooled.Contains(containedPooled); pooled.Contains(containedPooled);
            }
        }

        private List<int> list;
        private PooledList<int> pooled;
        private int containedList;
        private int containedPooled;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            list = CreateList(N);
            pooled = CreatePooled(N);
            containedList = list[list.Count / 2];
            containedPooled = pooled[pooled.Count / 2];
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
