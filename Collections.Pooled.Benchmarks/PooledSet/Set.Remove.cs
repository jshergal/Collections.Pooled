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
    public class Set_Remove : SetBase
    {
        [Benchmark(Baseline = true)]
        public void HashSet_Remove()
        {
            foreach (int thing in stuffToRemove)
            {
                hashSet.Remove(thing);
            }
        }

        [Benchmark]
        public void PooledSet_Remove()
        {
            foreach (int thing in stuffToRemove)
            {
                pooledSet.Remove(thing);
            }
        }

        private int[] startingElements;
        private int[] stuffToRemove;
        private HashSet<int> hashSet;
        private PooledSet<int> pooledSet;

        [Params(1, 100, 10000)]
        public int CountToRemove;

        [Params(SetSize_Large)]
        public int InitialSetSize;

        [IterationSetup(Target = nameof(HashSet_Remove))]
        public void HashIterationSetup()
        {
            hashSet = new HashSet<int>(startingElements);
        }

        [IterationSetup(Target = nameof(PooledSet_Remove))]
        public void PooledIterationSetup()
        {
            pooledSet = new PooledSet<int>(startingElements);
        }

        [IterationCleanup(Target = nameof(PooledSet_Remove))]
        public void PooledIterationCleanup()
        {
            pooledSet?.Dispose();
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var intGenerator = new RandomTGenerator<int>(InstanceCreators.IntGenerator);
            startingElements = intGenerator.MakeNewTs(InitialSetSize);
            stuffToRemove = intGenerator.GenerateSelectionSubset(startingElements, CountToRemove);
        }
    }
}
