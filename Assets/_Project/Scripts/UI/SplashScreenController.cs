using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{

    [SerializeField] private FadeController fadeController;

    private IEnumerator SplashScreenSequence()
    {

        yield return new WaitForSeconds(5);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Fade in when the game first starts
        fadeController.FadeIn();
        StartCoroutine(SplashScreenSequence());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
