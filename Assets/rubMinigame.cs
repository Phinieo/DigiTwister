using UnityEngine;
using UnityEngine.UI;

public class rubMinigame : MinigameBase
{
    [Header("Rub Settings")]
    [SerializeField] private RectTransform rubZone;
    [SerializeField] private float rubTimeRequired = 3f;

    [Header("Visuals")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite[] backgroundStages; // 3 stages
    [SerializeField] private Image fakeCursor;

    private float rubTimer;
    private bool hasWon;

    private void Start()
    {
        rubTimer = 0f;
        hasWon = false;

        Cursor.visible = false;

        if (backgroundStages.Length > 0)
            backgroundImage.sprite = backgroundStages[0];

        //StartMiniGame(timeRemaining > 0 ? timeRemaining : rubTimeRequired + 1f);
    }

    protected void Update()
    {

        if (!isRunning || hasWon)
            return;

        UpdateFakeCursor();

        if (IsMouseOverRubZone())
        {
            rubTimer += Time.deltaTime;
            UpdateBackground();

            if (rubTimer >= rubTimeRequired)
            {
                Complete(true);
            }
        }
    }

    private void UpdateFakeCursor()
    {
        fakeCursor.rectTransform.position = Input.mousePosition;
    }

    private bool IsMouseOverRubZone()
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            rubZone,
            Input.mousePosition,
            null // Overlay canvas
        );
    }

    private void UpdateBackground()
    {
        if (backgroundStages.Length < 3)
            return;

        float progress = rubTimer / rubTimeRequired;

        if (progress < 0.33f)
            backgroundImage.sprite = backgroundStages[0];
        else if (progress < 0.66f)
            backgroundImage.sprite = backgroundStages[1];
        else
            backgroundImage.sprite = backgroundStages[2];
    }

    //protected override void Win()
    //{
    //    if (hasWon)
    //        return;

    //    hasWon = true;
    //    Cursor.visible = true;
    //    base.Win();
    //}

    //protected override void Lose()
    //{
    //    Cursor.visible = true;
    //    base.Lose();
    //}

    private void OnDisable()
    {
        Cursor.visible = true;
    }
}
