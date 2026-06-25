using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI pointsGameText;
    public int totalPoints;


    [Header("Finish Screen")]
    public GameObject finishScreen;
    public GameObject[] stars;
    public TextMeshProUGUI itemText;
    public int itemPoints;
    public TextMeshProUGUI evadeMomText;
    public int evadeMomPoints;
    public TextMeshProUGUI captureThiefsText;
    public int captureThiefsPoints;
    public TextMeshProUGUI stopKidsText;
    public int stopKidsPoints;
    public TextMeshProUGUI totalPointText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartPoints();
        UpdateFinishPointsText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Points
    void StartPoints()
    {
        totalPoints = 0;
        itemPoints = 0;
        evadeMomPoints = 0;
        captureThiefsPoints = 0;
        stopKidsPoints = 0;

        UpdatePoints();
        UpdateFinishPointsText();
    }
    public void CalculateItemPoints(int itemSpecificPoints)
    {
        totalPoints += itemSpecificPoints;
        itemPoints += itemSpecificPoints;
        UpdatePoints();
    }
    public void CalculateMomPoints(int momSpecificPoints)
    {
        totalPoints += momSpecificPoints;
        evadeMomPoints += momSpecificPoints;
        UpdatePoints();
    }
    public void CalculateThiefPoints(int thiefSpecificPoints)
    {
        totalPoints += thiefSpecificPoints;
        captureThiefsPoints += thiefSpecificPoints;
        UpdatePoints();
    }
    public void CalculateKidPoints(int kidSpecificPoints)
    {
        totalPoints += kidSpecificPoints;
        stopKidsPoints += kidSpecificPoints;
        UpdatePoints();
    }

    void UpdatePoints()
    {
      pointsGameText.text = "Points: " + totalPoints;
      UpdateFinishPointsText();
      CalculateStars();
    }

    public void GameTimer()
    {

    }


    //Finish Screen
    void ShowFinishScreen()
    {
        finishScreen.SetActive(true);
        CalculateStars();
        UpdateFinishPointsText();
    }

    void UpdateFinishPointsText()
    {
        itemText.text = "Items: " + itemPoints;
        evadeMomText.text = "Evade Moms: " + evadeMomPoints;
        captureThiefsText.text = "Capture Thiefs: " + captureThiefsPoints;
        stopKidsText.text = "Stop Kids: " + stopKidsPoints;
        totalPointText.text = "Total Points: " + totalPoints;
    }

    void CalculateStars()
    {
        if (totalPoints >= 100)
        {
            for (int i = 0; i < 5; i++)
            {
                stars[i].SetActive(true);
            }
        }
        else if (totalPoints >= 80)
        {
            for (int i = 0; i < 4; i++)
            {
                stars[i].SetActive(true);
            }
        }
        else if (totalPoints >= 60)
        {
            for (int i = 0; i < 3; i++)
            {
                stars[i].SetActive(true);
            }
        }
        else if (totalPoints >= 40)
        {
            for (int i = 0; i < 2; i++)
            {
                stars[i].SetActive(true);
            }
        }
        else if (totalPoints >= 20)
        {
            for (int i = 0; i < 1; i++)
            {
                stars[i].SetActive(true);
            }
        }

    }
}
