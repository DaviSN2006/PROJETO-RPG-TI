using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    void Start()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnPauseMenu()
    {
        if (!pauseMenuPanel.activeSelf)
        {
            pauseMenuPanel.SetActive(true);
            Time.timeScale = 0.02f;
        }
        else
        {
            pauseMenuPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        LoadingScreen.instance?.ShowLoadingScreen();
        yield return new WaitForSecondsRealtime(0.2f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        while (!asyncLoad.isDone)
        {
            Debug.Log("AsyncLoadCall");
            yield return null;
        }
    }
}
