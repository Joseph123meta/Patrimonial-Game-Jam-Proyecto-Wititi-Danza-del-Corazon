using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicaciones : MonoBehaviour
{
    public GameObject indicaciones;
    // Start is called before the first frame update
    void Start()
    {
        aparecerIndicacion();
    }

    // Update is called once per frame
    void aparecerIndicacion()
    {
        indicaciones.SetActive(false);
        StartCoroutine(inicioIndicacion());
    }
    IEnumerator inicioIndicacion()
    {
        yield return new WaitForSeconds(3.5f);
        indicaciones.SetActive(true);
        yield return new WaitForSeconds(6.2f);
        indicaciones.SetActive(false);
    }
}
