using System;

public class Program
{
    public static void Main(string[] args)
    {
        stopwatch counter = new stopwatch();

        counter.OnStarted += notification => Console.WriteLine(notification);
        counter.OnStopped += notification => Console.WriteLine(notification);
        counter.OnCleared += notification => Console.WriteLine(notification);

        Console.WriteLine("Press 'S' to Start, 'T' to Stop, 'R' to Reset, and 'Q' to Quit.");

        while (true)
        {
            string input = Console.ReadLine()?.ToUpper();

            switch (input)
            {
                case "S":
                    counter.StartCounter();
                    break;
                case "T":
                    counter.StopCounter();
                    break;
                case "R":
                    counter.ResetCounter();
                    break;
                case "Q":
                    Console.WriteLine("Exiting application.");
                    return;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
}

