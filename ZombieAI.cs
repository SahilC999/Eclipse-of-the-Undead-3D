using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls zombie AI: patrol, chase, attack, hear sounds, and die.
/// Attach this to the Zombie prefab.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class ZombieAI : MonoBehaviour
{
    [Header("Zombie Settings")]
    public float detectionRange = 15f;
    public float attackRange = 2f;
    public float hearingRange = 25f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 15;

    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    private bool isPatrolling = true;

    [Header("References")]
    public Transform player;
    public Animator animator;
    public AudioSource growlAudio;
    public AudioSource attackAudio;
    public GameObject lootDropPrefab;

    private NavMeshAgent agent;
    private float nextAttackTime = 0;
    private bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[0].position);
        }
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // If player is close -> chase
        if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        // If player is making noise (gunshots), zombie is alerted
        else if (ZombieHearingSystem.playerMadeNoise && 
                 Vector3.Distance(player.position, transform.position) <= hearingRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        // Check for attack
        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            AttackPlayer();
        }

        UpdateAnimations();
    }

    // Patrol between waypoints
    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
        isPatrolling = true;
    }

    // Chase player aggressively
    void ChasePlayer()
    {
        agent.SetDestination(player.position);
        isPatrolling = false;
        if (growlAudio && !growlAudio.isPlaying) growlAudio.Play();
    }

    // Attack player and apply damage
    void AttackPlayer()
    {
        if (attackAudio) attackAudio.Play();
        if (animator) animator.SetTrigger("Attack");
        nextAttackTime = Time.time + attackCooldown;

        // Damage player if in range
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist <= attackRange + 0.5f)
        {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if (ph) ph.TakeDamage(attackDamage);
        }
    }

    // Called when zombie takes fatal damage
    public void Die()
    {
        if (isDead) return;
        isDead = true;
        agent.isStopped = true;
        if (animator) animator.SetTrigger("Die");

        // Drop loot randomly
        if (lootDropPrefab && Random.value > 0.6f)
        {
            Instantiate(lootDropPrefab, transform.position + Vector3.up, Quaternion.identity);
        }

        Destroy(gameObject, 4f);
    }

    // Updates animation parameters
    void UpdateAnimations()
    {
        if (!animator) return;

        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
        animator.SetBool("IsChasing", !isPatrolling);
    }
}

/// <summary>
/// Simple static class to handle zombie hearing.
/// PlayerCombat.cs should set playerMadeNoise=true when gunshots occur.
/// </summary>
public static class ZombieHearingSystem
{
    public static bool playerMadeNoise = false;

    public static void RegisterGunshot()
    {
        playerMadeNoise = true;
        // Reset noise after short time
        TimerHelper.SetTimeout(1.5f, () => playerMadeNoise = false);
    }
}

/// <summary>
/// Helper for invoking delayed actions without coroutines.
/// </summary>
public class TimerHelper : MonoBehaviour
{
    public static TimerHelper instance;
    void Awake() { if (!instance) instance = this; }

    public static void SetTimeout(float delay, System.Action action)
    {
        if (!instance)
        {
            GameObject helper = new GameObject("TimerHelper");
            instance = helper.AddComponent<TimerHelper>();
        }
        instance.StartCoroutine(instance.ExecuteAfterTime(delay, action));
    }

    System.Collections.IEnumerator ExecuteAfterTime(float time, System.Action action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }
}
