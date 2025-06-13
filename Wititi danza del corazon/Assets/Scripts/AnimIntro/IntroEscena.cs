using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEscena : MonoBehaviour
{
    public static IntroEscena instance;

    public Animator animIntro;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        InicioIntro();
    }

    public void InicioIntro()
    {
        StartCoroutine(inicioEscena());
    }
    IEnumerator inicioEscena()
    {        
        yield return new WaitForSeconds(1.5f);
        animIntro.SetTrigger("inicio");
    }
    public void FinIntro()
    {
        StartCoroutine(FinEscena());
        
    }

    IEnumerator FinEscena()
    {
        animIntro.SetBool("pausa", true);
        yield return new WaitForSeconds(2.5f);
        MenuManager.instance.cambiarEscena();
    }

 
}
