using UnityEngine;
using UnityEngine.UI;

public class BeatManager : MonoBehaviour
{
    public float bpm = 100f;
    public Image beatCircle;

    public static bool isOnBeat = false;

    private float beatTimer;
    private float beatInterval;

    void Start()
    {
        beatInterval = 50f / bpm;
        beatTimer = 0f;
    }

    void Update()
    {
        beatTimer += Time.deltaTime;

        if (beatTimer >= beatInterval)
        {
            // Disparo del beat
            isOnBeat = true;
            beatTimer -= beatInterval;

            StartCoroutine(FlashBeat());
        }
        else
        {
            isOnBeat = false;
        }
    }

    System.Collections.IEnumerator FlashBeat()
    {
        beatCircle.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        beatCircle.color = Color.gray;
    }
}
