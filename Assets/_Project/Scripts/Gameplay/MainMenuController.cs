using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    [Header("Narration")]
    public AudioClip narrationClip;

    [Header("Next Scene")]
    public string nextSceneName;


    // Start is called before the first frame update
    void Start()
    {
        AudioController.Instance.PlayNarrationClip(narrationClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonPressed()
    {

        AudioController.Instance.StopAllClips();

        SceneTransitionManager.Instance.FadeScene(nextSceneName);

    }
}
