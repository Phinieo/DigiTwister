using UnityEngine;

public class ListenOrSkipMiniGame : MinigameBase
{
    [Header("Audio")]
    [SerializeField] private AudioClip narrationClip;

    private bool hasWon;

    private void OnEnable()
    {
        if (AudioController.Instance != null)
        {
            AudioController.Instance.OnNarrationClipStopped += HandleNarrationEnded;
        }
    }

    private void OnDisable()
    {
        if (AudioController.Instance != null)
        {
            AudioController.Instance.OnNarrationClipStopped -= HandleNarrationEnded;
        }
    }

    private void Start()
    {
        hasWon = false;

        if (AudioController.Instance != null && narrationClip != null)
        {
            AudioController.Instance.PlayNarrationClip(narrationClip);
        }

        //StartMiniGame(timeRemaining > 0 ? timeRemaining : narrationClip.length);
    }

    protected void Update()
    {
        //base.Update();

        if (!isRunning || hasWon)
            return;

        // Any key press wins
        if (Input.anyKeyDown)
        {
            Complete(true);
        }
    }

    private void HandleNarrationEnded(AudioClip clip)
    {
        if (hasWon)
            return;

        if (clip == narrationClip)
        {
            Complete(true);
        }
    }

}
