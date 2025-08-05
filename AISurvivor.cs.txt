using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// AI Survivor: waits hidden, follows player when detected, and heads to car.
/// Attach to each Survivor NPC.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class AISurvivor : MonoBehaviour
{
    public enum SurvivorState { Hidden, Following, Safe }
    public SurvivorState state = SurvivorState.Hidden;

    [Header("Detection")]
    public float detectionRange = 10f;

    [Header("References")]
    public Transform player;
    public Transform car;
    public Animator animator;
    private NavMeshAgent agent;

    private bool hasMetPlayer = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!player) player = GameObject.FindWithTag("Player").transform;
        if (!car) car = GameObject.FindWithTag("Car").transform;
    }

    void Update()
    {
        if (state == SurvivorState.Hidden)
        {
            DetectPlayer();
        }
        else if (state == SurvivorState.Following)
        {
            FollowPlayer();
            CheckIfReachedCar();
        }
    }

    void DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            state = SurvivorState.Following;
            hasMetPlayer = true;
            animator?.SetTrigger("Follow");
            Debug.Log(name + " detected player! Following...");
        }
    }

    void FollowPlayer()
    {
        if (player)
        {
            agent.SetDestination(player.position);
        }
    }

    void CheckIfReachedCar()
    {
        float carDist = Vector3.Distance(transform.position, car.position);
        if (carDist <= 3f && hasMetPlayer)
        {
            state = SurvivorState.Safe;
            agent.isStopped = true;
            animator?.SetTrigger("Safe");
            Debug.Log(name + " reached the car!");

            SurvivorManager.ReportSurvivorReachedCar();
            gameObject.SetActive(false); // Survivor enters car and disappears
        }
    }
}
