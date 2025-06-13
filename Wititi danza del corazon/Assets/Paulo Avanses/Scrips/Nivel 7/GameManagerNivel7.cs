using UnityEngine;
using UnityEngine.UI;

public class GameManagerNivel7 : MonoBehaviour
{
    public int score = 50;
    public int winScore = 300;
    public int loseScore = 0;
    public Transform player;
    public float moveDistance = 0.1f;
    public Text scoreText;
    public GameObject winScreen;
    public GameObject loseScreen;

    public void AddScore(int value)
    {
        score += value;
        score = Mathf.Clamp(score, 0, 300);
        scoreText.text = $"Puntaje: {score}";

        if (value > 0) player.Translate(Vector3.right * moveDistance);
        else           player.Translate(Vector3.left * moveDistance);

        if (score >= winScore) Win();
        if (score <= loseScore) Lose();
    }

    void Win()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    void Lose()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
