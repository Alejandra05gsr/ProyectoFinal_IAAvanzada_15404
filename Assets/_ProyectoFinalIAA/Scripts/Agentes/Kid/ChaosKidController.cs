using UnityEngine;
using UnityEngine.AI;

public class ChaosKidController : MonoBehaviour
{
    public enum KidBehaviour
    {
        Patrol,
        Sleep,
        Follow
    }

    [Header("Componentes")]
    public NavMeshAgent agent;
    public Transform player;
    public PatrolController patrolController;


    [Header("Parámetros Multiagente")]
    public float separationDistance = 2f;
    public float separationForce = 3f;

    public KidBehaviour behavior = KidBehaviour.Sleep;
    public float stopDistance = 0.5f;
    public float preferedDistance = 1.5f;
    public float speed = 3f;

    private void Start()
    {
        behavior = (KidBehaviour)Random.Range(0, 2);
    }

    void Update()
    {
        switch (behavior)
        {
            case KidBehaviour.Patrol:
                PatrolBehavior();
                break;
            case KidBehaviour.Follow:
                FollowBehavior();
                break;
            case KidBehaviour.Sleep:
                SleepBehavior();
                break;
        }
    }

    void PatrolBehavior()
    {
        speed = 5f;
        patrolController.canPatrol = true;

    }


    void SleepBehavior()
    {
        speed = 0f;
    }

    void FollowBehavior()
    {
        speed = 2f;

        Vector3 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;
        Vector3 direction = Vector3.zero;

        if (distance < preferedDistance - 1f)
        {
            //Retroceder
            direction = -toPlayer.normalized;
        }
        else if (distance > preferedDistance + 1f)
        {
            //Acercarse un poco
            direction = toPlayer.normalized;
        }
        else
        {
            //Mantener
            return;
        }

        Vector3 separation = Separate() * separationForce;
        Vector3 finalDirection = direction + separation;
        finalDirection.y = 0f;
        finalDirection = Vector3.ClampMagnitude(finalDirection, 1f);

        transform.position += finalDirection * speed * Time.deltaTime;

    }

     public void CambiarComportamiento()
     {
        behavior = (KidBehaviour)Random.Range(0, 2);
        Debug.Log("ChangeBehaviour" + behavior);
     }


    Vector3 Separate()
    {
        Vector3 force = Vector3.zero;
        Collider[] neighbors = Physics.OverlapSphere(transform.position, separationDistance);

        foreach (Collider neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject && neighbor.CompareTag("Kid"))
            {
                Vector3 away = transform.position - neighbor.transform.position;
                float strength = Mathf.Clamp01((separationDistance - away.magnitude) / separationDistance);
                force += away.normalized * strength;

            }
        }

        return force;
    }

}
