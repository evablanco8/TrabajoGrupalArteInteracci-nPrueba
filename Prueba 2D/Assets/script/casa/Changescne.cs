using UnityEngine;
using UnityEngine.SceneManagement;

public class CartelScript : MonoBehaviour
{
    public bool enZona;
    public GameObject textoCartel;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enZona = true;
            textoCartel.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enZona = false;
            textoCartel.SetActive(false);
        }
    }
    void Update()
    {
        //que se tenga que pulsar una tecla para cambiar de escena
        if (Input.GetKeyDown(KeyCode.E) && enZona==true)
        {
            //cambiar de escena
            SceneManager.LoadScene("telecinematica");
        }
    }
}