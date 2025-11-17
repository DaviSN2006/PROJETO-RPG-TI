using UnityEngine;

public interface IInteractable
{
    GameObject GetGameObject();
    void Interact(); // O que acontece ao interagir
    void StartInteractionInput(float interactionHeldTime);
    void CancelInteractionInput();

    string GetInteractionMessage(); // Mensagem exibida ao jogador
}
