using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int numeroEscena;
    public GameObject Texto;
    private bool lugar;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && lugar == true)
        {
            SceneManager.LoadScene(numeroEscena);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Texto.SetActive(true);
            lugar = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
         if(Input.GetKeyDown(KeyCode.E))
        {
            Texto.SetActive(false);
            lugar = false;
        }
    }
}
