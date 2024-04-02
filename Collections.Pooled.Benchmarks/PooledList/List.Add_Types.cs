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
    public class List_Add_Types : ListBase
    {
        [Benchmark(Baseline = true)]
        public void ListAdd_ValueType()
        {
            var list = new List<int>();
            for (int j = 0; j < N; ++j)
            {
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
            }
        }

        [Benchmark]
        public void PooledAdd_ValueType()
        {
            var list = new PooledList<int>();
            for (int j = 0; j < N; ++j)
            {
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
                list.Add(j);
            }
            list.Dispose();
        }

        [Benchmark]
        public void ListAdd_ReferenceType()
        {
            var list = new List<string>();
            for (int j = 0; j < N; ++j)
            {
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
            }
        }

        [Benchmark]
        public void PooledAdd_ReferenceType()
        {
            var list = new PooledList<string>();
            for (int j = 0; j < N; ++j)
            {
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
            }
            list.Dispose();
        }

        [Params(256, 512, 2048)]
        public int N;

        private readonly string stringToAdd = "foo";
    }
}
