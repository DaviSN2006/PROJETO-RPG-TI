using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player encontrado! Pedindo para o GameManager iniciar batalha...");
            Destroy(this.gameObject); // Destroi o inimigo que colidiu com o jogador
            GameManager.Instance.StartBattle(this.gameObject);
        }
    }
}
