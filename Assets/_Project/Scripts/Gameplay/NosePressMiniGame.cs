using UnityEngine;
using UnityEngine.UI;

public class PressSpaceMiniGame : MinigameBase
{
    [Header("Audio")]
    [SerializeField] private AudioClip narrationClip;
    [SerializeField] private AudioClip spacePressSfx;

    [Header("Visuals")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite[] backgroundStages; // Size = 3

    private int pressCount;

    private void Start()
    {
        // Defensive checks (helps catch setup errors early)
        if (backgroundStages.Length != 3)
        {
            Debug.LogError("PressSpaceTwiceMiniGame requires exactly 3 background sprites.");
        }

        // Set initial background
        backgroundImage.sprite = backgroundStages[0];

        // Play narration
        if (AudioController.Instance != null && narrationClip != null)
        {
            AudioController.Instance.PlayNarrationClip(narrationClip);
        }

        // Start the minigame timer
        //StartMiniGame(timeRemaining > 0 ? timeRemaining : 3f);
    }

    protected void Update()
    {

        if (!isRunning)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleSpacePressed();
        }
    }

    private void HandleSpacePressed()
    {
        pressCount++;

        // Play SFX
        if (AudioController.Instance != null && spacePressSfx != null)
        {
            AudioController.Instance.PlayEffectClip(spacePressSfx);
        }

        // Clamp index to valid sprite range
        int spriteIndex = Mathf.Clamp(pressCount, 0, backgroundStages.Length - 1);
        backgroundImage.sprite = backgroundStages[spriteIndex];

        if (pressCount >= backgroundStages.Length)
        {
            Complete(true);
        }
    }
}
