using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl6 : MonoBehaviour
{
    public static lvl6 instance;

    public Animator danzante;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void danzanteCirculo()
    {
        danzante.SetBool("circulo", true);    
    }
}
