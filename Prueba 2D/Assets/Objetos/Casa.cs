using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CasaScript : MonoBehaviour
{

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
        if (collision.CompareTag("Player"))
        {
            enZona = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enZona = false;
        }
    }

    void Update()
    {
        if (tiempoEspera==true && vision==true)
        {
           GetComponent<SpriteRenderer>().enabled = false;
        }
        if (vision==true && enZona==true)
        {
            SceneManager.LoadScene("Nivel2");
        }
    }
}
