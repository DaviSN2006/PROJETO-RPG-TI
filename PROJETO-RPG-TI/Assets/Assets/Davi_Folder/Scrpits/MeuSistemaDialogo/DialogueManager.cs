using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // Singleton para acesso global

    public GameObject dialoguePanel; // Painel de diálogo
    public TextMeshProUGUI dialogueText; // Texto do diálogo (usando TextMeshPro)
    public Button nextButton; // Botão para avançar o diálogo

    private DialogueData currentDialogueData;
    private int currentLineIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Configura o botão "Próximo" para avançar o diálogo
        nextButton.onClick.AddListener(NextDialogueLine);
        nextButton.gameObject.SetActive(false); // Esconde o botão no início
    }

    public void StartDialogue(DialogueData dialogueData, QuestData questData)
    {
        currentDialogueData = dialogueData;
        currentLineIndex = 0;

        dialoguePanel.SetActive(true);
        nextButton.gameObject.SetActive(true); // Mostra o botão "Próximo"

        // Exibe a primeira fala do diálogo
        ShowCurrentLine();

        // Inicia a missão, se houver
        if (questData != null && !questData.isCompleted)
        {
            QuestManager.Instance.StartQuest(questData);
        }
    }

    private void ShowCurrentLine()
    {
        if (currentLineIndex < currentDialogueData.dialogueLines.Length)
        {
            dialogueText.text = currentDialogueData.dialogueLines[currentLineIndex];
        }
        else
        {
            EndDialogue(); // Fecha o diálogo quando todas as falas forem exibidas
        }
    }

    public void NextDialogueLine()
    {
        currentLineIndex++;
        ShowCurrentLine();
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        nextButton.gameObject.SetActive(false); // Esconde o botão "Próximo"
        currentDialogueData = null;
        currentLineIndex = 0;
    }

    // Método para verificar se o diálogo está ativo
    public bool IsDialogueActive()
    {
        return dialoguePanel.activeInHierarchy;
    }
}