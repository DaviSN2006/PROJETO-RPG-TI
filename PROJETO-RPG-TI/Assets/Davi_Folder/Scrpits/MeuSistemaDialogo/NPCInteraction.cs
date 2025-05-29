using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public DialogueData dialogueData; // Dados do diálogo
    public QuestData questData; // Dados da missão
    public float interactionRange = 3f; // Alcance da interação

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Encontra o jogador pela tag
    }

    private void Update()
    {
        if (player == null) return;

        // Calcula a distância entre o NPC e o jogador
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Verifica se o jogador está no alcance
        if (distance <= interactionRange)
        {
            // Mostra feedback visual (opcional)
            Debug.Log("Pressione E para interagir");

            // Inicia o diálogo se o jogador pressionar a tecla E
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.Instance.StartDialogue(dialogueData, questData);
            }
        }
        else
        {
            // Fecha o diálogo se o jogador sair do alcance
            if (DialogueManager.Instance.IsDialogueActive())
            {
                DialogueManager.Instance.EndDialogue();
            }
        }
    }

    // Método para desenhar o alcance de interação no Editor (opcional)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}