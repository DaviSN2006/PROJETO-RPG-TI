using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Configurações de Movimento e Combate")]
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float moveSpeed = 3.5f;
    public float attackCooldown = 2f;
    public int attackDamage = 1;

    private Transform playerTarget;
    private float lastAttackTime;
    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.speed = moveSpeed;
            agent.stoppingDistance = attackRange * 0.8f;
        }

        FindPlayer();
    }

    void Update()
    {
        if (playerTarget == null)
        {
            FindPlayer();
            return;
        }

        float distance = Vector3.Distance(transform.position, playerTarget.position);

        if (distance <= attackRange)
        {
            agent.isStopped = true;
            animator.SetFloat("Speed", 0f);

            if (Time.time - lastAttackTime > attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
        else if (distance <= detectionRange)
        {
            agent.isStopped = false;
            agent.SetDestination(playerTarget.position);
            animator.SetFloat("Speed", 1f);

            Vector3 dir = agent.velocity.normalized;
            if (dir.x != 0)
                transform.localScale = new Vector3(Mathf.Sign(dir.x), 1f, 1f);
        }
        else
        {
            agent.isStopped = true;
            animator.SetFloat("Speed", 0f);
        }
    }

    void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            playerTarget = playerObj.transform;
    }

    void AttackPlayer()
    {
        if (playerTarget == null) return;

        animator.SetTrigger("Attack");

        // Aqui você pode adicionar dano real no player depois
        Debug.Log("Enemy atacou o player!");

        // Se você tiver um PlayerHealth, aqui seria:
        // playerTarget.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
    }
}
