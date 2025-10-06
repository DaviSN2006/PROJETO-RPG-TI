using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    [Header("Configurações")]
    public int maxSlots = 20; // Número máximo de slots no inventário
    public bool infiniteCapacity = false; // Modo sem limites

    [Header("UI Referências")]
    public GameObject inventoryPanel;
    public Transform itemsContainer;
    public GameObject itemSlotPrefab;
    public TextMeshProUGUI capacityText;

    private List<InventoryItem> items = new List<InventoryItem>();
    private bool isInventoryOpen = false;

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
        UpdateInventoryUI();
        CloseInventory(); // Fecha o inventário no início
    }

    private void Update()
    {
        // Tecla para abrir/fechar inventário (padrão: Tab)
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        if (isInventoryOpen)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        UpdateInventoryUI();
        isInventoryOpen = true;
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        isInventoryOpen = false;
    }

    // Adiciona um item ao inventário
    public bool AddItem(ItemData itemData, int quantity = 1)
    {
        // Verifica se há espaço no inventário
        if (!infiniteCapacity && items.Count >= maxSlots && !HasItem(itemData.itemID))
        {
            Debug.Log("Inventário cheio!");
            return false;
        }

        // Verifica se o item já existe no inventário (e é acumulável)
        if (itemData.isStackable)
        {
            InventoryItem existingItem = items.Find(i => i.itemData.itemID == itemData.itemID);
            if (existingItem != null)
            {
                existingItem.quantity += quantity;
                UpdateInventoryUI();
                return true;
            }
        }

        // Adiciona novo item
        items.Add(new InventoryItem(itemData, quantity));
        UpdateInventoryUI();
        return true;
    }

    // Remove um item do inventário
    public bool RemoveItem(string itemID, int quantity = 1)
    {
        InventoryItem itemToRemove = items.Find(i => i.itemData.itemID == itemID);

        if (itemToRemove != null)
        {
            if (itemToRemove.quantity > quantity)
            {
                itemToRemove.quantity -= quantity;
            }
            else
            {
                items.Remove(itemToRemove);
            }

            UpdateInventoryUI();
            return true;
        }

        return false;
    }

    // Verifica se o jogador tem um item
    public bool HasItem(string itemID, int minQuantity = 1)
    {
        InventoryItem item = items.Find(i => i.itemData.itemID == itemID);
        return item != null && item.quantity >= minQuantity;
    }

    // Usa um item do inventário
    public void UseItem(string itemID)
    {
        InventoryItem item = items.Find(i => i.itemData.itemID == itemID);

        if (item != null)
        {
            // Aplica o efeito do item
            switch (item.itemData.itemType)
            {
                case ItemType.Consumable:
                    ApplyConsumableEffect(item.itemData);
                    RemoveItem(itemID);
                    break;
                
                case ItemType.Equipment:
                    EquipItem(item.itemData);
                    break;
                
                case ItemType.Quest:
                    // Itens de missão geralmente não são usáveis
                    Debug.Log("Este é um item de missão");
                    break;
            }
        }
    }

    private void ApplyConsumableEffect(ItemData item)
    {
        // Implemente os efeitos dos consumíveis (vida, stamina, etc)
        Debug.Log($"Usando {item.itemName} - Efeito: {item.consumableEffect}");

        // Exemplo: recuperar vida
        // PlayerHealth.Instance.Heal(item.effectAmount);
    }

    private void EquipItem(ItemData item)
    {
        // Implemente o sistema de equipamento
        Debug.Log($"Equipando {item.itemName}");
        
        // EquipmentSystem.Instance.Equip(item);
    }

    // Atualiza a UI do inventário
    private void UpdateInventoryUI()
    {
        // Limpa todos os slots
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }

        // Preenche com os itens atuais
        foreach (InventoryItem item in items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemsContainer);
            ItemSlotUI slotUI = slot.GetComponent<ItemSlotUI>();

            if (slotUI != null)
            {
                slotUI.Setup(item);
                
                // Configura o botão para usar o item
                Button slotButton = slot.GetComponent<Button>();
                if (slotButton != null)
                {
                    string itemID = item.itemData.itemID; // Cópia para o closure
                    slotButton.onClick.AddListener(() => UseItem(itemID));
                }
            }
        }

        // Atualiza texto de capacidade
        if (capacityText != null)
        {
            capacityText.text = infiniteCapacity ? 
                $"Itens: {items.Count}" : 
                $"Itens: {items.Count}/{maxSlots}";
        }
    }

    // Retorna a lista de itens (útil para salvar o jogo)
    public List<InventoryItem> GetItems()
    {
        return items;
    }

    // Carrega itens (útil para carregar o jogo)
    public void LoadItems(List<InventoryItem> loadedItems)
    {
        items = loadedItems;
        UpdateInventoryUI();
    }
}

[System.Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int quantity;

    public InventoryItem(ItemData data, int qty = 1)
    {
        itemData = data;
        quantity = qty;
    }
}

[System.Serializable]
public class ItemData
{
    public string itemID; // Identificador único
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;
    public ItemType itemType;
    public bool isStackable = true;
    public int maxStack = 99;

    // Para itens consumíveis
    public ConsumableEffect consumableEffect;
    public float effectAmount;

    // Para equipamentos
    public EquipmentSlot equipSlot;
    public int armorValue;
    public int damageValue;

    // Para itens de venda
    public int price;
}

public enum ItemType
{
    Consumable,
    Equipment,
    Quest,
    Miscellaneous
}

public enum ConsumableEffect
{
    Health,
    Mana,
    Stamina,
    Experience
}

public enum EquipmentSlot
{
    Head,
    Chest,
    Hands,
    Legs,
    Feet,
    Weapon,
    Shield,
    Accessory
}