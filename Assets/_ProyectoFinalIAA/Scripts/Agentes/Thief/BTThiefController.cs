using Panda;
using UnityEngine;
using UnityEngine.AI;

public class BTThiefController : MonoBehaviour
{
    [Header("Components")]
    public NavMeshAgent agent;
    public FuzzyRiskController fuzzySystem;
    public EvaluateTerrain evaluateTerrain;
    public UI ui;
    public GameObject itemStolen;

    [Header("Transforms")]
    public Transform player;
    public Transform exitPoint;

    [Header("Distance")]
    public float panicDistance = 3.5f; 
    public float arrivalDistance = 1.0f;

    [Header("Points")]
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
        itemStolen.SetActive(false);
        agent.SetDestination(exitPoint.position);
        agent.speed = 10f; 

        if (Vector3.Distance(transform.position, exitPoint.position) <= arrivalDistance)
        {
            ui.CalculateThiefPoints(addPoints);
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
            ui.CalculateThiefPoints(removePoints);
            Destroy(gameObject);
            Task.current.Succeed();
        }
    }

    [Task]
    void Hide()
    {
        Transform bestHidePoint = evaluateTerrain.GetBestPoint(IAEstado.Hide);

        if (bestHidePoint != null)
        {
            agent.SetDestination(bestHidePoint.position);
        }

        if (!agent.pathPending && agent.remainingDistance <= arrivalDistance)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    void Steal()
    {
        Transform bestStealPoint = evaluateTerrain.GetBestPoint(IAEstado.Steal);

        if (bestStealPoint != null)
        {
            if (agent.destination != bestStealPoint.position)
            {
                agent.SetDestination(bestStealPoint.position);
                agent.speed = 3.5f;
            }
        }

        if (!agent.pathPending && agent.remainingDistance <= arrivalDistance)
        {
            fuzzySystem.hasProduct = true;
            itemStolen.SetActive(true);
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
