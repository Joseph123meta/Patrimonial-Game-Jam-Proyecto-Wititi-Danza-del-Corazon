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
       
        StartCoroutine(inicioIndicacion());
    }
    IEnumerator inicioIndicacion()
    {
        //yield return new WaitForSeconds(1.5f);
        //in//dicaciones.SetActive(true);
        yield return new WaitForSeconds(13.2f);
        indicaciones.SetActive(false);
    }
}
