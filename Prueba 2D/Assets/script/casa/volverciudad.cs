using UnityEngine;
using UnityEngine.SceneManagement;

public class volverciudad : MonoBehaviour
{
    private const string Tag = "Player";
    public string nombrescena;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombrescena);
        }
        
    }
}
