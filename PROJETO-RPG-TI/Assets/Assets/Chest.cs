using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private bool isOpen = false;

    public void Interact()
    {
        if (!isOpen)
        {
            OpenChest();
        }
        else
        {
            Debug.Log("Baú já está aberto!");
        }
    }

    public string GetInteractionMessage()
    {
        return isOpen ? "Baú já está aberto" : "Pressione E para abrir o baú";
    }

    private void OpenChest()
    {
        isOpen = true;
        Debug.Log("Você abriu o baú");
        // Adicione animações ou lógica de recompensa aqui
    }
}
