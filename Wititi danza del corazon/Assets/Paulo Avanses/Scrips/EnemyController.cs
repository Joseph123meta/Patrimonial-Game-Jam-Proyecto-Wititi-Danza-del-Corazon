using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;

    public enum MovementMode
    {
        Estatico,
        EnMovimiento
    }

    [Header("Comportamiento de Movimiento")]
    [SerializeField] private MovementMode Estado = MovementMode.Estatico;
    [SerializeField] private bool ActualizacionDestino = false;
    [SerializeField] private GameObject Destino;
    [SerializeField] private float Velocidad = 2f;
    [SerializeField] private float TiempoDeEspera = 2f;
    [SerializeField] private float DistanciaAceptada = 0.1f;

    private Vector2 CoordenadasDestino;
    private Vector2 PosicionInicial;
    private bool Caminando = false;
    private bool DeRegreso = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        if (Destino != null)
        {
            CoordenadasDestino = Destino.transform.position;
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (!ActualizacionDestino)
        {
            PosicionInicial = transform.position;
        }

        if (Estado == MovementMode.EnMovimiento)
        {
            StartCoroutine(WaitAtStartAndGoToDestination());
        }
    }

    private void FixedUpdate()
    {
        if (ActualizacionDestino)
        {
            PosicionInicial = transform.position;
        }

        if (Estado == MovementMode.EnMovimiento)
        {
            HandleGoToDestinationAndReturn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
        }
    }

    public void StopRotation()
    {
        PatternController patternController = GetComponentInParent<PatternController>();
        if (patternController != null)
        {
            patternController.enabled = !patternController.enabled;
        }
        Persecucion persecucion = GetComponent<Persecucion>();
        if (persecucion !=null)
        {
            persecucion.enabled = !persecucion.enabled;
        }
    }

    private void HandleGoToDestinationAndReturn()
    {
        if (Caminando)
        {
            Vector2 direction = (CoordenadasDestino - (Vector2)transform.position).normalized;
            rb.MovePosition(rb.position + direction * Velocidad * Time.fixedDeltaTime);

            if (Vector2.Distance(transform.position, CoordenadasDestino) < DistanciaAceptada)
            {
                Caminando = false;
                StartCoroutine(WaitAndReturn());
            }
        }
        else if (DeRegreso)
        {
            Vector2 direction = (PosicionInicial - (Vector2)transform.position).normalized;
            rb.MovePosition(rb.position + direction * Velocidad * Time.fixedDeltaTime);

            if (Vector2.Distance(transform.position, PosicionInicial) < DistanciaAceptada)
            {
                DeRegreso = false;
                StartCoroutine(WaitAtStartAndGoToDestination());
            }
        }
    }

    IEnumerator WaitAndReturn()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(TiempoDeEspera);
        DeRegreso = true;
    }

    IEnumerator WaitAtStartAndGoToDestination()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(TiempoDeEspera);
        Caminando = true;
    }
}
