using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingPanel;

    public static LoadingScreen instance;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this); 
            
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.activeSceneChanged += OnActiveSceneChanged;

            return;
        }
        Destroy(gameObject);
    }

    void OnSceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
    {
        loadingPanel.SetActive(false);
    }

    void OnActiveSceneChanged(Scene scene1,Scene scene2)
    {
    }

    public void ShowLoadingScreen()
    {
        loadingPanel.SetActive(true);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }
}
