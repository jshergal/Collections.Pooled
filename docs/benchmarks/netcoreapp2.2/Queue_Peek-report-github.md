``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|     Method |       N |   Type |        Mean |       Error |      StdDev |      Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------- |-------- |------- |------------:|------------:|------------:|------------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|  **QueuePeek** |   **10000** |    **Int** |    **53.30 us** |   **3.3853 us** |   **9.7132 us** |    **46.13 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledPeek |   10000 |    Int |    44.70 us |   0.0499 us |   0.0389 us |    44.72 us |  0.87 |    0.13 |           - |           - |           - |                   - |
|            |         |        |             |             |             |             |       |         |             |             |             |                     |
|  **QueuePeek** |   **10000** | **String** |    **25.34 us** |   **0.5009 us** |   **0.4441 us** |    **25.62 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledPeek |   10000 | String |    54.39 us |   3.5102 us |  10.1838 us |    54.91 us |  2.23 |    0.49 |           - |           - |           - |                   - |
|            |         |        |             |             |             |             |       |         |             |             |             |                     |
|  **QueuePeek** |  **100000** |    **Int** |   **489.04 us** |  **15.4096 us** |  **43.9646 us** |   **475.49 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledPeek |  100000 |    Int |   466.74 us |  10.9103 us |  30.5936 us |   445.36 us |  0.96 |    0.10 |           - |           - |           - |                   - |
|            |         |        |             |             |             |             |       |         |             |             |             |                     |
|  **QueuePeek** |  **100000** | **String** |   **236.09 us** |  **28.6948 us** |  **82.7911 us** |   **247.67 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledPeek |  100000 | String |   255.60 us |  40.2733 us | 115.5517 us |   249.70 us |  1.09 |    0.36 |           - |           - |           - |                   - |
|            |         |        |             |             |             |             |       |         |             |             |             |                     |
|  **QueuePeek** | **1000000** |    **Int** | **4,331.56 us** |  **60.5305 us** |  **47.2582 us** | **4,340.46 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledPeek | 1000000 |    Int | 2,479.89 us | 325.0091 us | 878.6810 us | 2,502.83 us |  0.74 |    0.19 |           - |           - |           - |                   - |
|            |         |        |             |             |             |             |       |         |             |             |             |                     |
|  **QueuePeek** | **1000000** | **String** | **2,358.70 us** |  **30.5432 us** |  **27.0758 us** | **2,349.68 us** |  **1.00** |    **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledPeek | 1000000 | String | 2,381.05 us |  51.6173 us |  61.4467 us | 2,352.80 us |  1.01 |    0.03 |           - |           - |           - |                   - |