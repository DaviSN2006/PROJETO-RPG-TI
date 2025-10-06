using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public Button lancerButton; // Botão do Lancer
    public Button mageButton;   // Botão do Mage
    public GameObject lancerPrefab; // Prefab do Lancer
    public GameObject magePrefab;   // Prefab do Mage
    public Transform arenaTransform; // Transform onde o personagem será instanciado na arena

    private GameObject selectedCharacter;
    public Canvas selectionCanvas; // O Canvas que contém os botões de escolha

    private void Start()
    {
        // Adiciona os listeners para os botões
        lancerButton.onClick.AddListener(SelectLancer);
        mageButton.onClick.AddListener(SelectMage);
    }

    // Função para selecionar o Lancer
    public void SelectLancer()
    {
        SelectCharacter(lancerPrefab);  // Chama SelectCharacter passando o prefab do Lancer
    }

    // Função para selecionar o Mage
    public void SelectMage()
    {
        SelectCharacter(magePrefab);  // Chama SelectCharacter passando o prefab do Mage
    }

    // Função que instancia o personagem na arena
    private void SelectCharacter(GameObject characterPrefab)
    {
        if (selectedCharacter != null)
        {
            Destroy(selectedCharacter); // Se já houver um personagem, destrói o anterior
        }

        // Instancia o personagem escolhido na arena
        selectedCharacter = Instantiate(characterPrefab, arenaTransform.position, Quaternion.identity);
        selectedCharacter.transform.SetParent(arenaTransform);

         selectionCanvas.gameObject.SetActive(false);
    }
}
