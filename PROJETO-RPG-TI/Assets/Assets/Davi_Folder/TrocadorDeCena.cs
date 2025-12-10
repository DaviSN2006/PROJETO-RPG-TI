using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocadorDeCena : MonoBehaviour
{
    public string nomeDaCena;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadSceneAsync());
        }
    }

    IEnumerator LoadSceneAsync()
    {
        LoadingScreen.instance?.ShowLoadingScreen();
        yield return new WaitForSecondsRealtime(0.2f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nomeDaCena);
        while (!asyncLoad.isDone)
        {
            Debug.Log("AsyncLoadCall");
            yield return null;
        }
    }
}