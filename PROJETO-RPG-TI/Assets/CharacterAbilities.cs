using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAbilities : MonoBehaviour
{
    [System.Serializable]
    public class Ability
    {
        public string abilityName; // Nome da habilidade
        public KeyCode activationKey; // Tecla para ativar (Z, X, C ou V)
        public GameObject effectPrefab; // Prefab do efeito (opcional, como projéteis ou partículas)
        public float cooldown; // Tempo de recarga
        [HideInInspector] public float lastUsedTime; // Tempo em que foi usada pela última vez
    }

    public Ability[] abilities; // Lista de habilidades do personagem

    private void Update()
    {
        foreach (var ability in abilities)
        {
            if (Input.GetKeyDown(ability.activationKey) && Time.time >= ability.lastUsedTime + ability.cooldown)
            {
                ActivateAbility(ability);
            }
        }
    }

    private void ActivateAbility(Ability ability)
    {
        Debug.Log($"Ativando habilidade: {ability.abilityName}");

        // Instancia o efeito da habilidade, se houver
        if (ability.effectPrefab != null)
        {
            Instantiate(ability.effectPrefab, transform.position + transform.forward, transform.rotation);
        }

        // Atualiza o tempo de uso
        ability.lastUsedTime = Time.time;
    }
}
