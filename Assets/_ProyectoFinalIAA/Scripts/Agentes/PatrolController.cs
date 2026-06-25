using Panda;
using UnityEngine;
using UnityEngine.AI;

public class PatrolController : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] patrolPoints;
    int currentPatrolIndex = 0;

    public NavMeshAgent agent;
    public bool canPatrol = true;


    void Start()
    {
        canPatrol = false;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }


    void ChoosePatrolPoint()
    {
        currentPatrolIndex = Random.Range(0, patrolPoints.Length);
    }



    public void Patrol()
    {
        if (!canPatrol)
        {
            return;
        }

        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        float distance = Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position);

        if (distance <= 1f)
        {
            Invoke("ChoosePatrolPoint", 2.0f);
        }
    }

}
