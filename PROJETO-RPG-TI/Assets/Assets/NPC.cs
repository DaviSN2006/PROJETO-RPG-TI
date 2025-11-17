using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public string npcName = "Guardião";
    public string npcPhrase = "";
    public DialogueData dialogueData;

    public GameObject GetGameObject() => gameObject;
    public void Interact()
    {
        Debug.Log($"Interagindo com {npcName}: {npcPhrase}");
        // Aqui você pode iniciar um diálogo ou outra lógica

        if (dialogueData)
        {
            if (DialogueManager.Instance.IsDialogueActive())
                DialogueManager.Instance.NextDialogueLine();
            else
                DialogueManager.Instance.StartDialogue(dialogueData);
        }
    }

    public void StartInteractionInput(float interactionHeldTime)
    {
        if (DialogueManager.Instance.IsDialogueActive())
        {
            DialogueManager.Instance.StartNextDialogueInputHeld(interactionHeldTime);
        }
        else
        {

        }
    }

    public void CancelInteractionInput()
    {
        if (DialogueManager.Instance.IsDialogueActive())
        {
            DialogueManager.Instance.CancelNextDialogueInputHeld();
        }
        else
        {

        }
    }

    public string GetInteractionMessage()
    {
        return "[Pressione E para falar]";
    }
}
