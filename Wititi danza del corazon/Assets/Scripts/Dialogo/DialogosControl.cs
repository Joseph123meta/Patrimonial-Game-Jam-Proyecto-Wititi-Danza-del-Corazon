using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogosControl : MonoBehaviour
{
    public GameObject a1, b1, a2, b2;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("collision de dialogos");
            //caminar = true;
            //gameObject.GetComponent<BoxCollider2D>();
            a1.SetActive(false);
            b1.SetActive(false);
            a2.SetActive(true);
            b2.SetActive(true);
            gameObject.SetActive(false);
        }
        
    }
}
