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
    public class List_Indexer_Types : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListIndexer_Int()
        {
            int item;
            for (int j = 0; j < N; ++j)
            {
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
                item = listInt[j];
            }
        }

        [Benchmark]
        public void PooledIndexer_Int()
        {
            int item;
            for (int j = 0; j < N; ++j)
            {
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
                item = pooledInt[j];
            }
        }

        [Benchmark]
        public void PooledIndexer_Span_Int()
        {
            int item;
            var span = pooledInt.Span;
            for (int j = 0; j < N; ++j)
            {
                item = span[j];
                item = span[j];
                item = span[j];
                item = span[j];
                item = span[j];
                item = span[j];
                item = span[j];
                item = span[j];
                item = span[j];
                item = span[j];
            }
        }

        [Benchmark]
        public void ListIndexer_String()
        {
            string item;
            for (int j = 0; j < N; ++j)
            {
                item = listString[j];
                item = listString[j];
                item = listString[j];
                item = listString[j];
                item = listString[j];
                item = listString[j];
                item = listString[j];
                item = listString[j];
                item = listString[j];
                item = listString[j];
            }
        }

        [Benchmark]
        public void PooledIndexer_String()
        {
            string item;
            for (int j = 0; j < N; ++j)
            {
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
                item = pooledString[j];
            }
        }

        [Benchmark]
        public void PooledIndexer_Span_String()
        {
            string item;
            var span = pooledString.Span;
            for (int j = 0; j < N; ++j)
            {
                item = span[j];
                item = span[j];
                item = span[j];
                item = span[j];
                item = span[j];
                item = span[j];
                item = span[j];
                item = span[j];
                item = span[j];
                item = span[j];
            }
        }

        private List<int> listInt;
        private List<string> listString;
        private PooledList<int> pooledInt;
        private PooledList<string> pooledString;

        private const int N = 8192;

        [GlobalSetup]
        public void GlobalSetup()
        {
            listInt = CreateList(N);
            listString = listInt.ConvertAll(i => i.ToString());
            pooledInt = new PooledList<int>(listInt);
            pooledString = new PooledList<string>(listString);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledInt?.Dispose();
            pooledString?.Dispose();
        }
    }
}
