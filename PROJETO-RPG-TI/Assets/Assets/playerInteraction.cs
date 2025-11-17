using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2.0f; // Alcance da interação
    public LayerMask interactionLayer;    // Camada para objetos interativos
    public GameObject InteractionMessagePrefab;
    private InteractionMessage interactionMessageInstance;

    private IInteractable currentInteractable;

    // Input System
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Interact.started += _ => StartInteractionInput();
        inputActions.Player.Interact.canceled += _ => CancelInteractionInput();

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

    void LateUpdate()
    {
        DetectInteractable();
    }

    private void DetectInteractable()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRange, interactionLayer);

        if (hits.Length > 0)
        {
            foreach (Collider hit in hits)
            {
                IInteractable interactable = hit.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    if (currentInteractable != interactable)
                    {
                        currentInteractable = interactable;
                        ShowInteractionMessage(currentInteractable);
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

            if (DialogueManager.Instance.IsDialogueActive())
                DialogueManager.Instance.EndDialogue();
        }
    }

    private void HandleInteraction()
    {
        if (currentInteractable != null)
        {
            Debug.Log($"Interagindo com: {currentInteractable}");
            currentInteractable.Interact();
            HideInteractionMessage();
        }
        else
        {
            Debug.LogWarning("Nenhum objeto interativo detectado.");
        }
    }

    private void StartInteractionInput()
    {
        currentInteractable?.StartInteractionInput(InputSystem.settings.defaultHoldTime);
    }

    private void CancelInteractionInput()
    {
        currentInteractable?.CancelInteractionInput();
    }

    private void ShowInteractionMessage(IInteractable interactable)
    {
        // Exibe a mensagem de interação na interface (placeholder para UI)
        if (!interactionMessageInstance)
        {
            interactionMessageInstance = Instantiate(InteractionMessagePrefab, interactable.GetGameObject().transform).GetComponent<InteractionMessage>();
            interactionMessageInstance.textReference.text = interactable.GetInteractionMessage();
        }

        Debug.Log($"Mensagem de interação: {interactable.GetInteractionMessage()}");
    }

    private void HideInteractionMessage()
    {
        // Oculta a mensagem de interação na interface
        if (interactionMessageInstance)
            Destroy(interactionMessageInstance.gameObject);

        Debug.Log("Mensagem de interação ocultada.");
    }
}
