using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] dialogueLines; // Linhas de diálogo

    // Método para obter uma fala específica
    public string GetDialogueLine(int index)
    {
        if (index >= 0 && index < dialogueLines.Length)
        {
            return dialogueLines[index];
        }
        return null; // Retorna null se o índice for inválido
    }
}