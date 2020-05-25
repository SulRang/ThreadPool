using System;
using System.Threading;
using System.Diagnostics;

public class ThreadPool
{
    public static void ThreadProc()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("ThreadProc{0}: {1}",Thread.CurrentThread.Name, i);
            Thread.Sleep(0);
        }
    }
    private static void ExecuteInForeground()
    {
        DateTime start = DateTime.Now;
        var sw = Stopwatch.StartNew();
        Console.WriteLine("Thread {0}: {1}, Priority {2}",
                          Thread.CurrentThread.ManagedThreadId,
                          Thread.CurrentThread.ThreadState,
                          Thread.CurrentThread.Priority);
        do
        {
            Console.WriteLine("Thread {0}: Elapsed {1:N2} seconds",
                              Thread.CurrentThread.ManagedThreadId,
                              sw.ElapsedMilliseconds / 1000.0);
            Thread.Sleep(500);
        } while (sw.ElapsedMilliseconds <= 5000);
        sw.Stop();
    }

    public static void Main()
    {
        Console.WriteLine("Main thread: Start a second thread.");
        Thread t1 = new Thread(new ThreadStart(ThreadPool.ThreadProc));
        Thread t2 = new Thread(new ThreadStart(ThreadPool.ExecuteInForeground));
        t1.Name = "Thread1";
        t1.Start();
        t2.Name = "Thread2";
        t2.Start();
        Thread.Sleep(1000);
        Console.WriteLine("Main thread ({0}) exiting...",
                          Thread.CurrentThread.ManagedThreadId);

        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine("Main thread: Do some work.");
            Thread.Sleep(0);
        }

        Console.WriteLine("Main thread: Call Join(), to wait until ThreadProc1 ends.");
        t1.Join();
        Console.WriteLine("Main thread: ThreadProc.Join has returned.  Press Enter to end program.");
        Console.ReadLine();
    }
}