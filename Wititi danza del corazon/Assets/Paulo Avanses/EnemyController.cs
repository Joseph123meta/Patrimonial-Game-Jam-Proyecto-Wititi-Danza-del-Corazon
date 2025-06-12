using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;

    // --- Nuevas variables para el comportamiento de movimiento ---
    public enum MovementMode
    {
        Estatico,
        EnMovimiento
    }

    [Header("Comportamiento de Movimiento")]
    [SerializeField] private MovementMode Estado = MovementMode.Estatico;
    [SerializeField] private bool ActualizacionDestino = false;
    [SerializeField] private GameObject Destino; // El destino al que debe ir
    [SerializeField] private float Velocidad = 2f; // Velocidad de movimiento al destino
    [SerializeField] private float TiempoDeEspera = 2f; // Tiempo de espera en el destino
    [SerializeField] private float DistanciaAceptada = 0.1f; // Distancia para considerar que ha llegado al destino

    private Vector2 CoordenadasDestino;
    private Vector2 PosicionInicial; // Corrección: 'PocicionInicial' a 'PosicionInicial'
    private bool Caminando = false; // Bandera para saber si se dirige al destino
    private bool DeRegreso = false; // Bandera para saber si está regresando al inicio
    private Rigidbody2D rb; // Referencia al Rigidbody2D para movimiento

    private void Awake()
    {
        if (Destino != null) // Asegúrate de que el GameObject Destino esté asignado
        {
            CoordenadasDestino = Destino.transform.position;
        }
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (!ActualizacionDestino)
        {
            PosicionInicial = transform.position; // Corrección: 'PocicionInicial' a 'PosicionInicial'
        }

        // Si el modo es EnMovimiento al inicio, empieza el ciclo
        if (Estado == MovementMode.EnMovimiento)
        {
            StartCoroutine(WaitAtStartAndGoToDestination()); // Empieza yendo al destino
        }
    }

    private void FixedUpdate()
    {
        if (ActualizacionDestino)
        {
            PosicionInicial = transform.position; // Corrección: 'PocicionInicial' a 'PosicionInicial'
        }

        if (Estado == MovementMode.EnMovimiento)
        {
            HandleGoToDestinationAndReturn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("detecto colision");
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
            Debug.Log("daño recivido");
            // Aquí puedes agregar la lógica para que el jugador reciba daño.
            // GameManager.instance.VidasPlayer();
            // GameManager.instance.audioHit();
        }
    }

    public void StopRotation()
    {
        Debug.Log("ejecucion en proceso");
        RotateController rotateController = GetComponentInParent<RotateController>();
        if (rotateController != null)
        {
            rotateController.enabled = !rotateController.enabled;
            Debug.Log("rotacion detenida");
        }
    }
    public void StopCoreografia()
    {
        Debug.Log("ejecucion en proceso");
        Coreografia coreografia = GetComponentInParent<Coreografia>();
        if (coreografia != null)
        {
            coreografia.enabled = !coreografia.enabled;
            Debug.Log("rotacion detenida");
        }
    }

    // --- Métodos corregidos para el comportamiento de movimiento ---
    private void HandleGoToDestinationAndReturn()
    {
        if (Caminando) // CORRECTO: Si está caminando hacia el destino
        {
            // Mover hacia el destino
            Vector2 direction = (CoordenadasDestino - (Vector2)transform.position).normalized;
            rb.MovePosition(rb.position + direction * Velocidad * Time.fixedDeltaTime);

            // Verificar si ha llegado al destino
            if (Vector2.Distance(transform.position, CoordenadasDestino) < DistanciaAceptada)
            {
                Caminando = false; // Ya no está caminando al destino
                StartCoroutine(WaitAndReturn()); // Esperar y luego regresar
            }
        }
        else if (DeRegreso) // CORRECTO: Si está regresando a la posición inicial
        {
            // Mover de regreso a la posición inicial
            Vector2 direction = (PosicionInicial - (Vector2)transform.position).normalized;
            rb.MovePosition(rb.position + direction * Velocidad * Time.fixedDeltaTime);

            // Verificar si ha llegado al inicio
            if (Vector2.Distance(transform.position, PosicionInicial) < DistanciaAceptada)
            {
                DeRegreso = false; // Ya no está regresando
                StartCoroutine(WaitAtStartAndGoToDestination()); // Esperar y luego ir al destino
            }
        }
        // Si ninguna bandera es true, el enemigo está esperando en un punto.
    }

    IEnumerator WaitAndReturn()
    {
        rb.velocity = Vector2.zero; // Detener el movimiento
        Debug.Log("Llegó al destino. Esperando...");
        yield return new WaitForSeconds(TiempoDeEspera);
        Debug.Log("Regresando al inicio.");
        DeRegreso = true; // Iniciar el regreso al inicio
    }

    IEnumerator WaitAtStartAndGoToDestination()
    {
        rb.velocity = Vector2.zero; // Detener el movimiento
        Debug.Log("Llegó al inicio. Esperando...");
        yield return new WaitForSeconds(TiempoDeEspera);
        Debug.Log("Yendo al destino.");
        Caminando = true; // Iniciar el camino hacia el destino
    }
}