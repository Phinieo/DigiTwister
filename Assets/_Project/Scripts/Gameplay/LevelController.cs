using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour
{
    [Header("Config")]
    public List<GameObject> minigamePrefabs;
    public int gamesToWin = 10;
    public int startingHearts = 3;
    public float minigameTimeLimit = 5f;

    [Header("Runtime")]
    public Transform spawnPoint;

    private int hearts;
    private int gamesCompleted;
    private GameObject currentMinigame;

    void Start()
    {
        hearts = startingHearts;
        StartCoroutine(LevelLoop());
    }

    IEnumerator LevelLoop()
    {
        while (hearts > 0 && gamesCompleted < gamesToWin)
        {
            yield return PlayNextMinigame();
        }

        EndLevel();
    }

    IEnumerator PlayNextMinigame()
    {
        GameObject prefab = minigamePrefabs[Random.Range(0, minigamePrefabs.Count)];
        currentMinigame = Instantiate(prefab, spawnPoint);

        var game = currentMinigame.GetComponent<IMinigame>();
        bool? result = null;

        game.OnGameCompleted += success => result = success;
        game.StartGame();

        float timer = minigameTimeLimit;

        while (result == null && timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        game.EndGame();

        if (result == true)
        {
            gamesCompleted++;
            Debug.Log("MiniGame Win");
        }
        else
        {
            hearts--;
            Debug.Log("MiniGame Loss");
        }


        Destroy(currentMinigame);
        yield return new WaitForSeconds(0.5f); // pacing
    }

    void EndLevel()
    {
        if (gamesCompleted >= gamesToWin)
        {
            Debug.Log("LEVEL PASSED");
            SceneTransitionManager.Instance.FadeScene("OverworldScene");
        }
        else
        {

            Debug.Log("LEVEL FAILED");
            SceneTransitionManager.Instance.FadeScene("OverworldScene");

        }

    }
}
