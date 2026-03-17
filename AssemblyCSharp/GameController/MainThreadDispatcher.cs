using System;
using System.Collections.Generic;

public class MainThreadDispatcher
{
    private static readonly Queue<Action> actions = new Queue<Action>();

    public static void Enqueue(Action action)
    {
        lock (actions)
        {
            actions.Enqueue(action);
        }
    }

    public static void Execute()
    {
        lock (actions)
        {
            while (actions.Count > 0)
            {
                actions.Dequeue().Invoke();
            }
        }
    }
}