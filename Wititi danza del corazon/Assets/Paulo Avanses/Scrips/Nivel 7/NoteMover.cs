using UnityEngine;

public class NoteMover : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public float despawnY = 3f;

    private bool alreadyFailed = false;

    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        if (!alreadyFailed && transform.position.y > despawnY)
        {
            alreadyFailed = true; // 🔒 Marcar que ya se procesó el fallo

            GameManagerNivel7 gameManager = FindObjectOfType<GameManagerNivel7>();
            if (gameManager != null)
            {
                gameManager.AddScore(-10);
            }

            Destroy(gameObject); // Solo una vez y solo si no fue ya destruido
        }
    }
}

