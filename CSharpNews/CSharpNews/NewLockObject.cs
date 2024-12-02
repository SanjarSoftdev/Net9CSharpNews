public static class NewLockObject
{
    public static void Demonstrate()
    {
        CounterWithNewLock counter = new CounterWithNewLock();
        const int taskCount = 1000;
        Task[] tasks = new Task[taskCount];
        for (int i = 0; i < taskCount; i++)
            tasks[i] = Task.Run(counter.Increment);
        Task.WaitAll(tasks);
        Console.WriteLine("Final count: " + counter.GetCount());
    }
}
public class CounterWithoutLock
{
    private int _count = 0;

    public void Increment() => _count++;

    public int GetCount() => _count;
}

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