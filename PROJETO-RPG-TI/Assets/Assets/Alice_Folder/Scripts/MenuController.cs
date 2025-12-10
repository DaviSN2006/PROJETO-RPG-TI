using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuReal2;    // Referência para o segundo menu
    public GameObject loadingPanel;
    public AudioSource musicaDeFundo; // Referência para o componente AudioSource

    void Start()
    {
        if (!LoadingScreen.instance)
            SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
    }

    // Método que começa o segundo menu
    public void StartMenu2()
    {
        menuReal2.SetActive(true);   // Ativa o segundo menu
        musicaDeFundo.Play();        // Começa a música de fundo
    }

    // Método para iniciar o jogo (carregar uma cena)
    public void Jogar()
    {
        StartCoroutine(LoadSceneAsync());
    }

    // Método para sair do jogo
    public void Sair()
    {
        Debug.Log("Jogo fechou.");
        Application.Quit();  
    }

    public void StopMusica()
    {
        musicaDeFundo.Stop();        // Para a música quando sair do menu
    }

    IEnumerator LoadSceneAsync()
    {
        LoadingScreen.instance?.ShowLoadingScreen();
        yield return new WaitForSecondsRealtime(0.2f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("IlhaLoop"); // Substitua pelo nome da sua cena de jogo
        while (!asyncLoad.isDone)
        {
            Debug.Log("AsyncLoadCall");
            yield return null;
        }
    }
}
