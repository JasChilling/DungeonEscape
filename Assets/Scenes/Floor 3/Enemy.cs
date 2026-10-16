using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float maxHP = 100f;
    private float currentHP;
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1f;
    private float attackTimer;

    [Header("Wander Settings")]
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderTimer = 5f;
    private float wanderCountdown;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Slider healthSlider;

    private Animator animator;
    private NavMeshAgent navAgent;
    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        wanderCountdown = wanderTimer;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHP;
            healthSlider.value = currentHP;
        }

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
    }

    void Update()
    {
        if (isDead || player == null || navAgent == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // Attack State
            navAgent.isStopped = true;
            if (animator != null) animator.SetBool("isWalking", false);

            // Handle Attack Timing & Damage
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                animator.SetTrigger("Attack");

                // Deal damage to player heart system
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.LoseHeart();
                }

                attackTimer = 0f;
            }
        }
        else if (distanceToPlayer <= chaseRange)
        {
            // Chase State
            navAgent.isStopped = false;
            navAgent.SetDestination(player.position);
            attackTimer = 0f; // Reset attack timer while chasing

            if (animator != null) animator.SetBool("isWalking", true);
        }
        else
        {
            // Wander State (Random Patrol)
            navAgent.isStopped = false;
            wanderCountdown += Time.deltaTime;
            attackTimer = 0f;

            if (wanderCountdown >= wanderTimer || !navAgent.hasPath || navAgent.remainingDistance < 0.5f)
            {
                Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
                randomDirection += transform.position;
                NavMeshHit hit;

                if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
                {
                    NavMeshPath path = new NavMeshPath();
                    if (navAgent.CalculatePath(hit.position, path) && path.status == NavMeshPathStatus.PathComplete)
                    {
                        navAgent.SetDestination(hit.position);
                    }
                }
                wanderCountdown = 0f;
            }

            // Check if destination is active and moving to keep walking animation alive
            bool isMoving = navAgent.hasPath && navAgent.remainingDistance > navAgent.stoppingDistance;
            if (animator != null)
            {
                animator.SetBool("isWalking", isMoving);
            }
        }
    }

    public void takeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHP -= damageAmount;
        Debug.Log("Enemy took " + damageAmount + " damage! Current HP: " + currentHP);

        if (healthSlider != null)
        {
            healthSlider.value = currentHP;
        }

        if (currentHP <= 0)
        {
            currentHP = 0;
            isDead = true;
            if (animator != null) animator.SetTrigger("DIE");
            if (navAgent != null) navAgent.isStopped = true;
            StartCoroutine(LoadVictoryAfterDelay(1.5f));
        }
        else
        {
            if (animator != null) animator.SetTrigger("DAMAGE");
        }
    }

    IEnumerator LoadVictoryAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameWon");
    }
}