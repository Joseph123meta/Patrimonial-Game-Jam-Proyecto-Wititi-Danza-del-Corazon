using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologo : MonoBehaviour
{
    //public GameObject prologo;
    public GameObject panelIndicaciones;
    // Start is called before the first frame update
    private void Awake()
    {
        panelIndicaciones.SetActive(false);
    }
    void Start()
    {
        //prologo.SetActive(true);
       
        MovementController.instance.jugadorHabilitado = false;
        Empezar();
    }

    // Update is called once per frame
    void Empezar()
    {
        StartCoroutine(Escena());
    }
    IEnumerator Escena()
    {
        yield return new WaitForSeconds(9.0f);
        panelIndicaciones.SetActive(true);
        MovementController.instance.jugadorHabilitado = true;
        gameObject.SetActive(false);
    }
}
