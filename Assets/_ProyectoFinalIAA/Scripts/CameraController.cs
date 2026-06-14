using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 angle = new Vector2(-90 * Mathf.Deg2Rad, 0);

    public Transform follow;
    public float distance;
    public Vector2 sensitivity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X");
        if (horizontal != 0)
        {
            angle.x += horizontal * Mathf.Deg2Rad * sensitivity.x;
            Debug.Log("CamY");
        }

        float vertical = Input.GetAxis("Mouse Y");
        if (vertical != 0)
        {
            angle.y += vertical * Mathf.Deg2Rad * sensitivity.y;
            angle.y = Mathf.Clamp(angle.y, -80 * Mathf.Deg2Rad, 80 * Mathf.Deg2Rad);
        }

    }

    private void LateUpdate()
    {
        Vector3 orbit = new Vector3(Mathf.Cos(angle.x) * Mathf.Cos(angle.y),
                                        -Mathf.Sin(angle.y),
                                        -Mathf.Sin(angle.x) * Mathf.Cos(angle.y));


      transform.position = follow.position + new Vector3(0, 3, -distance);  
        
    }
}
