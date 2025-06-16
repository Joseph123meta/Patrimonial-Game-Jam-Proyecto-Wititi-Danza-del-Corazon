using UnityEngine;
using UnityEngine.UI;

public class GameManagerNivel7 : MonoBehaviour
{
    public int score = 50;
    public int winScore = 300;
    public int loseScore = 0;

    public Transform player;
    public Transform startPoint;
    public Transform endPoint;

    public Text scoreText;
    public GameObject winScreen;
    public GameObject loseScreen;

    public AudioSource musicSource;
    public NoteSpawner noteSpawner;
    private bool gameStarted = false;

    private float distanciaPorPunto;

    void Start()
    {
        if (player != null && startPoint != null)
        {
            player.position = startPoint.position;
        }

        if (startPoint != null && endPoint != null)
        {
            float distanciaTotal = Vector3.Distance(startPoint.position, endPoint.position);
            int scoreTotal = winScore - loseScore;
            distanciaPorPunto = distanciaTotal / scoreTotal;
        }

        UpdateScoreText();
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        musicSource.Play();
        noteSpawner.BeginSpawning();
    }

    public void AddScore(int value)
    {
        int oldScore = score;
        score += value;
        score = Mathf.Clamp(score, loseScore, winScore);
        int scoreDelta = score - oldScore;

        UpdateScoreText();

        if (scoreDelta != 0)
        {
            Vector3 direction = (endPoint.position - startPoint.position).normalized;
            player.position += direction * (distanciaPorPunto * scoreDelta);
        }

        if (score >= winScore) Win();
        else if (score <= loseScore) Lose();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text ="Puntaje: " + score.ToString();
        }
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
