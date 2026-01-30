using UnityEngine;
using System;

public abstract class MinigameBase : MonoBehaviour, IMinigame
{
    public event Action<bool> OnGameCompleted;

    protected bool isRunning;

    public virtual void StartGame()
    {
        isRunning = true;
    }

    protected void Complete(bool success)
    {
        if (!isRunning) return;

        isRunning = false;
        OnGameCompleted?.Invoke(success);
    }

    public virtual void EndGame()
    {
        isRunning = false;
    }
}
