using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Linq;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [SimpleJob(RuntimeMoniker.Net472)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class Dict_ContainsKey_String_True : DictContainsBase<string>
    {
        [Benchmark(Baseline = true)]
        public void DictContainsKey_String_True()
        {
            bool result = false;
            for (int j = 0; j < N; j++)
                result = dict.ContainsKey(sampleKeys[j]);
        }

        [Benchmark]
        public void PooledContainsKey_String_True()
        {
            bool result = false;
            for (int j = 0; j < N; j++)
                result = pooled.ContainsKey(sampleKeys[j]);
        }

        protected override string GetT(int i) => i.ToString();

        private string[] sampleKeys;

        public override void GlobalSetup()
        {
            base.GlobalSetup();
            sampleKeys = dict.Keys.ToArray();
        }
    }
}
