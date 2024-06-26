using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [SimpleJob(RuntimeMoniker.Net472)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class Dict_ContainsKey_Int_True : DictContainsBase<int>
    {
        [Benchmark(Baseline = true)]
        public void DictContainsKey_Int_True()
        {
            bool result = false;
            for (int j = 0; j < N; j++)
                result = dict.ContainsKey(j);
        }

        [Benchmark]
        public void PooledContainsKey_Int_True()
        {
            bool result = false;
            for (int j = 0; j < N; j++)
                result = pooled.ContainsKey(j);
        }

        protected override int GetT(int i) => i;
    }
}
