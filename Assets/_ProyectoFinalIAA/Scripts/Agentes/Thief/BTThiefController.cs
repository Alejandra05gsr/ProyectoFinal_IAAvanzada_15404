using Panda;
using UnityEngine;

public class BTThiefController : MonoBehaviour
{
    [Header("Flee")]
    public Transform player;
    public float detectionRange = 10f;
    public float fleeRange = 5f;

    [Header("Patrol")]
    Vector3 patrolPoint;
    public float patrolRange = 15f;
    public float arrivalDistance = 0.5f;
    private bool hasPatrolPoint = false;

    private MovementController movement;



    void Start()
    {
        movement = this.GetComponent<MovementController>();
    }

    [Task]
    bool IsPlayerTooClose() //Checar
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < fleeRange)
        {
            Task.current.Succeed();
            return true;
        }
        Task.current.Fail();
        return false;
    }

    [Task]
    void Flee() //Checar
    {
        Vector3 directionAwayFromPlayer = (transform.position - player.position).normalized;
        Vector3 fleeTarget = transform.position + directionAwayFromPlayer * 5f; // Flee 5 units away
        movement.MoveTowards(fleeTarget);
        Task.current.Succeed();
    }


    [Task]
    bool HasStolenItem()
    {
        // Implement logic to check if the thief has a stolen item
        // For now, let's assume it always returns true for demonstration purposes
        return true;
    }

    [Task]
    void MoveToExit()
    {
        // Implement logic to move the thief towards the exit
        // For now, let's assume the exit is at a fixed position (e.g., (0, 0, 0))
        Vector3 exitPosition = new Vector3(0, 0, 0);
        movement.MoveTowards(exitPosition);
        Task.current.Succeed();
    }

    [Task]
    void FindProduct()
    {
        // Implement logic to find a product in the store
        // For now, let's assume the product is at a fixed position (e.g., (5, 0, 5))
        Vector3 productPosition = new Vector3(5, 0, 5);
        movement.MoveTowards(productPosition);
        Task.current.Succeed();
    }

    [Task]
    void StealProduct()
    {
        // Implement logic to steal the product
        // For now, let's assume the thief successfully steals the product
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
        movement.MoveTowards(patrolPoint);

        if (Vector3.Distance(transform.position, patrolPoint) < arrivalDistance)
        {
            hasPatrolPoint = false;
            Task.current.Succeed();
        }
    }


}
