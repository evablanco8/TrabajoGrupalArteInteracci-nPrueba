using UnityEngine;
using UnityEngine.SceneManagement;

public class CartelScript : MonoBehaviour
{
    public bool enZona;
    public bool vision;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enZona = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enZona = false;
        }
    }
    void Update()
    {
        if (vision==false && enZona==true)
        {
            SceneManager.LoadScene("Nivel 2");
        }
    }
}
