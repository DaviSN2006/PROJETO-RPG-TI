using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    public CharacterStats stats; // Referência ao script de estatísticas
    private CharacterController characterController; // Referência ao CharacterController
    public float moveSpeed = 5f; // Velocidade do movimento
        private Vector2 moveInput; // Entrada de movimento
    public float rotationSpeed = 700f; // Velocidade da rotação
    private Vector3 originalScale; // Escala original do personagem

    private void Start()
    {
        characterController = GetComponent<CharacterController>(); // Obtém o CharacterController
        originalScale = transform.localScale; // Armazena a escala original do personagem
    }

    private void Update()
    {
        MoveCharacter(); // Movimento e rotação do personagem
                RotateCharacter(); // Chama a função de rotação
    }

    private void MoveCharacter()
    {
        // Obtenha entrada de movimento
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Calcula a direção do movimento
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // Aplica movimento
        if (moveDirection.magnitude > 0)
        {
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            // Gira o personagem para olhar na direção do movimento
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            
        }
    }

    private void RotateCharacter()
    {
        // Se houver entrada de movimento (se o jogador estiver se movendo)
        if (moveInput.magnitude > 0)
        {
            // Cria um vetor de movimento baseado na entrada
            Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
            // Calcula a rotação que o personagem deve ter para olhar na direção do movimento
            Quaternion toRotation = Quaternion.LookRotation(direction);
            // Aplica uma rotação suave utilizando RotateTowards
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    
}
