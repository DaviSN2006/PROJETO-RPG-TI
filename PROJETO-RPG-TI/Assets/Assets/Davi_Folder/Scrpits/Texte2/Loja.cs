using UnityEngine;

public class Loja : MonoBehaviour
{
    public GameObject loja; // Arraste o Empty GameObject aqui
    public GameObject player; // Arraste o Empty GameObject aqui

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // Certifique-se de que o jogador tem a tag "Player"
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                loja.SetActive(true);
                player.SetActive(false);
            }
        }
    }
}
