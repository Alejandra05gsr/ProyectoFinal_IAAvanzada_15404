using Panda;
using UnityEngine;
using UnityEngine.AI;

public class BTThiefController : MonoBehaviour
{
    
    public NavMeshAgent agent;
    public FuzzyRiskController fuzzySystem;
    public UI ui;

    [Header("Transforms")]
    public Transform player;
    public Transform exitPoint;
    public Transform hideSpot;
    public Transform productPoint;


    public float panicDistance = 3.5f; 
    public float arrivalDistance = 1.0f;

    public int removePoints = -10; 
    public int addPoints = 10; 


    [Task]
    bool IsScared()
    {
        // Pánico inmediato si el jugador está excesivamente cerca
        return Vector3.Distance(transform.position, player.position) < panicDistance;
    }


    [Task]
    bool ShouldEscape()
    {
        return fuzzySystem.currentDecision == FuzzyRiskController.ThiefDecision.Escape;
    }

    [Task]
    bool ShouldHide()
    {
        return fuzzySystem.currentDecision == FuzzyRiskController.ThiefDecision.Hide;
    }

    [Task]
    bool ShouldSteal()
    {
        return fuzzySystem.currentDecision == FuzzyRiskController.ThiefDecision.Steal;
    }

    [Task]
    bool HasItem()
    {
        return fuzzySystem.hasProduct;
    }

    [Task]
    bool HasNotItem()
    {
        return !fuzzySystem.hasProduct;
    }



    [Task]
    void FleeToExit()
    {
        agent.SetDestination(exitPoint.position);
        agent.speed = 10f; 

        if (Vector3.Distance(transform.position, exitPoint.position) <= arrivalDistance)
        {
            ui.CalculateItemPoints(addPoints);
            Destroy(gameObject);
            Task.current.Succeed();
        }
    }

    [Task]
    void Escape()
    {
        agent.SetDestination(exitPoint.position);
        agent.speed = 3.5f;

        if (Vector3.Distance(transform.position, exitPoint.position) <= arrivalDistance)
        {
            ui.CalculateItemPoints(removePoints);
            Destroy(gameObject);
            Task.current.Succeed();
        }
    }

    [Task]
    void Hide()
    {
        agent.SetDestination(hideSpot.position);
        agent.speed = 3.5f;

        if (Vector3.Distance(transform.position, hideSpot.position) <= arrivalDistance)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    void Steal()
    {
        agent.SetDestination(productPoint.position);
        agent.speed = 3.5f;

        if (Vector3.Distance(transform.position, productPoint.position) <= arrivalDistance)
        {
            fuzzySystem.hasProduct = true; 
            Task.current.Succeed();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, panicDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, arrivalDistance);
    }


}
