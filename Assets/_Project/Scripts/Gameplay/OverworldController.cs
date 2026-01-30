using UnityEngine;
using System.Collections.Generic;

public class OverworldController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform overworldMap;
    [SerializeField] private Transform playerIndicator;
    [SerializeField] private Transform locationsParent;
    [SerializeField] private Camera cam;



    [Header("Scrolling")]
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private float verticalScreenPadding = 1.5f;

    private List<Transform> locations = new List<Transform>();
    private int currentIndex = 0;

    private float mapStartY;

    private float targetMapY;

    private string targetLevelScene;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;

        // Cache locations in hierarchy order
        foreach (Transform child in locationsParent)
            locations.Add(child);

        mapStartY = overworldMap.position.y;
        targetMapY = mapStartY;


        // Start at first location
        SnapToLocation(0);
    }

    private void Update()
    {
        HandleInput();
        ScrollMapIfNeeded();
    }
    private void LateUpdate()
    {
        Vector3 pos = overworldMap.position;
        pos.y = Mathf.Lerp(pos.y, targetMapY, Time.deltaTime * scrollSpeed);
        overworldMap.position = pos;
    }


    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
            EnterLevel();

        if (Input.GetKeyDown(KeyCode.UpArrow))
            MoveSelection(1);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            MoveSelection(-1);
    }

    private void MoveSelection(int direction)
    {
        int newIndex = Mathf.Clamp(
            currentIndex + direction,
            0,
            locations.Count - 1
        );

        if (newIndex == currentIndex)
            return;

        currentIndex = newIndex;
        SnapToLocation(currentIndex);
    }

    private void SnapToLocation(int index)
    {
        //playerIndicator.position = locations[index].position;
        Vector3 newPosition = new Vector3(locations[index].position.x, locations[index].position.y, -0.1f);
        playerIndicator.position = newPosition;

        targetLevelScene = locations[index].GetComponent<MapLevelNode>().levelTargetName;
        Debug.Log("Updated TargetLevel to: " + targetLevelScene);
    }

    private void ScrollMapIfNeeded()
    {
        Transform targetLocation = locations[currentIndex];

        float cameraTopY = cam.transform.position.y + cam.orthographicSize;
        float cameraBottomY = cam.transform.position.y - cam.orthographicSize;

        float targetY = targetLocation.position.y;

        // If selected location is near top of screen, scroll map up
        if (targetY > cameraTopY - verticalScreenPadding)
        {
            float delta = targetY - (cameraTopY - verticalScreenPadding);
            MoveMap(-delta);
        }

        // If selected location is near bottom of screen, scroll map down
        if (targetY < cameraBottomY + verticalScreenPadding)
        {
            float delta = (cameraBottomY + verticalScreenPadding) - targetY;
            MoveMap(delta);
        }
    }

    //private void MoveMap(float deltaY)
    //{
    //    Vector3 newPos = overworldMap.position + Vector3.up * deltaY;

    //    // Clamp: never go lower than start
    //    if (newPos.y < mapStartY)
    //        newPos.y = mapStartY;

    //    overworldMap.position = Vector3.Lerp(
    //        overworldMap.position,
    //        newPos,
    //        Time.deltaTime * scrollSpeed
    //    );
    //}
    private void MoveMap(float deltaY)
    {
        targetMapY += deltaY;

        // Clamp bottom
        if (targetMapY > mapStartY)
            targetMapY = mapStartY;
    }

    private void EnterLevel()
    {
        Debug.Log("EnterLevel: " + targetLevelScene);
        AudioController.Instance.StopAllClips();
        SceneTransitionManager.Instance.FadeScene(targetLevelScene);

    }
}
