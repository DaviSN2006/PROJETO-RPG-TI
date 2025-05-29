using UnityEngine;
using System.Collections.Generic;

public class AbilityManager : MonoBehaviour
{
    [Header("Component References")]
    public Animator animator;
    public WeaponDamage weaponDamage;

    [Header("Attack Configuration")]
    public List<AttackKeyMapping> attackMappings;

    private Dictionary<KeyCode, AttackData> attackDictionary;
    private float lastAttackTime;

    private void Start()
    {
        attackDictionary = new Dictionary<KeyCode, AttackData>();
        foreach (var mapping in attackMappings)
        {
            attackDictionary[mapping.key] = mapping.attack;
        }

        if (weaponDamage == null)
        {
            weaponDamage = GetComponentInChildren<WeaponDamage>();
        }
    }

    private void Update()
    {
        HandleAttackInput();
    }

    private void HandleAttackInput()
    {
        foreach (var key in attackDictionary.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                AttackData attack = attackDictionary[key];
                if (Time.time >= lastAttackTime + attack.cooldown)
                {
                    PerformAttack(attack);
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    private void PerformAttack(AttackData attack)
    {
        Debug.Log($"Executando ataque: {attack.attackName}");

        if (!string.IsNullOrEmpty(attack.animationTrigger))
        {
            animator.SetTrigger(attack.animationTrigger);
        }

        if (weaponDamage != null)
        {
            int attackIndex = attackMappings.FindIndex(m => m.attack == attack);
            if (attackIndex >= 0)
            {
                weaponDamage.SetCurrentAttack(attackIndex);
                weaponDamage.EnableWeaponColliderForDuration(attack.attackDuration);
            }
        }
    }
}