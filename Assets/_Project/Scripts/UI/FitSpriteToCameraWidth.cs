using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FitSpriteToCameraWidth : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Camera cam;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        FitAndAlign();
    }

    private void FitAndAlign()
    {
        if (spriteRenderer.sprite == null || cam == null)
            return;

        // ---- 1. Fit sprite to camera width ----
        float cameraHeight = cam.orthographicSize * 2f;
        float cameraWidth = cameraHeight * cam.aspect;

        float spriteWidth = spriteRenderer.sprite.bounds.size.x;
        float scale = cameraWidth / spriteWidth;

        transform.localScale = new Vector3(scale, scale, 1f);

        // ---- 2. Align sprite bottom to camera bottom ----
        float spriteHeight = spriteRenderer.sprite.bounds.size.y * scale;

        float cameraBottomY = cam.transform.position.y - cam.orthographicSize;
        float spriteCenterY = cameraBottomY + (spriteHeight / 2f);

        transform.position = new Vector3(
            cam.transform.position.x,
            spriteCenterY,
            transform.position.z
        );
    }
}