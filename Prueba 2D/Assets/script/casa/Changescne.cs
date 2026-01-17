using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class InteractTriggerScene : MonoBehaviour
{
    public string sceneToLoad; // Escena destino
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange)
        {
#if ENABLE_INPUT_SYSTEM
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
                SceneManager.LoadScene(sceneToLoad);
#else
            if (Input.GetKeyDown(KeyCode.E))
                SceneManager.LoadScene(sceneToLoad);
#endif
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Jugador en rango. Presiona E");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}