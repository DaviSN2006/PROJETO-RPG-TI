using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // Singleton para acesso global

    public GameObject dialoguePanel; // Painel de diálogo
    public TMP_Text dialoguerNameText; // Texto do nome do autor do diálogo (usando TextMeshPro)
    public TMP_Text dialogueSentenceText; // Texto do diálogo (usando TextMeshPro)
    public Button nextButton; // Botão para avançar o diálogo
    public Image inputHeldFeedbackImage;

    private DialogueData currentDialogueData;
    private int currentLineIndex = 0;

    private bool inputBeingHeld = false;
    private float inputHeldTimeMax = 1;
    private float inputHeldTimeCurrent = 0;

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
        nextButton?.onClick.AddListener(NextDialogueLine);
        dialoguePanel?.SetActive(false);
    }

    private void Update()
    {
        if (inputBeingHeld)
        {
            inputHeldTimeCurrent += Time.deltaTime;
            inputHeldFeedbackImage.fillAmount = inputHeldTimeCurrent / inputHeldTimeMax;
        }
    }

    public void StartDialogue(DialogueData dialogueData, QuestData questData = null)
    {
        currentDialogueData = dialogueData;
        currentLineIndex = 0;

        dialoguePanel.SetActive(true);

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
        if (currentLineIndex < currentDialogueData.dialogueSentences.Length)
        {
            dialoguerNameText.text = currentDialogueData.dialogueSentences[currentLineIndex].dialoguerName + ":";
            dialogueSentenceText.text = currentDialogueData.dialogueSentences[currentLineIndex].sentence;
        }
        else
        {
            EndDialogue(); // Fecha o diálogo quando todas as falas forem exibidas
        }
    }

    public void NextDialogueLine()
    {
        if (!currentDialogueData)
        {
            EndDialogue();
            return;
        }

        currentLineIndex++;
        ShowCurrentLine();

        CancelNextDialogueInputHeld();
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        currentDialogueData = null;
        currentLineIndex = 0;
    }

    // Método para verificar se o diálogo está ativo
    public bool IsDialogueActive()
    {
        return dialoguePanel.activeInHierarchy;
    }

    public void StartNextDialogueInputHeld(float time)
    {
        inputBeingHeld = true;
        inputHeldTimeMax = time;
        inputHeldTimeCurrent = 0;
    }

    public void CancelNextDialogueInputHeld()
    {
        inputBeingHeld = false;
        inputHeldFeedbackImage.fillAmount = inputHeldTimeCurrent = 0;
    }
}