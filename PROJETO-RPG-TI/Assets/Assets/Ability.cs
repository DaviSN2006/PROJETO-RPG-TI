using UnityEngine;

[System.Serializable]
public class Ability
{
    public string name; // Nome da habilidade
    public KeyCode activationKey; // Tecla para ativar a habilidade
    public string animationTrigger; // Nome do Trigger no Animator
    public float cooldown; // Tempo de recarga da habilidade

    public GameObject visualEffect; // Prefab do efeito visual
    public AudioClip soundEffect; // Áudio da habilidade
    public float effectDuration; // Duração do efeito (opcional)

    [HideInInspector] public float lastUsedTime; // Momento em que a habilidade foi usada
}
