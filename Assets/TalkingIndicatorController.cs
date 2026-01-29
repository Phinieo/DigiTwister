using UnityEngine;
using UnityEngine.UI;

public class TalkingSpriteController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image characterImage;

    [Header("Sprites")]
    [SerializeField] private Sprite mouthClosed;
    [SerializeField] private Sprite mouthOpen;

    [Header("Animation")]
    [SerializeField] private float frameDuration = 0.25f;

    public bool isTalking;

    private float timer;
    private bool showingOpen;

    private void Awake()
    {
        if (characterImage == null)
            characterImage = GetComponent<Image>();

        SetMouthClosed();
    }

    private void Update()
    {
        if (!isTalking)
        {
            if (showingOpen)
                SetMouthClosed();

            return;
        }

        timer += Time.deltaTime;

        if (timer >= frameDuration)
        {
            timer = 0f;
            ToggleMouth();
        }
    }

    private void ToggleMouth()
    {
        showingOpen = !showingOpen;
        characterImage.sprite = showingOpen ? mouthOpen : mouthClosed;
    }

    private void SetMouthClosed()
    {
        showingOpen = false;
        characterImage.sprite = mouthClosed;
        timer = 0f;
    }
}
