using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance; // Singleton para acesso global

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

    // Inicia uma missão
    public void StartQuest(QuestData questData)
    {
        if (questData.isCompleted) return;

        Debug.Log("Missão Iniciada: " + questData.questTitle);
        Debug.Log("Descrição: " + questData.questDescription);

        // Inicializa o progresso dos objetivos
        foreach (var objective in questData.objectives)
        {
            objective.currentAmount = 0;
        }
    }

    // Atualiza o progresso de um objetivo
    public void UpdateObjective(QuestData questData, string targetID, int amount)
    {
        if (questData.isCompleted) return;

        foreach (var objective in questData.objectives)
        {
            if (objective.targetID == targetID)
            {
                objective.currentAmount += amount;
                Debug.Log($"Progresso: {objective.currentAmount}/{objective.requiredAmount}");

                // Verifica se o objetivo foi concluído
                if (objective.currentAmount >= objective.requiredAmount)
                {
                    CheckQuestCompletion(questData);
                }
                break;
            }
        }
    }

    // Verifica se todos os objetivos da missão foram concluídos
    private void CheckQuestCompletion(QuestData questData)
    {
        foreach (var objective in questData.objectives)
        {
            if (objective.currentAmount < objective.requiredAmount)
            {
                return; // Ainda há objetivos pendentes
            }
        }

        // Todos os objetivos foram concluídos
        questData.isCompleted = true;
        Debug.Log("Missão Concluída: " + questData.questTitle);
        // Dar recompensa em dinheiro
        if (questData.moneyReward > 0)
        {
            PlayerEconomy.Instance.AddMoney(questData.moneyReward);
        }
    }
}