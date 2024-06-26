using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Collections.Generic;

namespace Collections.Pooled.Benchmarks.PooledSet
{
    [SimpleJob(RuntimeMoniker.Net472)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [MemoryDiagnoser]
    public class Set_SymmetricExcept : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_SymmetricExcept_Hashset()
        {
            hashSet.SymmetricExceptWith(hashSetToExcept);
        }

        [Benchmark]
        public void PooledSet_SymmetricExcept_PooledSet()
        {
            pooledSet.SymmetricExceptWith(pooledSetToExcept);
        }

        [Benchmark]
        public void HashSet_SymmetricExcept_Enum()
        {
            hashSet.SymmetricExceptWith(GetEnum());
        }

        [Benchmark]
        public void PooledSet_SymmetricExcept_Enum()
        {
            pooledSet.SymmetricExceptWith(GetEnum());
        }

        [Benchmark]
        public void HashSet_SymmetricExcept_Array()
        {
            hashSet.SymmetricExceptWith(stuffToExcept);
        }

        [Benchmark]
        public void PooledSet_SymmetricExcept_Array()
        {
            pooledSet.SymmetricExceptWith(stuffToExcept);
        }

        private IEnumerable<int> GetEnum()
        {
            for (int i = 0; i < stuffToExcept.Length; i++)
            {
                yield return stuffToExcept[i];
            }
        }

        private int[] startingElements;
        private int[] stuffToExcept;
        private HashSet<int> hashSet;
        private HashSet<int> hashSetToExcept;
        private PooledSet<int> pooledSet;
        private PooledSet<int> pooledSetToExcept;

        [Params(MaxStartSize, SetSize_Small)]
        public int CountToIntersect;

        [Params(SetSize_Large)]
        public int InitialSetSize;

        [IterationSetup]
        public void IterationSetup()
        {
            hashSet.UnionWith(startingElements);
            pooledSet.UnionWith(startingElements);
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            hashSet.Clear();
            pooledSet.Clear();
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var intGenerator = new RandomTGenerator<int>(InstanceCreators.IntGenerator);
            startingElements = intGenerator.MakeNewTs(InitialSetSize);
            stuffToExcept = intGenerator.GenerateMixedSelection(startingElements, CountToIntersect);

            hashSet = new HashSet<int>();
            hashSetToExcept = new HashSet<int>(stuffToExcept);
            pooledSet = new PooledSet<int>();
            pooledSetToExcept = new PooledSet<int>(stuffToExcept);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledSet?.Dispose();
            pooledSetToExcept?.Dispose();
        }
    }
}