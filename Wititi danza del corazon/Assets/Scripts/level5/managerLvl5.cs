using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerLvl5 : MonoBehaviour
{
    public static managerLvl5 instance;

    public GameObject hitoFinal;
    public bool ActivarHito;
    bool Nulo = false;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        hitoFinal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Nulo && ActivarHito)
        {
            Nulo = true;
            hitoFinal.SetActive(true);
        }
    }
}
