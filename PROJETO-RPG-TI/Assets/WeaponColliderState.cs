using UnityEngine;

public class WeaponColliderState : StateMachineBehaviour
{
    public bool enableOnEnter = true;
    public bool disableOnExit = true;

    private WeaponDamage weaponDamage;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (weaponDamage == null)
        {
            weaponDamage = animator.GetComponentInChildren<WeaponDamage>();
        }

        if (enableOnEnter && weaponDamage != null)
        {
            weaponDamage.EnableWeaponCollider();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (disableOnExit && weaponDamage != null)
        {
            weaponDamage.DisableWeaponCollider();
        }
    }
}