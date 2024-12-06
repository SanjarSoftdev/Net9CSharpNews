using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<BenchmarkTest>();

public class CounterWithOldLock
{
    private int _count = 0;
    private readonly object _lock = new object();
    public void Increment()
    {
        lock (_lock)
        {
            _count++;
        }
    }
    public int GetCount() => _count;
}
public class CounterWithNewLock
{
    private int _count = 0;
    private readonly Lock _newLock = new Lock();
    public void Increment()
    {
        using (var scope = _newLock.EnterScope())
        {
            _count++;
        }
    }
    public int GetCount() => _count;
}

[MemoryDiagnoser]
public class BenchmarkTest
{
    const int iteration = 100;
    const int operationPerTask = 100;


    [Benchmark]
    public int TestOldLock()
    {
        var counterOldLock = new CounterWithOldLock();
        Parallel.For(0, iteration, _ =>
        {
            for (int i = 0; i < operationPerTask; i++)
            {
                counterOldLock.Increment();
            }
        });
        return counterOldLock.GetCount();
    }


    [Benchmark]
    public int TestNewLock()
    {
        var counterNewLock = new CounterWithNewLock();
        Parallel.For(0, iteration, _ =>
        {
            for (int i = 0; i < operationPerTask; i++)
            {
                counterNewLock.Increment();
            }
        });
        return counterNewLock.GetCount();
    }
}