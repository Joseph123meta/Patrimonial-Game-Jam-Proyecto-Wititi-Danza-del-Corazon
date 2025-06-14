using UnityEngine;
using System.Collections;

public class CharacterAnimatorController : MonoBehaviour
{
    public Animator animator;
    public float returnToIdleDelay = 0.5f; // tiempo antes de volver al idle

    private bool isTurning = false;

    void Update()
    {
        if (isTurning) return; // Ignorar entrada mientras gira

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(PlayTurn("izquierda"));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(PlayTurn("derecha"));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(PlayTurn("abajo"));
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow)){
            StartCoroutine(PlayTurn("arriba"));
        }
        // UpArrow no hace nada
    }

    IEnumerator PlayTurn(string triggerName)
    {
        isTurning = true;
        animator.SetTrigger(triggerName);

        yield return new WaitForSeconds(returnToIdleDelay);

        animator.SetTrigger("arriba"); // volver al frente
        isTurning = false;
    }
}
