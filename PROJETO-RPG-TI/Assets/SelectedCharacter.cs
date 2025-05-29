using UnityEngine;

public class SelectedCharacter : MonoBehaviour
{
    public GameObject characterPrefab; // Prefab do personagem escolhido
    public GameObject enemyInTrigger; // Referência ao inimigo que iniciou a arena
    public Vector3 playerMapPosition; // Posição do jogador no mapa antes de entrar na arena
    public Quaternion playerMapRotation; // Rotação do jogador no mapa antes de entrar na arena

    private static SelectedCharacter instance;

    public static SelectedCharacter Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("SelectedCharacterManager");
                instance = go.AddComponent<SelectedCharacter>();
                DontDestroyOnLoad(go); // Torna o objeto persistente entre cenas
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Torna persistente
        }
        else
        {
            Destroy(gameObject); // Evita duplicatas
        }
    }
}
