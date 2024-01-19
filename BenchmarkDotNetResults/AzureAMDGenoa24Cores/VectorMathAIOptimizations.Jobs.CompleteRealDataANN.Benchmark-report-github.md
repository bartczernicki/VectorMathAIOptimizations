```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.20348.2227) (Hyper-V)
AMD EPYC 9V33X, 2 CPU, 24 logical and 24 physical cores
.NET SDK 8.0.200-preview.23624.5
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-OYRCPM : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Runtime=.NET 8.0  RunStrategy=Throughput  

```
| Method              | Mean        | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------- |------------:|----------:|----------:|---------:|--------:|-
| Complete            | 107.2623 ms | 2.1008 ms | 4.0476 ms | baseline |         | 
| CompleteRealDataANN |   0.6250 ms | 0.0015 ms | 0.0013 ms |   -99.4% |    4.8% | 
