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

    public int GetCount()
    {
        lock (_lock)
        {
            return _count;
        }
    }
}

public class CounterWithNewLock
{
    private int _count = 0;
    private readonly Lock _lock = new Lock();

    public void Increment()
    {
        using (_lock.EnterScope())
        {
            _count++;
        }
    }

    public int GetCount()
    {
        using (_lock.EnterScope())
        {
            return _count;
        }
    }
}

[MemoryDiagnoser]
public class BenchmarkTest
{
    private CounterWithOldLock _counterOldLock;
    private CounterWithNewLock _counterNewLock;
    const int totalIterations = 1000000;

    [GlobalSetup]
    public void Setup()
    {
        _counterOldLock = new CounterWithOldLock();
        _counterNewLock = new CounterWithNewLock();
    }

    [Benchmark]
    public void TestOldLock()
    {
        for (int i = 0; i < totalIterations; i++)
        {
            _counterOldLock.Increment();
        }
    }

    [Benchmark]
    public void TestNewLock()
    {
        for (int i = 0; i < totalIterations; i++)
        {
            _counterNewLock.Increment();
        }
    }
}