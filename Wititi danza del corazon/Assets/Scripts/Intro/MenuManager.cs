using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public string nombreEscena;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    public void cambiarEscena()
    {
        SceneManager.LoadScene(nombreEscena);
    }
    public void SalirJuego()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
