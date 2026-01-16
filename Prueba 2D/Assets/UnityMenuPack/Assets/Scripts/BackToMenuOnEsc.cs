using UnityEngine;

public class BackToMenuOnEsc : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<SceneLoader>()?.LoadMainMenu();
        }
    }
}
