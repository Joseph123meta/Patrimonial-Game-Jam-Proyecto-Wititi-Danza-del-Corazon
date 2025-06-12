using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coreografia : MonoBehaviour
{
    [System.Serializable]
    public class Bailarin
    {
        public Transform objeto;
        public Transform destino;
        [HideInInspector] public Vector3 posicionInicialActual;
        [HideInInspector] public Vector3 posicionDestinoActual;
    }

    public List<Bailarin> bailarines;
    public Transform objetoAGirar;
    public float velocidadMovimiento = 3f;
    public float velocidadRotacion = 90f; // grados por segundo
    public float gradosPorRotacion = 90f;
    private float tolerancia = 0.01f;

    void Start()
    {
        StartCoroutine(EjecutarCoreografia());
    }

    IEnumerator EjecutarCoreografia()
    {
        while (true)
        {
            // 1. Actualizar posiciones dinámicas
            ActualizarPosiciones();

            // 2. Mover a destinos (todos esperan a los demás)
            yield return StartCoroutine(MoverTodos(b => b.posicionDestinoActual));

            // 3. Mover de vuelta (todos esperan también)
            yield return StartCoroutine(MoverTodos(b => b.posicionInicialActual));

            // 4. Rotar el objeto
            yield return StartCoroutine(RotarObjeto(objetoAGirar, gradosPorRotacion));
        }
    }

    void ActualizarPosiciones()
    {
        foreach (var b in bailarines)
        {
            // Usamos posiciones locales y convertimos a global
            b.posicionInicialActual = objetoAGirar.TransformPoint(b.objeto.localPosition);
            b.posicionDestinoActual = objetoAGirar.TransformPoint(b.destino.localPosition);
        }
    }

    IEnumerator MoverTodos(System.Func<Bailarin, Vector3> objetivo)
    {
        bool[] haLlegado = new bool[bailarines.Count];

        while (true)
        {
            bool todosLlegaron = true;

            for (int i = 0; i < bailarines.Count; i++)
            {
                if (haLlegado[i]) continue;

                Vector3 destino = objetivo(bailarines[i]);
                Transform obj = bailarines[i].objeto;

                if ((obj.position - destino).sqrMagnitude > tolerancia * tolerancia)
                {
                    obj.position = Vector3.MoveTowards(obj.position, destino, velocidadMovimiento * Time.deltaTime);
                    todosLlegaron = false;
                }
                else
                {
                    obj.position = destino; // fijar posición exacta al llegar
                    haLlegado[i] = true;
                }
            }

            if (todosLlegaron)
                break;

            yield return null;
        }
    }

    IEnumerator RotarObjeto(Transform objeto, float grados)
    {
        float rotado = 0f;
        while (rotado < grados)
        {
            float paso = Mathf.Min(velocidadRotacion * Time.deltaTime, grados - rotado);
            objeto.Rotate(0f, 0f, paso);
            rotado += paso;
            yield return null;
        }
    }
}
