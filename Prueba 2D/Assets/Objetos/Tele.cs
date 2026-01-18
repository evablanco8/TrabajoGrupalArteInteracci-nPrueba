using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleScript : MonoBehaviour
{
    public bool enZona;
    public GameObject Canvas;

    void Start()
    {
        Canvas.SetActive(false);
        enZona = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enZona = true;
            Canvas.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enZona = false;
            Canvas.SetActive(false);
        }
    }
    void Update()
    {
        //que se tenga que pulsar una tecla para cambiar de escena
        if (Input.GetKeyDown(KeyCode.I) && enZona==true)
        {
            //cambiar de escena
            SceneManager.LoadScene("telecinematica");
        }
    }
}
