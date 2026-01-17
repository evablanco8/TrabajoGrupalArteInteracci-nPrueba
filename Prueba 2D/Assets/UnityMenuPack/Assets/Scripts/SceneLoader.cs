using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLevel() => SceneManager.LoadScene("Level01");
    public void LoadCredits() => SceneManager.LoadScene("Credits");
    public void LoadMainMenu() => SceneManager.LoadScene("MainMenu");
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
