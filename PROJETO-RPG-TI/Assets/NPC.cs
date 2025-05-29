using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public string npcName = "Guardião";
    

    public void Interact()
    {
        Debug.Log($"Olá, eu sou {npcName}. Bem-vindo ao nosso mundo!");
        // Aqui você pode iniciar um diálogo ou outra lógica
    }

    public string GetInteractionMessage()
    {
        return "Pressione E para falar";
    }
}
