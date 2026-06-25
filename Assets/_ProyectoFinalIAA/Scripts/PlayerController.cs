using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;


    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, 0, v);
        transform.Translate(movement * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopStealing();
        }
    }


    void StopStealing()
    {
        Debug.Log("Player stopped stealing.");
    }
}
