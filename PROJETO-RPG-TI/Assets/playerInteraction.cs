using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2.0f; // Alcance da interação
    public LayerMask interactionLayer;    // Camada para objetos interativos
    private IInteractable currentInteractable;

    // Input System
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Interact.performed += _ =>
        {
            Debug.Log("Botão de interação pressionado.");
            HandleInteraction();
        };
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Update()
    {
        DetectInteractable();
    }

    private void DetectInteractable()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRange, interactionLayer);

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                var interactable = hit.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    if (currentInteractable != interactable)
                    {
                        currentInteractable = interactable;
                        ShowInteractionMessage(currentInteractable.GetInteractionMessage());
                    }
                    return;
                }
            }
        }

        // Nenhum objeto interativo encontrado
        if (currentInteractable != null)
        {
            currentInteractable = null;
            HideInteractionMessage();
        }
    }

    private void HandleInteraction()
    {
        if (currentInteractable != null)
        {
            Debug.Log($"Interagindo com: {currentInteractable}");
            currentInteractable.Interact();
        }
        else
        {
            Debug.LogWarning("Nenhum objeto interativo detectado.");
        }
    }

    private void ShowInteractionMessage(string message)
    {
        // Exibe a mensagem de interação na interface (placeholder para UI)
        Debug.Log($"Mensagem de interação: {message}");
    }

    private void HideInteractionMessage()
    {
        // Oculta a mensagem de interação na interface
        Debug.Log("Mensagem de interação ocultada.");
    }
}
