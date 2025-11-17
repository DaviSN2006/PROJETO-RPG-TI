using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [System.Serializable]
    public struct DialogueSentence
    {
        public string dialoguerName;
        public string sentence;
    };

    [SerializeField]
    public DialogueSentence[] dialogueSentences; // Linhas de Diálogo com o Autor e sua Frase

    // Método para obter uma fala específica
    public DialogueSentence GetDialogueSentenceN(int indexN)
    {
        if (indexN >= 0 && indexN < dialogueSentences.Length)
        {
            return dialogueSentences[indexN];
        }
        return new DialogueSentence(); // Retorna null se o índice for inválido
    }
}