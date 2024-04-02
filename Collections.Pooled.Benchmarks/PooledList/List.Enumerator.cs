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
    public class List_Enumerator : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListEnumerate_Int()
        {
            int item;
            foreach (int x in listInt)
            {
                item = x;
            }
        }

        [Benchmark]
        public void PooledEnumerate_Int()
        {
            int item;
            foreach (int x in pooledInt)
            {
                item = x;
            }
        }

        [Benchmark]
        public void PooledEnumerateSpan_Int()
        {
            int item;
            foreach (int x in pooledInt.Span)
            {
                item = x;
            }
        }

        [Benchmark]
        public void ListEnumerate_String()
        {
            string item;
            foreach (string x in listString)
            {
                item = x;
            }
        }

        [Benchmark]
        public void PooledEnumerate_String()
        {
            string item;
            foreach (string x in pooledString)
            {
                item = x;
            }
        }

        [Benchmark]
        public void PooledEnumerateSpan_String()
        {
            string item;
            foreach (string x in pooledString.Span)
            {
                item = x;
            }
        }

        private List<int> listInt;
        private List<string> listString;
        private PooledList<int> pooledInt;
        private PooledList<string> pooledString;

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [GlobalSetup]
        public void GlobalSetup()
        {
            listInt = CreateList(N);
            listString = listInt.ConvertAll(i => i.ToString());
            pooledInt = new PooledList<int>(listInt);
            pooledString = pooledInt.ConvertAll(i => i.ToString());
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledInt?.Dispose();
            pooledString?.Dispose();
        }
    }
}
