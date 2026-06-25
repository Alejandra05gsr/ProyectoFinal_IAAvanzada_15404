using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public bool canMove = true;
    public float rotationSpeed = 8f;
    public UI ui;

    void Start()
    {
        canMove = true;
    }


    private void Update()
    {
       Movement();
       LookDirection();


    }


    void CalmKid()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Se añaden puntos?
        }
    }


    void Movement()
    {
        if (!canMove)
            return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, 0, v);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    void LookDirection()
    {
        if (!canMove)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane groundPlane = new Plane(Vector3.up, transform.position);

        float distance;

        if (groundPlane.Raycast(ray, out distance))
        {
            Vector3 mousePosition = ray.GetPoint(distance);

            Vector3 direction = mousePosition - transform.position;
            direction.y = 0;

            if (direction.magnitude > 0.1f)
            {
                Quaternion targetRotation =
                    Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime);
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Kid"))
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                other.GetComponent<ChaosKidController>().CambiarComportamiento();
                ui.CalculateKidPoints(5);
            }
        }
    }


    public IEnumerator EnableMovement(float paralysed)
    {
        yield return new WaitForSeconds(paralysed);
        canMove = true;
    }

}
