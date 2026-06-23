using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [Header("Items to spawn")]
    public GameObject[] items;
    int itemIndex;
    public Transform spawnerItem;

    [Header("Timer")]
    public float actualTime;
    float spawnTime = 5f;
    bool canSpawn = true;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canSpawn = true;
        actualTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSpawn)
        {
            actualTime += Time.deltaTime;
            if (actualTime >= spawnTime)
            {
                canSpawn = true;
                actualTime = 0f;
            }
        }
    }

    void ChooseOneItem()
    {
        itemIndex = Random.Range(0, items.Length);
    }

    public void SpawnItemInScene()
    {
        if (Input.GetKeyDown(KeyCode.E) && canSpawn)
        {
            ChooseOneItem();
            Instantiate(items[itemIndex], spawnerItem.position, Quaternion.identity);
            canSpawn = false;
        }
    }



}
