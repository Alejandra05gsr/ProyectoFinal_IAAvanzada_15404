using Panda;
using UnityEngine;
using UnityEngine.AI;

public class BTMomController : MonoBehaviour
{
    [Header("Chase")]
    public Transform player;
    public float detectionRange = 10f;
    public float chaseRange = 5f;

    [Header("Patrol")]
    Vector3 patrolPoint;
    public float patrolRange = 15f;
    public float arrivalDistance = 0.5f;
    private bool hasPatrolPoint = false;
    public NavMeshAgent agent;

    private MovementController movement;

   
    void Start()
    {
        movement = this.GetComponent<MovementController>();
    }

    [Task]
    bool IsPlayerVeryClose()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < chaseRange)
        {
            Task.current.Succeed();
            return true;
        }
        Task.current.Fail();
        return false;
    }

    [Task]
    void StopPlayer()
    {
        //Hace que el jugador no se pueda mover por 5 segundos
    }


    [Task]
    bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance < detectionRange;
    }

    [Task]
    void ChasePlayer()
    {
        agent.SetDestination(player.position);
        Task.current.Succeed();
    }


    [Task]
    void ChoosePatrolPoint()
    {
        patrolPoint = transform.position + new Vector3(Random.Range(-patrolRange, patrolRange), 0,
        Random.Range(-patrolRange, patrolRange));

        hasPatrolPoint = true;

        Task.current.Succeed();
    }

    
    [Task]
    void Patrol()
    {
        agent.SetDestination(patrolPoint);

        if (Vector3.Distance(transform.position, patrolPoint) < arrivalDistance)
        {
            hasPatrolPoint = false;
            Task.current.Succeed();
        }
    }



}
