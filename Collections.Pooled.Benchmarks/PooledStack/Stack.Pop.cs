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
    public class Stack_Pop : StackBase
    {
        [Benchmark(Baseline = true)]
        public void StackPop()
        {
            if (Type == StackType.Int)
            {
                int result;
                for (int i = 0; i < N; i++)
                {
                    result = intStack.Pop();
                }
            }
            else
            {
                string result;
                for (int i = 0; i < N; i++)
                {
                    result = stringStack.Pop();
                }
            }
        }

        [Benchmark]
        public void PooledPop()
        {
            if (Type == StackType.Int)
            {
                int result;
                for (int i = 0; i < N; i++)
                {
                    result = intPooled.Pop();
                }
            }
            else
            {
                string result;
                for (int i = 0; i < N; i++)
                {
                    result = stringPooled.Pop();
                }
            }
        }

        [Params(10000, 100000, 1000000)]
        public int N;

        [Params(StackType.Int, StackType.String)]
        public StackType Type;

        private Stack<int> intStack;
        private Stack<string> stringStack;
        private PooledStack<int> intPooled;
        private PooledStack<string> stringPooled;
        private int[] numbers;
        private string[] strings;

        [GlobalSetup]
        public void GlobalSetup()
        {
            numbers = CreateArray(N);

            if (Type == StackType.String)
            {
                strings = Array.ConvertAll(numbers, x => x.ToString());
            }
        }

        [IterationSetup]
        public void IterationSetup()
        {
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
            intPooled?.Dispose();
            stringPooled?.Dispose();
        }
    }
}
