using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chicaNPC : MonoBehaviour
{
    public bool caminar;
    Rigidbody2D rigidbodyDance;

    public Animator chicaAnimator;
    public float TiempoEsperaChica;
    public GameObject panuelo;
    public bool hayPanuelo;

    float timer = 0f;

    void Start()
    {
        rigidbodyDance = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            print("chica npc");
            //caminar = true;
            //gameObject.GetComponent<BoxCollider2D>();
            StartCoroutine(TiempoCaminar());
          
        }
    }
    private void Update()
    {
        if(caminar)
        {
            timer += Time.deltaTime;

            if (timer >= 0.05f) // 1 segundo ha pasado
            {
                rigidbodyDance.velocity = Vector2.right * 5f;
                chicaAnimator.SetBool("CaminarDer", true);
            }
        }
    }
    IEnumerator TiempoCaminar()
    {
        yield return new WaitForSeconds(TiempoEsperaChica);
        caminar = true;
        if (hayPanuelo)
        {
            panuelo.SetActive(true);
        }
    }
}
