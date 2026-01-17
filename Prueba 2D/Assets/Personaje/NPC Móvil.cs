using UnityEngine;

public class NPCMovimiento : MonoBehaviour
{
    public Transform[] puntos;   // aquí pones 3 puntos
    public float velocidad = 2f;

    private int puntoActual = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoverNPC();
    }

    public void MoverNPC()
    {
        Vector3 destino = puntos[puntoActual].position;

        Vector3 direccion = destino - transform.position;

        CambiarAnimacion(direccion);

        transform.position = Vector3.MoveTowards(
            transform.position,
            destino,
            velocidad * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, destino) <= 0.01f)
        {
            puntoActual = puntoActual + 1;

            if (puntoActual >= puntos.Length)
            {
                puntoActual = 0;
            }
        }
    }

    public void CambiarAnimacion(Vector3 direccion)
    {
        if (Mathf.Abs(direccion.x) > Mathf.Abs(direccion.y))
        {
            if (direccion.x > 0)
            {
                animator.Play("Movimiento Derecha");
            }
            else
            {
                animator.Play("Movimiento Izquierda");
            }
        }
        else
        {
            if (direccion.y > 0)
            {
                animator.Play("Movimiento Detrás");
            }
            else
            {
                animator.Play("Movimiento Frente");
            }
        }
    }
}
