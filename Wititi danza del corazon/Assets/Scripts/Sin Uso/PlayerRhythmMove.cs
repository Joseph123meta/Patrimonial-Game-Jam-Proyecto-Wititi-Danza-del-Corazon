using UnityEngine;

public class PlayerRhythmMove : MonoBehaviour
{
    public float moveDistance = 1f;
    private bool canMove = true;

    void Update()
    {
        if (BeatManager.isOnBeat || !canMove) return;

        Vector2 move = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow)) move = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) move = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) move = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow)) move = Vector2.right;

        if (move != Vector2.zero)
        {
            transform.position += (Vector3)(move * moveDistance);
            canMove = false;
            Invoke(nameof(ResetMove), 0.1f);
            print("aca");
        }
    }

    void ResetMove()
    {
        canMove = true;
    }
}
