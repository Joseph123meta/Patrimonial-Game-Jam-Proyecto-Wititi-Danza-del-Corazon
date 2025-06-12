using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string nombreEscena;
    // Start is called before the first frame update
    public void cambiarEscena()
    {
        SceneManager.LoadScene(nombreEscena);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
