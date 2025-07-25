using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persecucion : MonoBehaviour
{
    public Transform jugador;
    public float velocidad = 2f;
    public float rangoDeteccion = 5f;
    public float distanciaVision = 2f;
    public float radioEnemigo = 0.3f;
    public LayerMask capaObstaculos;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (jugador == null) return;

        Vector2 direccion = (jugador.position - transform.position).normalized;
        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia <= rangoDeteccion)
        {
            RaycastHit2D obstaculo = Physics2D.CircleCast(transform.position, radioEnemigo, direccion, distanciaVision, capaObstaculos);

            if (obstaculo.collider != null)
            {
                Vector2 nuevaDireccion = BuscarRutaLibre(direccion);
                rb.MovePosition(rb.position + nuevaDireccion.normalized * velocidad * Time.deltaTime);
            }
            else
            {
                rb.MovePosition(rb.position + direccion * velocidad * Time.deltaTime);
            }
        }
    }

    Vector2 BuscarRutaLibre(Vector2 direccionOriginal)
    {
        float[] angulos = { 45, -45, 90, -90, 135, -135 };

        foreach (float angulo in angulos)
        {
            Vector2 dirRotada = RotarVector(direccionOriginal, angulo);

            RaycastHit2D hit = Physics2D.CircleCast(
                transform.position,
                radioEnemigo,
                dirRotada,
                distanciaVision,
                capaObstaculos
            );

            Debug.DrawRay(transform.position, dirRotada * distanciaVision, hit.collider == null ? Color.green : Color.red);

            if (hit.collider == null)
            {
                return dirRotada;
            }
        }

        return direccionOriginal * 0.2f;
    }

    Vector2 RotarVector(Vector2 vector, float anguloGrados)
    {
        float rad = anguloGrados * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(
            vector.x * cos - vector.y * sin,
            vector.x * sin + vector.y * cos
        ).normalized;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }
}
