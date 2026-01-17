using UnityEngine;
using UnityEngine.UI;

public class GameManager2D : MonoBehaviour
{
    public static GameManager2D Instance { get; private set; }
    public GameObject deathPanel;
    public Button quitButton;
    public SceneLoader sceneLoader;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void PlayerDied()
    {
        if (deathPanel != null) deathPanel.SetActive(true);
        if (quitButton != null && sceneLoader != null)
        {
            quitButton.onClick.RemoveAllListeners();
            quitButton.onClick.AddListener(sceneLoader.LoadMainMenu);
        }
    }
}
