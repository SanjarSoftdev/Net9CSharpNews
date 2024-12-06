public static class NewLockObject
{
    public static void Demonstrate()
    {
        var counter = new Counter();

        Parallel.For(0, 1000, _ => counter.Increment());

        Console.WriteLine($"{counter.GetCount()} (expected: 1000)");

        // 1000 (expected: 1000)
    }
}
public class CounterWithoutLock
{
    private int _count = 0;
    public void Increment() => _count++;
    public int GetCount() => _count;
}

public class Counter
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
    public int GetCount() => _count;
}