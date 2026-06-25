using UnityEngine;
using UnityEngine.AI;

public class ChaosKidController : MonoBehaviour
{
    public enum KidBehaviour
    {
        Patrol,
        Disorder,
        Sleep,
        Follow
    }

    [Header("Componentes")]
    public NavMeshAgent agent;
    public Transform player;
    public PatrolController patrolController;
    public GameObject detenerNiño;

    [Header("Parámetros Multiagente")]
    public float separationDistance = 2f;
    public float separationForce = 3f;

    public KidBehaviour behavior = KidBehaviour.Sleep;
    public float stopDistance = 0.5f;
    public float preferedDistance = 3f;
    public float speed = 3f;

    public Transform targetShelf; 


    void Update()
    {
        switch (behavior)
        {
            case KidBehaviour.Patrol:
                PatrolBehavior();
                break;
            case KidBehaviour.Disorder:
                DisorderBehavior();
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
        detenerNiño.SetActive(true);

    }

    void DisorderBehavior()
    {
        //En un punto del shelf el niño desordena
        detenerNiño.SetActive(true);
    }

    void SleepBehavior()
    {
        detenerNiño.SetActive(false);
        speed = 0f;
        //Elegir un behaviour de manera aleatoria para ponerle al niño
        behavior = (KidBehaviour)Random.Range(0, 4);
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

        detenerNiño.SetActive(true);

    }

     public void CambiarComportamientoSleep()
     {
         behavior = KidBehaviour.Sleep;
     }


    Vector3 Separate()
    {
        Vector3 force = Vector3.zero;
        Collider[] neighbors = Physics.OverlapSphere(transform.position, separationDistance);

        foreach (Collider neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject && neighbor.CompareTag("Enemy"))
            {
                Vector3 away = transform.position - neighbor.transform.position;
                float strength = Mathf.Clamp01((separationDistance - away.magnitude) / separationDistance);
                force += away.normalized * strength;

            }
        }

        return force;
    }




}
