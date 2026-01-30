using System;

public interface IMinigame
{
    void StartGame();
    void EndGame();

    event Action<bool> OnGameCompleted;
}
