using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public static MovementController instance;

    new Rigidbody2D rigidbody;
    Animator animator;


    public float speed;

    [Header("hito")]
    [SerializeField] private GameObject Inicio;

    

    public bool jugadorHabilitado = true;

    public bool jugadorVestido;

    int vida = 0;
    bool recuperado = true;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ResetPosicion();
        rigidbody = GetComponent<Rigidbody2D>();
        //obtener la referencia al rigiidbody
        animator = GetComponent<Animator>();

        //panelPause.SetActive(false);

        if(jugadorVestido)
        {
            CambiarVestido();
        }

    }

    void Update()
    {
        if (jugadorHabilitado)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (horizontal != 0 || vertical != 0)
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
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Final")
        {
            IntroEscena.instance.FinIntro();
            AnimIdle();
            jugadorHabilitado = false;
        }

        //Debug.Log("Colisión con: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Bailarin1" && recuperado)
        {
            recuperado = false;
            StartCoroutine(TiempoRecuperar());
            GameManager.instance.VidasPlayer();
            //gameObject.transform.position = Inicio.transform.position;
            
            GameManager.instance.audioAbuchear();

            StartCoroutine(DañarJugador(collision));

            if (vida < 2)
            {
                vida = vida + 1;
                print("vida player: " + vida);
            }
            else
            {
                ResetPosicion();
                vida = 0;
            }
            //print("chocamos xd");
        }
    }
    void ResetPosicion()
    {
        gameObject.transform.position = Inicio.transform.position;
    }
    IEnumerator TiempoRecuperar()
    {
        yield return new WaitForSeconds(1.0f);
        recuperado = true;
    }

  

    IEnumerator DañarJugador(Collision2D col)
    {
        // Retroceso (knockback)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direccion = (transform.position - col.transform.position).normalized;
        float fuerzaRetroceso = -5f;
        rb.velocity = Vector2.zero; // Detiene movimiento previo
        rb.AddForce(direccion * fuerzaRetroceso, ForceMode2D.Impulse);

        // Invulnerabilidad visual (parpadeo)
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        int repeticiones = 5;
        float duracion = 0.1f;

        for (int i = 0; i < repeticiones; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(duracion);
            sr.enabled = true;
            yield return new WaitForSeconds(duracion);
        }

        // Aquí podrías poner una variable para desactivar invulnerabilidad si usas lógica de daño
    }
    public void AnimIdle()
    {
        animator.SetBool("Moving", false);
        rigidbody.velocity = Vector2.zero;
    }
    public void CambiarVestido()
    {
        animator.SetBool("Vestido", true);
    }
    public void QuitarVestido()
    {
        animator.SetBool("Vestido", false);
    }
}
