using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerr : MonoBehaviour
{
    // Referências aos prefabs dos personagens
    public GameObject[] characterPrefabs;
    
    // Nomes das cenas dos mapas disponíveis
    public string[] mapScenes = new string[3];
    
    // Índice do personagem selecionado
    private int selectedCharacterIndex = 0;
    
    // Singleton pattern para manter o GameManager entre cenas
    private static GameManagerr instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Método para selecionar o personagem
    public void SelectCharacter(int characterIndex)
    {
        if (characterIndex >= 0 && characterIndex < characterPrefabs.Length)
        {
            selectedCharacterIndex = characterIndex;
            Debug.Log("Personagem selecionado: " + characterIndex);
        }
    }
    
    // Método para iniciar o jogo com um mapa aleatório
    public void StartGameWithRandomMap()
    {
        if (mapScenes.Length == 0)
        {
            Debug.LogError("Nenhuma cena de mapa configurada!");
            return;
        }
        
        // Seleciona um mapa aleatório
        int randomMapIndex = Random.Range(0, mapScenes.Length);
        string sceneToLoad = mapScenes[randomMapIndex];
        
        Debug.Log("Carregando mapa: " + sceneToLoad);
        
        // Carrega a cena do mapa
        SceneManager.LoadScene(sceneToLoad);
    }
    
    // Chamado quando a cena do mapa é carregada
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica se é uma cena de mapa (não é a cena de seleção)
        bool isMapScene = true;
        foreach (string mapScene in mapScenes)
        {
            if (scene.name == mapScene)
            {
                isMapScene = true;
                break;
            }
        }
        
        if (isMapScene)
        {
            SpawnPlayerCharacter();
        }
    }
    
    // Instancia o personagem selecionado no mapa
    private void SpawnPlayerCharacter()
    {
        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Length)
        {
            // Ponto de spawn padrão - você pode criar um GameObject com a tag "SpawnPoint" na cena
            Vector3 spawnPosition = Vector3.zero;
            Quaternion spawnRotation = Quaternion.identity;
            
            GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
            if (spawnPoint != null)
            {
                spawnPosition = spawnPoint.transform.position;
                spawnRotation = spawnPoint.transform.rotation;
            }
            
            Instantiate(characterPrefabs[selectedCharacterIndex], spawnPosition, spawnRotation);
        }
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}