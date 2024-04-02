using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Collections.Generic;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [SimpleJob(RuntimeMoniker.Net472)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [MemoryDiagnoser]
    public class Dict_Remove : DictBase
    {
        [Benchmark(Baseline = true)]
        public void DictRemove()
        {
            for (long i = 0; i < N; i++)
            {
                dict.Remove(items[i]);
            }
        }

        [Benchmark]
        public void PooledRemove()
        {
            for (long i = 0; i < N; i++)
            {
                pooled.Remove(items[i]);
            }
        }

        private PooledDictionary<long?, long?> pooled;
        private Dictionary<long?, long?> dict;
        private long?[] items;

        [Params(10, 100, 10000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            dict = new Dictionary<long?, long?>(N);
            pooled = new PooledDictionary<long?, long?>(N);
            items = new long?[N];

            for (long i = 0; i < N; ++i)
            {
                items[i] = i;
                dict.Add(i, i);
                pooled.Add(i, i);
            }
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
