using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bailarin1 : MonoBehaviour
{
    Rigidbody2D rigidbodyDance;
    bool pasoIzquierda, pasoDerecha;

    float timer = 0f;
    public float velocidad;

    void Start()
    {
        rigidbodyDance = GetComponent<Rigidbody2D>();    
    }
  
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 0.5f) // 1 segundo ha pasado
        {

            if (pasoIzquierda == false && pasoDerecha == false)
            {
                rigidbodyDance.velocity = Vector2.up;//new Vector2(transform.position.x + velocidad, transform.position.y);
                pasoIzquierda = true;
                print("1. true, true");
            }
            else if (pasoIzquierda == true && pasoDerecha == false)
            {
                rigidbodyDance.velocity = Vector2.zero;
                pasoDerecha = true;
                pasoIzquierda = false;
                print("2. true, false");

            }
            else if (pasoIzquierda == false && pasoDerecha == true)
            {
                rigidbodyDance.velocity = Vector2.down;//new Vector2(transform.position.x - velocidad, transform.position.y);//Vector2.left * 5;
                pasoDerecha = true;
                pasoIzquierda = true;
                print("3. false, true");
            }

            else if (pasoIzquierda == true && pasoDerecha == true)
            {
                rigidbodyDance.velocity = Vector2.zero;
                pasoIzquierda = false;
                pasoDerecha = false;
                print("4. true, true");
            }

            //Debug.Log("Ejecutado una vez por segundo");

            timer = 0f; // Reiniciamos el temporizador
        }

    }



}
