using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{

    [Header("Narration")]
    public AudioClip narrationClip;

    [Header("Next Scene")]
    public string nextSceneName;

    private bool inputReceived;


    // Start is called before the first frame update
    void Start()
    {


        StartCoroutine(NarrationFlow());

    }

    // Update is called once per frame
    void Update()
    {
        // Any key, mouse button, or controller button
        if (Input.anyKeyDown)
        {
            inputReceived = true;
        }
    }

    private IEnumerator NarrationFlow()
    {
        // Play narration
        AudioController.Instance.PlayNarrationClip(narrationClip);

        // Wait until narration ends OR input is pressed
        yield return new WaitUntil(() =>
            inputReceived || !AudioController.Instance.IsNarrationPlaying
        );

        // Optional: stop narration early if skipped
        AudioController.Instance.StopNarrationClip();

        // Transition
        SceneTransitionManager.Instance.FadeScene(nextSceneName);
    }
}

