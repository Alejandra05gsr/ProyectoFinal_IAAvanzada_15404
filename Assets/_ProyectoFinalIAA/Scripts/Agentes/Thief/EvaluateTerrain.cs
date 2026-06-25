using UnityEngine;

public enum IAEstado
{
    Steal,
    Hide
}

public class EvaluateTerrain : MonoBehaviour
{
    [Header("Transforms")]
    public Transform[] points;
    public Transform player;
    public Transform exitPoint;

    [Header("Pesos")]
    public float weightDistance = 1f;
    public float weightCover = 1.5f;
    public float weightExit = 1f;

    float EvaluatePoint(Transform point, IAEstado state)
    {

        float distance = Vector3.Distance(point.position, player.position);
        float distanceScore = Mathf.Clamp(10 - distance, 0, 10) / 10;

       
        float exitDistance = Vector3.Distance(point.position, exitPoint.position);
        float exitScore = Mathf.Clamp(20 - exitDistance, 0, 20) / 20;


        float coverScore = point.GetComponent<Points>().coverValue;


        if (state == IAEstado.Steal)
        {
            return (weightDistance * (1 - distanceScore)) + (weightCover * coverScore * 0.5f) + (weightExit * exitScore);
        }
        else 
        {
            return (weightDistance * (1 - distanceScore)) + (weightCover * coverScore) + (weightExit * (1 - exitScore));
        }
    }

    public Transform GetBestPoint(IAEstado state)
    {
        Transform bestPoint = null;
        float bestScore = -Mathf.Infinity;

        foreach (Transform point in points)
        {
            float score = EvaluatePoint(point, state);

            if (score > bestScore)
            {
                bestScore = score;
                bestPoint = point;
            }
        }
        return bestPoint;
    }
}
