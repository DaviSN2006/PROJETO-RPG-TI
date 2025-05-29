using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuReal2;    // Refer�ncia para o segundo menu
    public AudioSource musicaDeFundo; // Refer�ncia para o componente AudioSource

    // M�todo que come�a o segundo menu
    public void StartMenu2()
    {
        menuReal2.SetActive(true);   // Ativa o segundo menu
        musicaDeFundo.Play();        // Come�a a m�sica de fundo
    }

    // M�todo para iniciar o jogo (carregar uma cena)
    public void Jogar()
    {
        SceneManager.LoadScene("IlhaLoop");  // Substitua pelo nome da sua cena de jogo
    }

    // M�todo para sair do jogo
    public void Sair()
    {
        Debug.Log("Jogo fechou.");
        Application.Quit();  
    }

    public void StopMusica()
    {
        musicaDeFundo.Stop();        // Para a m�sica quando sair do menu
    }
}
