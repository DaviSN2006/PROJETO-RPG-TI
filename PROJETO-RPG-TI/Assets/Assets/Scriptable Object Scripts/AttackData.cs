using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "Combat/Attack Data")]
public class AttackData : ScriptableObject
{
    public string attackName;
    public int damage = 10;
    public float cooldown = 1f;
    public float attackDuration = 0.5f;
    public bool isMelee = true;
    public bool isAOE = false;
    public string animationTrigger;
    public GameObject effectPrefab;
    public AudioClip attackSound;
}