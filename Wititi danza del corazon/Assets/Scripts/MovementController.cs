using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    Animator animator;


    public float speed;

    [Header("Gameobject Emo")]
    [SerializeField] private GameObject Inicio;


    void Start()
    {
        ResetPosicion();
        rigidbody = GetComponent<Rigidbody2D>();
        //obtener la referencia al rigiidbody
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal != 0 || vertical != 0)
        {
            Vector2 velocity = new Vector2(horizontal, vertical).normalized * speed;

            rigidbody.velocity = velocity;

            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
            animator.SetBool("Moving", true);

        }
        else
        {
            rigidbody.velocity = Vector2.zero;
            animator.SetBool("Moving", false);
        }

                
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Colisión con: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Bailarin1")
        {
            GameManager.instance.VidasPlayer();
            //gameObject.transform.position = Inicio.transform.position;
            GameManager.instance.audioAbuchear();
            //print("chocamos xd");
        }
    }
    void ResetPosicion()
    {
        gameObject.transform.position = Inicio.transform.position;
    }
}
