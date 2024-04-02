using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;

namespace Collections.Pooled.Benchmarks.PooledStack
{
    [SimpleJob(RuntimeMoniker.Net472)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [MemoryDiagnoser]
    public class Stack_TryPeek : StackBase
    {
        [Benchmark(Baseline = true)]
        public void StackTryPeek()
        {
            if (Type == StackType.Int)
            {
                int result;
                bool found;
                for (int i = 0; i <= N; i++)
                {
                    found = intStack.TryPeek(out result);
                }
            }
            else
            {
                string result;
                bool found;
                for (int i = 0; i <= N; i++)
                {
                    found = stringStack.TryPeek(out result);
                }
            }
        }

        [Benchmark]
        public void PooledTryPeek()
        {
            if (Type == StackType.Int)
            {
                int result;
                bool found;
                for (int i = 0; i <= N; i++)
                {
                    found = intPooled.TryPeek(out result);
                }
            }
            else
            {
                string result;
                bool found;
                for (int i = 0; i <= N; i++)
                {
                    found = stringPooled.TryPeek(out result);
                }
            }
        }

        [Params(10000, 100000, 1000000)]
        public int N;

        [Params(StackType.Int, StackType.String)]
        public StackType Type;

        [Params(true, false)]
        public bool EmptyStack;

        private Stack<int> intStack;
        private Stack<string> stringStack;
        private PooledStack<int> intPooled;
        private PooledStack<string> stringPooled;
        private int[] numbers;
        private string[] strings;

        [GlobalSetup]
        public void GlobalSetup()
        {
            if (EmptyStack)
            {
                intStack = new Stack<int>();
                stringStack = new Stack<string>();
                intPooled = new PooledStack<int>();
                stringPooled = new PooledStack<string>();
            }

            numbers = CreateArray(N);

            if (Type == StackType.String)
            {
                strings = Array.ConvertAll(numbers, x => x.ToString());
            }
        }

        [IterationSetup]
        public void IterationSetup()
        {
            if (EmptyStack)
                return;

            if (Type == StackType.Int)
            {
                intStack = new Stack<int>(numbers);
                intPooled = new PooledStack<int>(numbers);
            }
            else
            {
                stringStack = new Stack<string>(strings);
                stringPooled = new PooledStack<string>(strings);
            }
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            if (EmptyStack)
                return;

            intPooled?.Dispose();
            stringPooled?.Dispose();
        }
    }
}
