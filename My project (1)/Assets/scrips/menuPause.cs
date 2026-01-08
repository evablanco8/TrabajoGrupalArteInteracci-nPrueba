using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{

    public GameObject menuPause;

    
    void Start()
    {
        menuPause.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DesactivarMenu();
        }
    }

    public void DesactivarMenu()
    {
        menuPause.SetActive(!menuPause.activeSelf);
        Time.timeScale = menuPause.activeSelf ? 0 : 1;
    }

    public void Continuar()
    {
        DesactivarMenu();
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuPrincipal");
    }

}

