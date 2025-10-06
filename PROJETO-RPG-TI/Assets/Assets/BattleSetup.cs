using UnityEngine;
using UnityEngine.UI;

public class BattleSetup : MonoBehaviour
{
    [System.Serializable]
    public class Card
    {
        public string cardName; // Nome do monstro
        public Sprite cardImage; // Imagem da carta
        public GameObject prefab; // Prefab 3D associado
    }

    public Card[] availableCards; // Lista de cartas disponíveis
    public GameObject cardButtonPrefab; // Prefab do botão de carta
    public Transform cardSelectionParent; // Onde os botões serão colocados
    private GameObject selectedPrefab; // Prefab escolhido pelo jogador

    private void Start()
    {
        GenerateCardButtons();
    }

    private void GenerateCardButtons()
    {
        foreach (var card in availableCards)
        {
            // Cria um botão para cada carta
            GameObject button = Instantiate(cardButtonPrefab, cardSelectionParent);
            button.GetComponent<Image>().sprite = card.cardImage;

            // Adiciona uma ação ao botão
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                SelectCard(card.prefab);
            });
        }
    }

    private void SelectCard(GameObject prefab)
    {
        selectedPrefab = prefab; // Armazena o prefab escolhido
        Debug.Log($"Selecionado: {prefab.name}");

        // Aqui você pode configurar a transição para a arena
        TransitionToArena();
    }

    private void TransitionToArena()
    {
        // Exemplo de desativar a UI e iniciar a batalha
        cardSelectionParent.gameObject.SetActive(false);
        // Aqui você configura os prefabs na arena, ativa a câmera, etc.
    }
}
