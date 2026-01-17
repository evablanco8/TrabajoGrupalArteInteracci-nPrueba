using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CasaScript : MonoBehaviour
{
    [Header("Zonas")]
    public Collider2D zonaVision;
    public Collider2D zonaCasa; 

    public bool enZona;
    public bool tiempoEspera;
    public bool vision;
    
    void Start()
    {
        StartCoroutine(EsperarDosMinutos());
        tiempoEspera = false;
        vision = false;
        enZona =false;
    }
    IEnumerator EsperarDosMinutos()
    {
        yield return new WaitForSeconds(120f); // 2 minutos
        tiempoEspera = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        // ZONA DE VISIÓN (la lejana)
        if (collision == zonaVision)
        {
            vision = true;
        }

        // ZONA DE LA CASA (donde está el objeto)
        if (collision == zonaCasa)
        {
            enZona = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (collision == zonaVision)
        {
            vision = false;
        }

        if (collision == zonaCasa)
        {
            enZona = false;
        }
    }
    void Update()
    {
        if (tiempoEspera==true && vision==true)
        {
           gameObject.SetActive(false);
        }
        if (vision==false && enZona==true)
        {
            SceneManager.LoadScene("Nivel 2");
        }
    }
}
