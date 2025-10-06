using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI quantityText;
    public GameObject equippedIndicator;

    public void Setup(InventoryItem item)
    {
        if (iconImage != null)
        {
            iconImage.sprite = item.itemData.icon;
            iconImage.enabled = true;
        }

        if (quantityText != null)
        {
            quantityText.text = item.quantity > 1 ? item.quantity.ToString() : "";
        }

        if (equippedIndicator != null)
        {
            //equippedIndicator.SetActive(EquipmentSystem.Instance.IsEquipped(item.itemData));
        }
    }
}