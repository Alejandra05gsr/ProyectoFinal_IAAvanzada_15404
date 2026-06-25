using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public bool canMove = true;


    void Start()
    {
        canMove = true;
    }


    private void Update()
    {
       Movement();
       LookDirection();


    }


    void StopStealing()
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

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float distance;

        if (groundPlane.Raycast(ray, out distance))
        {
            Vector3 mouseWorldPos = ray.GetPoint(distance);

            if (mouseWorldPos.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
    }


    public IEnumerator EnableMovement(float paralysed)
    {
        yield return new WaitForSeconds(paralysed);
        canMove = true;
    }

}
