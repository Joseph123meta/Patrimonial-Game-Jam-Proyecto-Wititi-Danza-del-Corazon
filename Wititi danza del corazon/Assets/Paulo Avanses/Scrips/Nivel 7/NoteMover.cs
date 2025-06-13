using UnityEngine;

public class NoteMover : MonoBehaviour
{
    public float scrollSpeed = 5f; // <- ESTA VARIABLE DEBE EXISTIR

    void Update()
    {
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        // Destruir la nota si se sale de la pantalla
        if (transform.position.y > 10f)
        {
            Destroy(gameObject);
        }
    }
}
