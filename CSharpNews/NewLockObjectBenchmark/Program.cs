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

public class BenchmarkTest
{
    private CounterWithOldLock _counterOldLock;
    private CounterWithNewLock _counterNewLock;

    [GlobalSetup]
    public void Setup()
    {
        _counterOldLock = new CounterWithOldLock();
        _counterNewLock = new CounterWithNewLock();
    }

    [Benchmark]
    [Arguments(1)] // Single task
    [Arguments(4)] // 4 tasks
    [Arguments(8)] // 8 tasks
    [Arguments(16)] // 16 tasks
    public async Task TestOldLock(int tasks)
    {
        var taskList = new Task[tasks];
        for (int i = 0; i < tasks; i++)
        {
            taskList[i] = Task.Run(() =>
            {
                for (int j = 0; j < 10000 / tasks; j++)
                {
                    _counterOldLock.Increment();
                }
            });
        }

        // Wait for all tasks to complete
        await Task.WhenAll(taskList);
    }

    [Benchmark]
    [Arguments(1)] // Single task
    [Arguments(4)] // 4 tasks
    [Arguments(8)] // 8 tasks
    [Arguments(16)] // 16 tasks
    public async Task TestNewLock(int tasks)
    {
        var taskList = new Task[tasks];
        for (int i = 0; i < tasks; i++)
        {
            taskList[i] = Task.Run(() =>
            {
                for (int j = 0; j < 10000 / tasks; j++)
                {
                    _counterNewLock.Increment();
                }
            });
        }

        // Wait for all tasks to complete
        await Task.WhenAll(taskList);
    }
}