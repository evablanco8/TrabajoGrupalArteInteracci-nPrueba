using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] puntos;
    public float velocidad = 2f;

    private int puntoActual = 0;

    void Start()
    {
        // Si el primer punto coincide con la posición inicial,
        // se salta automáticamente al siguiente.
        if (puntos.Length > 0)
        {
            if (Vector3.Distance(transform.position, puntos[0].position) < 0.8f)
            {
                puntoActual = 1;
            }
        }
    }

    void Update()
    {
        if (puntos.Length == 0)
            return;

        Transform destino = puntos[puntoActual];

        transform.position = Vector3.MoveTowards(transform.position, destino.position, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, destino.position) < 0.05f)
        {
            Debug.Log("Llego al punto: " + puntoActual);

            puntoActual++;

            if (puntoActual >= puntos.Length)
            {
                puntoActual = 0;
            }

            Debug.Log("Ahora va al punto: " + puntoActual);
        }
    }
}
