using UnityEngine;

public class ZonaVision : MonoBehaviour
{
    public CasaScript casa;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (casa.tiempoEspera==true && collision.CompareTag("Player"))
        {
            casa.vision = true;
        }
    }
}
