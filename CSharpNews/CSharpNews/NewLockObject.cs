public class CounterWithoutLock
{
    private int _count = 0;

    public void Increment() => _count++;


    public int GetCount() => _count;

    //When multiple tasks try to increment the same variable (_count) at the same time, they can interfere with each other.
    //This means some increments might be lost, so the final count could be lower than expected.
    public static void Demonstrate()
    {
        CounterWithoutLock counter = new CounterWithoutLock();
        const int taskCount = 1000;
        Task[] tasks = new Task[taskCount];
        for (int i = 0; i < taskCount; i++)
            tasks[i] = Task.Run(counter.Increment);
        Task.WaitAll(tasks);
        Console.WriteLine("Final count: " + counter.GetCount());
    }
}

public class CounterWithLock
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

    public static void Demonstrate()
    {
        CounterWithLock counter = new CounterWithLock();
        const int taskCount = 1000;
        Task[] tasks = new Task[taskCount];
        for (int i = 0; i < taskCount; i++)
            tasks[i] = Task.Run(counter.Increment);
        Task.WaitAll(tasks);
        Console.WriteLine("Final count: " + counter.GetCount());
    }
}
