using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Collections.Generic;

namespace Collections.Pooled.Benchmarks.PooledStack
{
    [SimpleJob(RuntimeMoniker.Net472)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [MemoryDiagnoser]
    public class Stack_Push : StackBase
    {
        [Benchmark(Baseline = true)]
        public void StackPush()
        {
            if (Type == StackType.Int)
            {
                var stack = new Stack<int>();
                for (int i = 0; i < N; i++)
                {
                    stack.Push(intArray[i]);
                }
            }
            else
            {
                var stack = new Stack<string>();
                for (int i = 0; i < N; i++)
                {
                    stack.Push(stringArray[i]);
                }
            }
        }

        [Benchmark]
        public void PooledPush()
        {
            if (Type == StackType.Int)
            {
                var stack = new PooledStack<int>();
                for (int i = 0; i < N; i++)
                {
                    stack.Push(intArray[i]);
                }
                stack.Dispose();
            }
            else
            {
                var stack = new PooledStack<string>();
                for (int i = 0; i < N; i++)
                {
                    stack.Push(stringArray[i]);
                }
                stack.Dispose();
            }
        }

        [Params(1_000, 10_000, 100_000)]
        public int N;

        [Params(StackType.Int, StackType.String)]
        public StackType Type;

        private int[] intArray;
        private string[] stringArray;

        [GlobalSetup]
        public void GlobalSetup()
        {
            intArray = CreateArray(N);

            stringArray = new string[N];
            for (int i = 0; i < N; i++)
            {
                stringArray[i] = intArray[i].ToString();
            }
        }
    }
}
