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
    public class Dict_GetKeys : DictBase
    {
        [Benchmark(Baseline = true)]
        public void DictGetKeys()
        {
            IEnumerable<int> result;
            for (int i = 0; i <= 20000; i++)
            {
                result = dict.Keys; result = dict.Keys; result = dict.Keys;
                result = dict.Keys; result = dict.Keys; result = dict.Keys;
                result = dict.Keys; result = dict.Keys; result = dict.Keys;
            }
        }

        [Benchmark]
        public void PooledGetKeys()
        {
            IEnumerable<int> result;
            for (int i = 0; i <= 20000; i++)
            {
                result = pooled.Keys; result = pooled.Keys; result = pooled.Keys;
                result = pooled.Keys; result = pooled.Keys; result = pooled.Keys;
                result = pooled.Keys; result = pooled.Keys; result = pooled.Keys;
            }
        }

        private PooledDictionary<int, int> pooled;
        private Dictionary<int, int> dict;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            pooled = CreatePooled(N);
            dict = CreateDictionary(N);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooled?.Dispose();
        }
    }
}
