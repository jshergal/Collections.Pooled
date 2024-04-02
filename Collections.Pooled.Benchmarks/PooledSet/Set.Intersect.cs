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
    public class Set_Intersect : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_Intersect_Hashset()
        {
            hashSet.IntersectWith(hashSetToIntersect);
        }

        [Benchmark]
        public void PooledSet_Intersect_PooledSet()
        {
            pooledSet.IntersectWith(pooledSetToIntersect);
        }

        [Benchmark]
        public void HashSet_Intersect_Enum()
        {
            hashSet.IntersectWith(GetEnum());
        }

        [Benchmark]
        public void PooledSet_Intersect_Enum()
        {
            pooledSet.IntersectWith(GetEnum());
        }

        [Benchmark]
        public void HashSet_Intersect_Array()
        {
            hashSet.IntersectWith(stuffToIntersect);
        }

        [Benchmark]
        public void PooledSet_Intersect_Array()
        {
            pooledSet.IntersectWith(stuffToIntersect);
        }

        private IEnumerable<int> GetEnum()
        {
            for (int i = 0; i < stuffToIntersect.Length; i++)
            {
                yield return stuffToIntersect[i];
            }
        }

        private int[] startingElements;
        private int[] stuffToIntersect;
        private HashSet<int> hashSet;
        private HashSet<int> hashSetToIntersect;
        private PooledSet<int> pooledSet;
        private PooledSet<int> pooledSetToIntersect;

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
            stuffToIntersect = intGenerator.GenerateMixedSelection(startingElements, CountToIntersect);

            hashSet = new HashSet<int>();
            hashSetToIntersect = new HashSet<int>(stuffToIntersect);
            pooledSet = new PooledSet<int>();
            pooledSetToIntersect = new PooledSet<int>(stuffToIntersect);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            pooledSet?.Dispose();
            pooledSetToIntersect?.Dispose();
        }
    }
}