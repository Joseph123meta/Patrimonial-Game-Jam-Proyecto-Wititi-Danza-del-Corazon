using UnityEngine;

public class ArrowInput : MonoBehaviour
{
    public float hitWindow = 0.5f;
    public GameManagerNivel7 gameManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) CheckHit(ArrowType.Left);
        if (Input.GetKeyDown(KeyCode.DownArrow)) CheckHit(ArrowType.Down);
        if (Input.GetKeyDown(KeyCode.UpArrow))   CheckHit(ArrowType.Up);
        if (Input.GetKeyDown(KeyCode.RightArrow))CheckHit(ArrowType.Right);
    }

    void CheckHit(ArrowType type)
    {
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");

        foreach (GameObject note in notes)
        {
            if (note.name != type.ToString()) continue;

            float distance = Mathf.Abs(note.transform.position.y - transform.position.y);
            if (distance <= hitWindow)
            {
                Destroy(note);
                gameManager.AddScore(10);
                return;
            }
        }

        gameManager.AddScore(-10);
    }
}
