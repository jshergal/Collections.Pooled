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
    public class Dict_Constructors : DictBase
    {
        [Benchmark(Baseline = true)]
        public void Dict_Ctor()
        {
            for (int i = 0; i <= 500; i++)
            {
                new Dictionary<int, string>(N); new Dictionary<int, string>(N); new Dictionary<int, string>(N);
                new Dictionary<int, string>(N); new Dictionary<int, string>(N); new Dictionary<int, string>(N);
                new Dictionary<int, string>(N); new Dictionary<int, string>(N); new Dictionary<int, string>(N);
            }
        }

        [Benchmark]
        public void Pooled_Ctor()
        {
            for (int i = 0; i <= 500; i++)
            {
                new PooledDictionary<int, string>(N).Dispose(); new PooledDictionary<int, string>(N).Dispose(); new PooledDictionary<int, string>(N).Dispose();
                new PooledDictionary<int, string>(N).Dispose(); new PooledDictionary<int, string>(N).Dispose(); new PooledDictionary<int, string>(N).Dispose();
                new PooledDictionary<int, string>(N).Dispose(); new PooledDictionary<int, string>(N).Dispose(); new PooledDictionary<int, string>(N).Dispose();
            }
        }

        [Params(0, 1024, 4096, 16384)]
        public int N;
    }
}
