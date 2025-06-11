using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoIntro : MonoBehaviour
{
    public Image logoImage;            // Asigna tu logo aquí (Image UI)
    public float fadeDuration = 2f;    // Duración del fade-in y fade-out
    public float displayTime = 2f;     // Tiempo que se mantiene visible
    public string nombreEscena;

    private void Start()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        // Aparece (fade in)
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        // Espera con el logo visible
        yield return new WaitForSeconds(displayTime);

        // Desaparece (fade out)
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));

        // Carga la siguiente escena
        SceneManager.LoadScene(nombreEscena); // Cambia por el nombre de tu escena principal
    }

    IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        Color color = logoImage.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            logoImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
    }
}