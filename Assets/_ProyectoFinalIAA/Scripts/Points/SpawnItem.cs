using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [Header("Items to spawn")]
    public GameObject[] items;
    int itemIndex;
    public Transform spawnerItem;
    bool isInSpawnRange = false;

    [Header("Timer")]
    public float actualTime;
    float spawnTime = 5f;
    public bool canSpawn = true;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canSpawn = true;
        actualTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
       TimerSpawn();
        SpawnItemInScene();

    }

    void ChooseOneItem()
    {
        itemIndex = Random.Range(0, items.Length);
    }

    void TimerSpawn()
    {
        if (!canSpawn)
        {
            actualTime += Time.deltaTime;
            if (actualTime >= spawnTime)
            {
                actualTime = spawnTime;
                canSpawn = true;
            }
        }
    }


    public void SpawnItemInScene()
    {
        if (Input.GetKeyDown(KeyCode.E) && canSpawn && isInSpawnRange)
        {
            Debug.Log("Spawn Item");
            ChooseOneItem();
            Instantiate(items[itemIndex], spawnerItem.position, Quaternion.identity);
            actualTime = 0f;
            canSpawn = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnRange"))
        {
            isInSpawnRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SpawnRange"))
        {
            isInSpawnRange = false;
        }
    }

}
