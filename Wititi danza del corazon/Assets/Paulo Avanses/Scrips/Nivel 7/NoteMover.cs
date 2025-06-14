using UnityEngine;

public class NoteMover : MonoBehaviour
{
    public float scrollSpeed = 8f;
    public float visualOffset = 0.5f; // ajusta este valor según el tamaño de tu flecha

    private bool alreadyFailed = false;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        if (!alreadyFailed && IsAboveScreen(mainCamera))
        {
            alreadyFailed = true;

            GameManagerNivel7 gameManager = FindObjectOfType<GameManagerNivel7>();
            if (gameManager != null)
            {
                gameManager.AddScore(-10);
            }

            Destroy(gameObject);
        }
    }

    bool IsAboveScreen(Camera cam)
    {
        Vector3 topPos = transform.position + new Vector3(0, visualOffset, 0);
        Vector3 viewportPos = cam.WorldToViewportPoint(topPos);
        return viewportPos.y > 1.0f;
    }
}
