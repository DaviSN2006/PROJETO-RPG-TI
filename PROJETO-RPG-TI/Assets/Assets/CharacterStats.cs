using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public enum CharacterType { Ally, Enemy, Neutral }

    public string characterName;
    public CharacterType characterType;
    public int health = 100;
    public int maxHealth = 100;
    public int defense = 5;
    public HealthBar healthBar;

    private void Start()
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int damage, CharacterStats attackerStats = null)
    {
        if (attackerStats != null && attackerStats.characterType == characterType)
        {
            Debug.Log($"[Proteção] {attackerStats.characterName} tentou atacar aliado {characterName}. Bloqueado.");
            return;
        }

        int calculatedDamage = Mathf.Max(damage - defense, 1);
        health = Mathf.Clamp(health - calculatedDamage, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.SetHealth(health);
        }

        Debug.Log($"{characterName} recebeu {calculatedDamage} de dano de {(attackerStats != null ? attackerStats.characterName : "unknown")}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{characterName} foi derrotado!");
        Destroy(gameObject);
    }
}