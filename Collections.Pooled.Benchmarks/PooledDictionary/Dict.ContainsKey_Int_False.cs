using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [SimpleJob(RuntimeMoniker.Net472)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class Dict_ContainsKey_Int_False : DictContainsBase<int>
    {
        [Benchmark(Baseline = true)]
        public void DictContainsKey_Int_False()
        {
            bool result = false;
            int missingKey = N;   //The key N is not present in the dictionary.
            for (int j = 0; j < N; j++)
                result = dict.ContainsKey(missingKey);
        }

        [Benchmark]
        public void PooledContainsKey_Int_False()
        {
            bool result = false;
            int missingKey = N;   //The key N is not present in the dictionary.
            for (int j = 0; j < N; j++)
                result = pooled.ContainsKey(missingKey);
        }

        protected override int GetT(int i) => i;
    }
}
