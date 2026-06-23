using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI pointsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePoints(int points)
    {
      pointsText.text = "Points: " + points;
    }

    public void GameTimer()
    {

    }

}
