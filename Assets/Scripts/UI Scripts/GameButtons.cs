using UnityEngine.UI;
using UnityEngine;

public class GameButtons : MonoBehaviour
{
    [SerializeField] private SceneTransition sceneTransition;

    [SerializeField] private GameObject infoMenu = null;
    [SerializeField] private GameObject pauseMenu = null;

    [SerializeField] private Button infoMenuButton = null;
    [SerializeField] private Button closeInfoMenuButton = null; 

    [SerializeField] private Button pauseMenuButton = null;
    [SerializeField] private Button backButton = null;
    [SerializeField] private Button restartLevelOneButton = null;
    [SerializeField] private Button exitButton = null;

    [SerializeField] private Button continueButton = null;
    [SerializeField] private Button restartLevelTwoButton = null;
    [SerializeField] private Button exitToMenuButton = null;


    private void Awake()
    {
        if (infoMenuButton != null) 
            infoMenuButton.onClick.AddListener(InfoMenu);
        
        if (closeInfoMenuButton != null)
            closeInfoMenuButton.onClick.AddListener(CloseInfoMenu);

        if (pauseMenuButton != null)
            pauseMenuButton.onClick.AddListener(PauseMenu);

        if(continueButton != null)
            continueButton.onClick.AddListener(ContinueGame);

        if (exitToMenuButton != null) 
            exitToMenuButton.onClick.AddListener(ExitTheGame);

        if (backButton != null) 
            backButton.onClick.AddListener(BackToGame);

        if (restartLevelOneButton != null)
            restartLevelOneButton.onClick.AddListener(RestartLevelOne);

        if (restartLevelTwoButton != null)
            restartLevelTwoButton.onClick.AddListener(RestartLevelTwo);

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitTheGame);
    }

    private void InfoMenu()
    {
        Time.timeScale = 0f;

        infoMenu.SetActive(true);
    }

    private void CloseInfoMenu()
    {
        Time.timeScale = 1.0f
            ;
        infoMenu.SetActive(false);
    }

    private void ContinueGame()
    {
        sceneTransition.SwitchToScene("ContinuationOfTheGame");
    }

    private void PauseMenu()
    {
        Time.timeScale = 0f;

        pauseMenu.SetActive(true);
    }

    private void BackToGame()
    {
        Time.timeScale = 1.0f;

        pauseMenu.SetActive(false);
    }

    private void RestartLevelOne()
    {
        Time.timeScale = 1.0f;

        sceneTransition.SwitchToScene("Game");
    }

    private void RestartLevelTwo()
    {
        Time.timeScale = 1.0f;

        sceneTransition.SwitchToScene("ContinuationOfTheGame");
    }

    private void ExitTheGame()
    {
        Time.timeScale = 1.0f;

        sceneTransition.SwitchToScene("Menu");
    }
}
