using UnityEngine;
using TMPro;

public class ItemPoints : MonoBehaviour
{
    [Header("Item Settings")]
    public int pointsValue = 10;
    UI ui;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            ui.UpdatePoints(pointsValue);
            Destroy(collision.gameObject);
        }
    }

}
