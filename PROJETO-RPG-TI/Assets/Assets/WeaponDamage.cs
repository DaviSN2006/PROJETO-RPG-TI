using UnityEngine;
using System.Collections.Generic;

public class WeaponDamage : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private List<AttackData> attackDataList;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private bool ignoreOwner = true;

    private Collider weaponCollider;
    private GameObject owner;
    private CharacterStats ownerStats;
    private int currentAttackIndex = 0;

    private HashSet<CharacterStats> alreadyHitTargets = new HashSet<CharacterStats>();

    private void Awake()
    {
        weaponCollider = GetComponent<Collider>();
        if (weaponCollider == null)
        {
            Debug.LogError("WeaponDamage precisa de um Collider!");
        }
        weaponCollider.enabled = false;
    }

    public void SetAttackDataList(List<AttackData> attacks, GameObject weaponOwner = null)
    {
        attackDataList = attacks;
        owner = weaponOwner;
        ownerStats = owner?.GetComponent<CharacterStats>();
    }

    public void SetCurrentAttack(int attackIndex)
    {
        if (attackIndex >= 0 && attackIndex < attackDataList.Count)
        {
            currentAttackIndex = attackIndex;
        }
        else
        {
            Debug.LogWarning("Índice de ataque inválido: " + attackIndex);
        }
    }

    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
        alreadyHitTargets.Clear();
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
    }

    public void EnableWeaponColliderForDuration(float duration)
    {
        EnableWeaponCollider();
        Invoke(nameof(DisableWeaponCollider), duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!weaponCollider.enabled) return; // Evita problemas
        if ((targetLayer.value & (1 << other.gameObject.layer)) == 0) return;

        if (ignoreOwner && owner != null && other.gameObject == owner) return;

        CharacterStats targetStats = other.GetComponent<CharacterStats>();
        if (targetStats == null) return;
        if (!IsValidTarget(targetStats)) return;
        if (alreadyHitTargets.Contains(targetStats)) return;

        AttackData currentAttack = attackDataList[currentAttackIndex];
        targetStats.TakeDamage(currentAttack.damage, ownerStats);

        alreadyHitTargets.Add(targetStats);
    }

    private bool IsValidTarget(CharacterStats target)
    {
        return ownerStats == null || ownerStats.characterType != target.characterType;
    }
}
