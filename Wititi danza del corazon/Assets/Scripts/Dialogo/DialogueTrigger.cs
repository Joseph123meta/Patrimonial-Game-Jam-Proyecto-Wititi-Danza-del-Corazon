using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string[] dialogueLines;
    public DialogueManager dialogueManager;

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
    //    {
    //        dialogueManager.StartDialogue(dialogueLines);
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Player")// && Input.GetKeyDown(KeyCode.E))
        {
            MovementController.instance.jugadorHabilitado = false;
            MovementController.instance.AnimIdle();
            print("aca");
            dialogueManager.StartDialogue(dialogueLines);
        }
    }
}