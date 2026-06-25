using UnityEngine;

public class FuzzyRiskController : MonoBehaviour
{
    public Transform player;
    public Transform exitPoint;

    public bool hasProduct = false;

    public enum ThiefDecision { Steal, Hide, Escape }
    public ThiefDecision currentDecision;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasProduct = false;
        Debug.Log("Has Item: " + hasProduct);
    }

    // Update is called once per frame
    void Update()
    {
        EvaluateDecision();
    }


    void EvaluateDecision()
    {
        float distancePlayer = Vector3.Distance(this.transform.position, player.position);
        float distanceExit = Vector3.Distance(this.transform.position, exitPoint.position);

        float ruleSteal = FarPlayer(distancePlayer);
        float ruleHide = AND(ClosePlayer(distancePlayer), FarExit(distanceExit));
        float ruleEscape = OR(FarPlayer(distancePlayer), NearExit(distanceExit));

        if (hasProduct)
        {
            ruleSteal = 0f;
            ruleHide = ClosePlayer(distancePlayer);
            ruleEscape = FarPlayer(distancePlayer);
        }


        float steal = ruleSteal;
        float hide = ruleHide;
        float escape = ruleEscape;


        if (escape > steal && escape > hide && hasProduct)
        {
            currentDecision = ThiefDecision.Escape;
        }
        else if (hide > steal && !hasProduct)
        {
            currentDecision = ThiefDecision.Hide;
        }
        else if(!hasProduct)
        {
            currentDecision = ThiefDecision.Steal;
        }
    }

    //Distance al jugador
    float ClosePlayer(float distance)
    {
        if (distance <= 4f) return 1f;
        if (distance >= 12f) return 0f;
        return (12f - distance) / 8f;
    }

    float FarPlayer(float distance)
    {
        if (distance <= 4f) return 0f;
        if (distance >= 12f) return 1f;
        return (distance - 4f) / 8f;
    }


    //Distancia a la salida

    float NearExit(float distance)
    {
        if (distance <= 5f) return 1f;
        if (distance >= 15f) return 0f;
        return (15f - distance) / 10f;
    }

    float FarExit(float distance)
    {
        if (distance <= 5f) return 0f;
        if (distance >= 15f) return 1f;
        return (distance - 5f) / 10f;
    }



    //Operaciones Difusas
    float AND(float a, float b)
    {
        return Mathf.Min(a, b);
    }

    float OR(float a, float b)
    {
        return Mathf.Max(a, b);
    }

    float NOT(float a)
    {
        return 1 - a;
    }

}
