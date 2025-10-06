using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public void PurchaseItem(ItemData item)
    {
        if (PlayerEconomy.Instance.HasEnoughMoney(item.price))
        {
            if (PlayerEconomy.Instance.RemoveMoney(item.price))
            {
                // Adiciona o item ao inventário do jogador
                InventorySystem.Instance.AddItem(item);
                Debug.Log($"Comprado: {item.itemName} por {item.price}");
            }
        }
        else
        {
            Debug.Log("Dinheiro insuficiente!");
        }
    }
}

[System.Serializable]
public class ItemDataShop
{
    public string itemName;
    public int price;
    public Sprite icon;
    // Outras propriedades do item...
}