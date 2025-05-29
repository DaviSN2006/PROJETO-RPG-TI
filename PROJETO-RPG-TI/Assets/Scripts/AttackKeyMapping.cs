using UnityEngine;

[System.Serializable]
public class AttackKeyMapping
{
    public string keyName; // Nome para facilitar o entendimento
    public KeyCode key; // Tecla associada
    public AttackData attack; // Dados do ataque
} 