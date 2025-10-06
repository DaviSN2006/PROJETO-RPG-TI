using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    private bool isPaused = false;

    void Update()
    {
        // Verifica se a tecla I foi pressionada
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        // Inverte o estado do inventário
        isPaused = !isPaused;
        
        // Ativa/desativa o painel do inventário
        inventoryPanel.SetActive(isPaused);
        
        // Pausa/despausa o jogo
        Time.timeScale = isPaused ? 0f : 1f;
        
        // Alterna o cursor (opcional)
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }
}