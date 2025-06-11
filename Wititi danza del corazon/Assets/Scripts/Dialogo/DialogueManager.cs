using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;

    [Header("Dialogo")]
    [SerializeField] private GameObject npc;
    [SerializeField] private GameObject jugador;

    public string[] lines;
    public int[] personajeNumero;
    public float textSpeed = 0.03f;

    private int index;
    private bool isTyping = false;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            
            //
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
                isTyping = false;
            }
            else
            {
                NextLine();
                BeginFoto();
            }


        }
    }

    public void StartDialogue(string[] dialogueLines)
    {
        lines = dialogueLines;
        index = 0;
        dialoguePanel.SetActive(true);
        BeginFoto();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in lines[index])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        index++;
        if (index < lines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            dialoguePanel.SetActive(false);
            MovementController.instance.jugadorHabilitado = true;
        }
    }
    void BeginFoto()
    {
        print("index: " + index);
        dialogoFotoOcultar();//
        if (index < lines.Length)
        {

            if (personajeNumero[index] == 1)
            {
                print("/////1");
                jugador.SetActive(true);
                dialogueText.color = Color.red;
            }
            else if (personajeNumero[index] == 0)
            {
                print("2//////");
                npc.SetActive(true);
                dialogueText.color = Color.white;
            }
        }
    }

    void dialogoFotoOcultar()
    {
        jugador.SetActive(false);
        npc.SetActive(false);

    }
}