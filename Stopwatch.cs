using System;
using System.Threading;

public class stopwatch
{
    public delegate void CounterEventHandler(string notification);

    public event CounterEventHandler OnStarted;
    public event CounterEventHandler OnStopped;
    public event CounterEventHandler OnCleared;

    private TimeSpan elapsedDuration;
    private bool counterActive;
    private Thread counterThread;

    public stopwatch()
    {
        elapsedDuration = TimeSpan.Zero;
        counterActive = false;
    }

    public void StartCounter()
    {
        if (counterActive)
        {
            Console.WriteLine("Counter is already active.");
            return;
        }

        counterActive = true;
        OnStarted?.Invoke("Counter has been started.");

        // Run the ticking in a separate thread
        counterThread = new Thread(() =>
        {
            while (counterActive)
            {
                Thread.Sleep(1000);
                IncrementTime();
                Console.WriteLine($"Time Elapsed: {elapsedDuration}");
            }
        });

        counterThread.Start();
    }

    public void StopCounter()
    {
        if (!counterActive)
        {
            Console.WriteLine("Counter is not currently active.");
            return;
        }

        counterActive = false;

        // Wait for the thread to finish
        counterThread?.Join();

        OnStopped?.Invoke($"Counter has been stopped at {elapsedDuration.TotalSeconds} seconds.");
    }

    public void ResetCounter()
    {
        StopCounter(); // Stop the counter first if it's running
        elapsedDuration = TimeSpan.Zero;
        OnCleared?.Invoke("Counter has been reset.");
    }

    private void IncrementTime()
    {
        elapsedDuration = elapsedDuration.Add(TimeSpan.FromSeconds(1));
    }
}



