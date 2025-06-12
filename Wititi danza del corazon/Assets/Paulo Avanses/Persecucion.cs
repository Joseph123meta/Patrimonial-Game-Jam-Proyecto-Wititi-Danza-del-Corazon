using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persecucion : MonoBehaviour
{
    public Transform jugador;
    public float velocidad = 2f;
    public float rangoDeteccion = 5f;

    public float radioEvitar = 0.3f;
    public float distanciaEvitar = 0.5f;

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
            Vector2 puntoOrigen = (Vector2)transform.position + direccion * (radioEvitar + 0.05f);
            RaycastHit2D obstaculo = Physics2D.CircleCast(puntoOrigen, radioEvitar, direccion, distanciaEvitar, capaObstaculos);

            if (obstaculo.collider != null && obstaculo.collider.gameObject != gameObject)
            {
                // Intentar rodear el obstáculo
                Vector2 perpendicularDerecha = Vector2.Perpendicular(direccion).normalized;
                Vector2 puntoDerecha = (Vector2)transform.position + perpendicularDerecha * radioEvitar;
                RaycastHit2D derechaLibre = Physics2D.CircleCast(puntoDerecha, radioEvitar, direccion, distanciaEvitar, capaObstaculos);

                if (derechaLibre.collider == null)
                {
                    rb.MovePosition(rb.position + (direccion + perpendicularDerecha).normalized * velocidad * Time.deltaTime);
                    return;
                }

                Vector2 perpendicularIzquierda = -perpendicularDerecha;
                Vector2 puntoIzquierda = (Vector2)transform.position + perpendicularIzquierda * radioEvitar;
                RaycastHit2D izquierdaLibre = Physics2D.CircleCast(puntoIzquierda, radioEvitar, direccion, distanciaEvitar, capaObstaculos);

                if (izquierdaLibre.collider == null)
                {
                    rb.MovePosition(rb.position + (direccion + perpendicularIzquierda).normalized * velocidad * Time.deltaTime);
                    return;
                }

                // Si no puede evadir, se detiene
                return;
            }

            // No hay obstáculos, ir directo al jugador
            rb.MovePosition(rb.position + direccion * velocidad * Time.deltaTime);
        }
    }

}
