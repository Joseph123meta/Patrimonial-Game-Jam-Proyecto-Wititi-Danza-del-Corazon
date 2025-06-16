using UnityEngine;

public class NoteMover : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public float despawnY = 10f; // 🔺 Altura en Y donde la nota se considera fallada (ajústalo en el Inspector)

    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        if (transform.position.y > despawnY)
        {
            GameManagerNivel7 gameManager = FindObjectOfType<GameManagerNivel7>();
            if (gameManager != null)
            {
                gameManager.AddScore(-10); // ❌ Penalización por fallar
            }

            Destroy(gameObject); // 🔥 Destruye la nota fallada
        }
    }
}
