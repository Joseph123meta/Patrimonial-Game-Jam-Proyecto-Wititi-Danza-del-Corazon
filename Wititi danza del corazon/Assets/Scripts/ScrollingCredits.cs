using UnityEngine;
using TMPro;

public class ScrollingCredits : MonoBehaviour
{
    public RectTransform creditsText; 
    public float scrollSpeed = 30f; 
    private float startY;
    private float resetY;

    void Start()
    {
        
        startY = creditsText.anchoredPosition.y;
        // Calcular punto de reiniciar
        resetY = creditsText.sizeDelta.y + GetComponent<RectTransform>().sizeDelta.y;
    }

    void Update()
    {
        
        creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        
        if (creditsText.anchoredPosition.y > resetY)
        {
            creditsText.anchoredPosition = new Vector2(creditsText.anchoredPosition.x, startY);
        }
    }
}
