using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    public Animator animator;

    public string ultimaDireccion = "Abajo";

    void Update()
    {
        Movimiento();
        Animaciones();
    }

    public void Movimiento()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            ultimaDireccion = "Derecha";
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            ultimaDireccion = "Izquierda";
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            ultimaDireccion = "Arriba";
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            ultimaDireccion = "Abajo";
        }
    }

    public void Animaciones()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.Play("Movimiento Derecha");
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.Play("Movimiento Izquierda");
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.Play("Movimiento Detrás");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.Play("Movimiento Frente");
        }
        else
        {
            if (ultimaDireccion == "Derecha")
            {
                animator.Play("Reposo Derecha");
            }
            else if (ultimaDireccion == "Izquierda")
            {
                animator.Play("Reposo Izquierda");
            }
            else if (ultimaDireccion == "Arriba")
            {
                animator.Play("Reposo Detrás");
            }
            else if (ultimaDireccion == "Abajo")
            {
                animator.Play("Reposo Frente");
            }
        }
    }
}