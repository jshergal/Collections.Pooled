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
    public class List_AddRange : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListAddRangeICollection()
        {
            for (int i = 0; i < 5000; i++)
            {
                var emptyList = new List<int>();
                emptyList.AddRange(list);
            }
        }

        [Benchmark]
        public void PooledAddRangeICollection()
        {
            for (int i = 0; i < 5000; i++)
            {
                var emptyList = new PooledList<int>();
                emptyList.AddRange(list);
                emptyList.Dispose();
            }
        }

        [Benchmark]
        public void ListAddRangeIEnumerable()
        {
            for (int i = 0; i < 5000; i++)
            {
                var emptyList = new List<int>();
                emptyList.AddRange(IntEnumerable());
            }
        }

        [Benchmark]
        public void PooledAddRangeIEnumerable()
        {
            for (int i = 0; i < 5000; i++)
            {
                var emptyList = new PooledList<int>();
                emptyList.AddRange(IntEnumerable());
                emptyList.Dispose();
            }
        }

        private IEnumerable<int> IntEnumerable()
        {
            for (int i = 0; i < N; i++)
                yield return list[i];
        }

        private List<int> list;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            list = CreateList(N);
        }
    }
}
