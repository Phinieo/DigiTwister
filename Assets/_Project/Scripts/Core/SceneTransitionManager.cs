using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    [SerializeField] private FadeController fadeController;

    private void Awake()
    {
        // Singleton pattern (jam-safe version)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Fade in when the game first starts
        fadeController.FadeIn();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAfterDelay(string sceneName, float delay)
    {
        StartCoroutine(LoadAfterDelay(sceneName, delay));
    }

    private IEnumerator LoadAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void FadeScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithFade(sceneName));
    }


    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        // Fade to black
        yield return fadeController.FadeOutCoroutine();

        // Load new scene
        SceneManager.LoadScene(sceneName);

        // Wait one frame so the new scene initializes
        yield return null;

        // Fade back in
        yield return fadeController.FadeInCoroutine();
    }
}
