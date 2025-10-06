using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterStats))]
public class DummyAI : MonoBehaviour
{
    public float walkRadius = 5f;
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;
    public float attackCooldown = 3f;
    public int attackDamage = 10;

    private CharacterStats stats;
    private Transform target;
    private float nextAttackTime;
    private Vector3 walkTarget;
    private bool isWalking = false;

    private Animator animator;

    private void Start()
    {
        stats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
        SetNewWalkTarget();
    }

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            Wander();
        }
        else
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= attackRange)
            {
                Attack();
            }
            else
            {
                ChaseTarget();
            }
        }
    }

    void FindTarget()
    {
        // Pode adaptar aqui para procurar o jogador ou outros alvos específicos
        GameObject player = GameObject.FindWithTag("Ally");
        if (player != null)
        {
            CharacterStats playerStats = player.GetComponent<CharacterStats>();
            if (playerStats != null && playerStats.characterType != stats.characterType)
            {
                target = player.transform;
            }
        }
    }

    void Wander()
    {
        if (!isWalking)
        {
            SetNewWalkTarget();
        }

        float distance = Vector3.Distance(transform.position, walkTarget);
        if (distance > 0.5f)
        {
            MoveTowards(walkTarget);
        }
        else
        {
            isWalking = false;
        }
    }

    void SetNewWalkTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection.y = 0; // Manter no plano horizontal
        walkTarget = transform.position + randomDirection;
        isWalking = true;
    }

    void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z)); // Olhar para o destino
    }

    void ChaseTarget()
    {
        if (target != null)
        {
            MoveTowards(target.position);
        }
    }

    void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            // Ativa a animação de ataque se existir
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }

            // Aplica dano
            CharacterStats targetStats = target.GetComponent<CharacterStats>();
            if (targetStats != null)
            {
                targetStats.TakeDamage(attackDamage, stats);
            }

            nextAttackTime = Time.time + attackCooldown;
        }
    }
}
