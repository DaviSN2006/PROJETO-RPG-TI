using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public string npcName = "Guardião";
    public string npcPhrase = "";
    

    public void Interact()
    {
        Debug.Log($"Interagindo com {npcName}: {npcPhrase}");
        // Aqui você pode iniciar um diálogo ou outra lógica
    }

    public string GetInteractionMessage()
    {
        return "Pressione E para falar";
    }
}
