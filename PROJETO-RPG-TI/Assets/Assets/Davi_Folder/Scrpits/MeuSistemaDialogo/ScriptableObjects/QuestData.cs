using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestData", menuName = "Quest/QuestData")]
public class QuestData : ScriptableObject
{
    public string questTitle; // Título da missão
    [TextArea(3, 10)]
    public string questDescription; // Descrição da missão
    public QuestObjective[] objectives; // Lista de objetivos da missão
    public bool isCompleted; // Missão foi concluída?
    public int moneyReward; // Adicione esta linha para a recompensa em dinheiro

    // Método para reiniciar o progresso da missão
    public void ResetProgress()
    {
        isCompleted = false;
        foreach (var objective in objectives)
        {
            objective.currentAmount = 0;
        }
    }
}

[System.Serializable]
public class QuestObjective
{
    public enum ObjectiveType
    {
        CollectItem,
        DefeatEnemy,
        TalkToNPC,
        ExploreArea
    }

    public ObjectiveType objectiveType; // Tipo de objetivo
    public string targetID; // ID do alvo (item, inimigo, NPC, área)
    public int requiredAmount; // Quantidade necessária
    public int currentAmount; // Quantidade atual (progresso)
}