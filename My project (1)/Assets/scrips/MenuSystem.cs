using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Nivel");
    }
    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }
    public void Salir()
    {
        Debug.Log("Saliendo del juego..."); 
        Application.Quit(); 
    }
}
