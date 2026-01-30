using UnityEngine;

public class PressSpaceMiniGame : MinigameBase
{
    protected void Update()
    {

        if (!isRunning)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Complete(true);
        }
    }
}
