using UnityEngine;
using System;

public class Shop : MonoBehaviour
{
    public static Shop Instance { get; private set; }

    public BuffData[] availableBuffs;
    public static event Action<BuffData> OnBuffPurchased;

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

    public void BuyBuff(int buffIndex)
    {
        if (buffIndex < 0 || buffIndex >= availableBuffs.Length)
        {
            Debug.LogWarning("Índice de buff inválido.");
            return;
        }

        BuffData buff = availableBuffs[buffIndex];

        if (HasEnoughCoins(buff.cost))
        {
            OnBuffPurchased?.Invoke(buff);
            DeductCoins(buff.cost);
        }
        else
        {
            Debug.Log("Moedas insuficientes.");
        }
    }

    private bool HasEnoughCoins(int cost)
    {
        int playerCoins = 100; // Exemplo
        return playerCoins >= cost;
    }

    private void DeductCoins(int cost)
    {
        Debug.Log($"Moedas deduzidas: {cost}");
    }
}