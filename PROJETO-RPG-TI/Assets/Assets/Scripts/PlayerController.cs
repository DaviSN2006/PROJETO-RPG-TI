using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento
    public float gravity = -9.81f; // Força gravitacional
    public float rotationSpeed = 700f; // Velocidade da rotação
    private Vector2 moveInput; // Entrada de movimento
    private Vector3 velocity; // Armazena a velocidade vertical para gravidade
    private bool isPaused = false; // Verifica se o movimento está pausado

    private CharacterController controller; // Componente CharacterController
    private PlayerInputActions inputActions;

    public GameObject characterSelectionCanvas; // Referência ao Canvas de seleção
    public string arenaSceneName; // Nome da cena da arena

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;

        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("O CharacterController não está anexado ao Player!");
        }
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Update()
    {
        if (!isPaused)
        {
            Move();
            ApplyGravity();
            RotateCharacter();
        }
    }

    private void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void RotateCharacter()
    {
        if (moveInput.magnitude > 0)
        {
            Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Enemy"))
    {
        PausePlayer();
        ShowCharacterSelection();
        return;
    }

    if (other.CompareTag("NPC"))
    {
        return;
    }

    if (other.CompareTag("Chest"))
    {
        return;
    }

    Debug.LogWarning($"Objeto colidido não possui uma tag esperada: {other.name}");
}



    private void PausePlayer()
    {
        isPaused = true; // Pausa o movimento do player
    }

    private void ShowCharacterSelection()
{
    if (characterSelectionCanvas != null)
    {
        characterSelectionCanvas.SetActive(true); // Ativa o Canvas
    }
    else
    {
        
    }
}

    public void OnCharacterSelected() // Chame este método após a seleção
    {
        characterSelectionCanvas.SetActive(false); // Fecha o Canvas
        LoadArena();
    }

    private void LoadArena()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(arenaSceneName); // Carrega a cena da arena
    }
}
