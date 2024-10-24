using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject[] UiPages;
    [SerializeField] private GameObject mainMenu, in_Game, pauseMenu, gameOverScreen, settingScreen;

    private GameObject _prePage;

    private void DisableAllPages()
    {
        foreach (GameObject page in UiPages)
        {
            page.SetActive(false);
        }
    }

    private void EnablePage(GameObject page)
    {
        DisableAllPages();
        page.SetActive(true);
    }

    public void BackToMainMenu()
    {
        EnablePage(mainMenu);
    }

    public void StartGame()
    {
        EnablePage(in_Game);
    }

    public void ResumeGame()
    {
        EnablePage(in_Game);
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        EnablePage(pauseMenu);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowSetting(GameObject fromPage)
    {
        _prePage = fromPage;
        EnablePage(settingScreen);
    }

    public void DisableSetting()
    {
        EnablePage(_prePage);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
