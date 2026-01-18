using UnityEngine;

public class PuntoCinematica : MonoBehaviour
{
    public NPCGuiaTimeline npc;
    public int numeroCinematica;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            npc.ActivarCinematica(numeroCinematica);
            gameObject.SetActive(false);
        }
    }
}
