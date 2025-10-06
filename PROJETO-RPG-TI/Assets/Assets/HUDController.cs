using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importar TextMeshPro

public class HUDController : MonoBehaviour
{
    public Slider healthBar; // Referência à barra de vida
    public TextMeshProUGUI healthText; // Texto para exibir a vida
    public Image ability1CooldownImage; // Imagem do cooldown da habilidade 1
    public TextMeshProUGUI ability1CooldownText; // Texto do cooldown da habilidade 1
    public Image ability2CooldownImage; // Imagem do cooldown da habilidade 2
    public TextMeshProUGUI ability2CooldownText; // Texto do cooldown da habilidade 2

    private CharacterStats characterStats; // Referência ao script CharacterStats

    void Start()
    {
        // Referências ao personagem pai
        characterStats = GetComponentInParent<CharacterStats>();


        if (characterStats != null)
        {
            // Inicializa a barra e o texto de vida
            healthBar.maxValue = characterStats.health;
            healthBar.value = characterStats.health;
            UpdateHealthText();
        }
    }

    void Update()
    {
        // Atualiza a barra e o texto de vida
        if (characterStats != null)
        {
            healthBar.value = characterStats.health;
            UpdateHealthText();
        }

        
    }

    // Atualiza o texto de vida (ex.: "HP: 80/100")
    void UpdateHealthText()
    {
        if (healthText != null && characterStats != null)
        {
            healthText.text = $"HP: {characterStats.health}/{healthBar.maxValue}";
        }
    }
    
}

