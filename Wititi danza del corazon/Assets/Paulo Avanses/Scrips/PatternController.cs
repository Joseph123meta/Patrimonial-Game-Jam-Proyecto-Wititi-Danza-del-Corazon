using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternController : MonoBehaviour
{
    public enum Modo
    {
        Ninguno,
        Coreografia,
        RotacionYEscalado
    }

    public Modo modoSeleccionado = Modo.Ninguno;

    [Header("Variables comunes")]
    public Transform objetoPrincipal; // usado para rotar en coreografía o como centro

    // === COREOGRAFÍA ===
    [System.Serializable]
    public class Bailarin
    {
        public Transform objeto;
        public Transform destino;
        [HideInInspector] public Vector3 posicionInicialActual;
        [HideInInspector] public Vector3 posicionDestinoActual;
    }

    [Header("Configuración Coreografía")]
    public List<Bailarin> bailarines;
    public float velocidadMovimiento = 3f;
    public float velocidadRotacion = 90f;
    public float gradosPorRotacion = 90f;
    private float tolerancia = 0.01f;

    // === ROTACIÓN Y ESCALADO ===
    [Header("Configuración Rotación y Escalado")]
    public bool crecimiento = false;
    public float velocidadDeRotacion = 360f;
    public float velocidadDeCrecimiento = 1f;
    public float cantidadDeCrecimiento = 0.5f;
    private Vector3 escalaInicial;

    void Start()
    {
        if (modoSeleccionado == Modo.Coreografia)
        {
            StartCoroutine(EjecutarCoreografia());
        }
        else if (modoSeleccionado == Modo.RotacionYEscalado)
        {
            escalaInicial = transform.localScale;
        }
    }

    void Update()
    {
        if (modoSeleccionado == Modo.RotacionYEscalado)
        {
            EjecutarRotacionYEscalado();
        }
    }

    // ==========================
    // MODO 1: COREOGRAFÍA
    // ==========================
    IEnumerator EjecutarCoreografia()
    {
        while (true)
        {
            ActualizarPosiciones();

            yield return StartCoroutine(MoverTodos(b => b.posicionDestinoActual));
            yield return StartCoroutine(MoverTodos(b => b.posicionInicialActual));
            yield return StartCoroutine(RotarObjeto(objetoPrincipal, gradosPorRotacion));
        }
    }

    void ActualizarPosiciones()
    {
        foreach (var b in bailarines)
        {
            b.posicionInicialActual = objetoPrincipal.TransformPoint(b.objeto.localPosition);
            b.posicionDestinoActual = objetoPrincipal.TransformPoint(b.destino.localPosition);
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
                    obj.position = destino;
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

    // ==========================
    // MODO 2: ROTACIÓN Y ESCALADO
    // ==========================
    void EjecutarRotacionYEscalado()
    {
        transform.Rotate(0f, 0f, velocidadDeRotacion * Time.deltaTime);

        if (crecimiento)
        {
            float scale = 1 + Mathf.PingPong(Time.time * velocidadDeCrecimiento, cantidadDeCrecimiento);
            transform.localScale = escalaInicial * scale;
        }
        else
        {
            transform.localScale = escalaInicial;
        }
    }
}
