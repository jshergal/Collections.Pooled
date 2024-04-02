using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;

namespace Collections.Pooled.Benchmarks.PooledDictionary
{
    [SimpleJob(RuntimeMoniker.Net472)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class Dict_ContainsKey_String_False_IgnoreCase : DictContainsBase<string>
    {
        [Benchmark(Baseline = true)]
        public void DictContainsKey_String_False_IgnoreCase()
        {
            bool result = false;
            string missingKey = N.ToString();   //The value N is not present in the dictionary.
            for (int j = 0; j < N; j++)
                result = dict.ContainsKey(missingKey);
        }

        [Benchmark]
        public void PooledContainsKey_String_False_IgnoreCase()
        {
            bool result = false;
            string missingKey = N.ToString();   //The value N is not present in the dictionary.
            for (int j = 0; j < N; j++)
                result = pooled.ContainsKey(missingKey);
        }

        protected override string GetT(int i) => i.ToString();

        protected override IEqualityComparer<string> Comparer
            => StringComparer.OrdinalIgnoreCase;
    }
}
