using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    public static Final instance;

    public GameObject panelMenu;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        FinalEscena();
    }

   public void FinalEscena()
    {
        StartCoroutine(Escena());
    }
    IEnumerator Escena()
    {
        yield return new WaitForSeconds(9.0f);
        panelMenu.SetActive(true);
        
        
    }
}
