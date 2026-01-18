using UnityEngine;
using UnityEngine.Playables;

public class NPCGuiaTimeline : MonoBehaviour
{
    public CasaScript casa;
    public Transform jugador;
    public Animator animator;

    public PlayableDirector[] cinematicas; // una por punto

    public bool enCinematica;

    void Start()
    {
        gameObject.SetActive(false);
        enCinematica = false;
    }

    void Update()
    {
        if (casa.tiempoEspera == true)
        {
            gameObject.SetActive(true);
        }

        if (enCinematica == false)
        {
            MirarJugadorReposo();
        }
    }

    void MirarJugadorReposo()
    {
        Vector3 direccion = jugador.position - transform.position;

        if (Mathf.Abs(direccion.x) > Mathf.Abs(direccion.y))
        {
            if (direccion.x > 0)
            {
                animator.Play("Reposo Derecha");
            }
            else
            {
                animator.Play("Reposo Izquierda");
            }
        }
        else
        {
            if (direccion.y > 0)
            {
                animator.Play("Reposo Detrás");
            }
            else
            {
                animator.Play("Reposo Frente");
            }
        }
    }

    public void ActivarCinematica(int numero)
    {
        if (enCinematica == true)
        {
            return;
        }

        enCinematica = true;
        cinematicas[numero].Play();
    }

    public void FinCinematica()
    {
        enCinematica = false;
    }
}
