using UnityEngine;

public class UI_Loja_Manager : MonoBehaviour
{
    public GameObject loja; // Arraste o Empty GameObject aqui
    public GameObject player; // Arraste o Empty GameObject aqui
    public void fechar()
    {
        loja.SetActive(false);
        player.SetActive(true);
    }
    public void buffs()
    {
        
    }
}
