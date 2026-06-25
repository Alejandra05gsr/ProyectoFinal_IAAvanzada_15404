using Panda;
using UnityEngine;
using UnityEngine.AI;

public class BTMomController : MonoBehaviour
{
    [Header("Chase")]
    public GameObject player;
    public float paralysisDuration = 5f;
    public float detectionRange = 10f;
    public float stopRange = 5f;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    int currentPatrolIndex = 0;

    public NavMeshAgent agent;



    void Start()
    {

    }

    [Task]
    bool IsPlayerVeryClose()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.gameObject.transform.position);
        if (distanceToPlayer < stopRange)
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
        player.GetComponent<PlayerController>().canMove = false;
        //Se espera 5 segundos para que el jugador pueda moverse de nuevo
        player.GetComponent<PlayerController>().Invoke("EnableMovement", paralysisDuration);
        Task.current.Succeed();
    }


    [Task]
    bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, player.gameObject.transform.position);
        return distance < detectionRange;
    }

    [Task]
    void ChasePlayer()
    {
        agent.SetDestination(player.gameObject.transform.position);
        Task.current.Succeed();
    }


    [Task]
    void ChoosePatrolPoint()
    {
        currentPatrolIndex = Random.Range(0, patrolPoints.Length);
        Task.current.Succeed();
    }


    [Task]
    void Patrol()
    {
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        float distance = Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position);

        //Si el agente llegó al punto de patrulla entonces se completa la tarea
        if (distance < 1.5f)
        {
            Task.current.Succeed();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopRange);
    }

}




