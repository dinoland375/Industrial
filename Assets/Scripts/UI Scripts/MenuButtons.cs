using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private SceneTransition sceneTransition;

    [SerializeField] private Button playButton = null;
    [SerializeField] private Button exitButton = null;

    private void Awake()
    {
        if (playButton != null)
            playButton.onClick.AddListener(PlayGame);

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitTheGame);
    }

    private void PlayGame()
    {
        sceneTransition.SwitchToScene("Game");
    }

    private void ExitTheGame()
    {
        Application.Quit();
    }
}
