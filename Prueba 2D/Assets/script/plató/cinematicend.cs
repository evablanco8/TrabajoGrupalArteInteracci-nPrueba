using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class cinematicend : MonoBehaviour
{
    public PlayableDirector director;
    public string nextSceneName;

    private void OnEnable()
    {
        director.stopped += OnCinematicFinished;
    }

    private void OnDisable()
    {
        director.stopped -= OnCinematicFinished;
    }

    private void OnCinematicFinished(PlayableDirector pd)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}