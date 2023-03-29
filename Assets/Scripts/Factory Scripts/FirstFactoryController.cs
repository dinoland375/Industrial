using TMPro;
using UnityEngine;

public class FirstFactoryController : MonoBehaviour
{
    [SerializeField] private GameObject resourcePrefab;
    [SerializeField] private GameObject notificationTextObject;

    [SerializeField] private TextMeshProUGUI notificationText;

    [SerializeField] private Transform warehouse;

    public int resourceCount;

    private bool production = true;

    private float spawnInterval = 3f;
    private float spawnTimer = 0f;
    private int maxResource;

    private void Start()
    {
        maxResource = warehouse.GetComponent<Warehouse>().maxCountFirstResources;
    }
    private void Update()
    {
        if (resourceCount >= maxResource)
        {
            production = false;
            notificationText.text = "Завод №1 прекратил производство\r\n(Склад заполнен)";
            notificationText.color = Color.red;
        }
        else
        {
            production = true;
            notificationText.text = "Завод №1 продолжил производство";
            notificationText.color = Color.green;
        }

        if (production)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;
                SpawnResource();
            }
        }
    }

    private void SpawnResource()
    {
        GameObject resource = Instantiate(resourcePrefab, transform.position, Quaternion.identity);

        ResourceMover resourceMover = resource.GetComponent<ResourceMover>();
        resourceMover.targetPoint = warehouse;

        resourceCount++;
    }
}