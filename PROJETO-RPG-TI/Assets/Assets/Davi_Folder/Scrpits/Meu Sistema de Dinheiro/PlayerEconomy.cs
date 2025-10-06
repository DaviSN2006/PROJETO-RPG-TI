using UnityEngine;
using TMPro;

public class PlayerEconomy : MonoBehaviour
{
    public static PlayerEconomy Instance;

    [Header("UI References")]
    public TextMeshProUGUI moneyText; // Texto para mostrar o dinheiro na UI
    public GameObject moneyChangePrefab; // Prefab para mostrar "+X" ou "-X" quando o dinheiro muda

    [Header("Settings")]
    public int initialMoney = 100; // Dinheiro inicial do jogador

    private int currentMoney;

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
        currentMoney = initialMoney;
        UpdateMoneyUI();
    }

    // Adiciona dinheiro ao jogador
    public void AddMoney(int amount)
    {
        if (amount <= 0) return;

        currentMoney += amount;
        UpdateMoneyUI();
        ShowMoneyChange(amount, true);
        
        Debug.Log($"Added {amount} money. Total: {currentMoney}");
    }

    // Remove dinheiro do jogador
    public bool RemoveMoney(int amount)
    {
        if (amount <= 0) return false;

        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            UpdateMoneyUI();
            ShowMoneyChange(amount, false);
            
            Debug.Log($"Removed {amount} money. Remaining: {currentMoney}");
            return true;
        }
        
        Debug.Log("Not enough money!");
        return false;
    }

    // Verifica se o jogador tem dinheiro suficiente
    public bool HasEnoughMoney(int amount)
    {
        return currentMoney >= amount;
    }

    // Atualiza a UI do dinheiro
    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = currentMoney.ToString();
        }
    }

    // Mostra efeito visual de mudança de dinheiro
    private void ShowMoneyChange(int amount, bool isPositive)
    {
        if (moneyChangePrefab == null) return;

        GameObject changeText = Instantiate(moneyChangePrefab, moneyText.transform.parent);
        TextMeshProUGUI textComponent = changeText.GetComponent<TextMeshProUGUI>();
        
        if (textComponent != null)
        {
            textComponent.text = $"{(isPositive ? "+" : "-")}{amount}";
            textComponent.color = isPositive ? Color.green : Color.red;
        }

        Destroy(changeText, 2f); // Destroi após 2 segundos
    }

    // Método para obter o dinheiro atual (pode ser útil para salvar o jogo)
    public int GetCurrentMoney()
    {
        return currentMoney;
    }

    // Método para definir o dinheiro (pode ser útil para carregar o jogo)
    public void SetMoney(int amount)
    {
        currentMoney = Mathf.Max(0, amount);
        UpdateMoneyUI();
    }
}