using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuReal2;    // Referência para o segundo menu
    public AudioSource musicaDeFundo; // Referência para o componente AudioSource

    // Método que começa o segundo menu
    public void StartMenu2()
    {
        menuReal2.SetActive(true);   // Ativa o segundo menu
        musicaDeFundo.Play();        // Começa a música de fundo
    }

    // Método para iniciar o jogo (carregar uma cena)
    public void Jogar()
    {
        SceneManager.LoadScene("IlhaLoop");  // Substitua pelo nome da sua cena de jogo
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
}
