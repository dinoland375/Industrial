using TMPro;
using UnityEngine;

public class ThirdFactoryController : MonoBehaviour
{
    [SerializeField] private Warehouse warehouseScript;
    [SerializeField] private WarehouseForFactories warehouseForFactoryScript; 
    
    [SerializeField] private Transform warehouse;
    [SerializeField] private GameObject thirdResourcePrefab;

    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private TextMeshProUGUI countFirstResourcesText;
    [SerializeField] private TextMeshProUGUI countSecondResourcesText;

    public int countFirstResourcesInFactory;
    public int countSecondResourcesInFactory;

    private bool production = true;

    private float spawnInterval = 7f;
    private float spawnTimer = 0f;

    private void Update()
    {
        UpdateCountResourcesText();

        if (warehouseScript.countThirdResources == warehouseScript.maxCountThirdResources)
        {
            production = false;
            notificationText.text = "Завод №3 прекратил производство\r\n(Склад переполнен)";
            notificationText.color = Color.red;
        }

        if (countFirstResourcesInFactory >= 4 && countSecondResourcesInFactory >= 2)
        {
            production = true;
        }
        else
        {
            production = false;
            notificationText.text = "Завод №3 прекратил производство\r\n(Нехватка ресурсов)";
            notificationText.color = Color.red;
        }

        if (production)
        {
            notificationText.text = "Завод №3 продолжил производство";
            notificationText.color = Color.green;

            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;

                countFirstResourcesInFactory -= 4;
                countSecondResourcesInFactory -= 2;
                SpawnResource();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FirstResource"))
        {
            countFirstResourcesInFactory++;
        }

        if (other.gameObject.CompareTag("SecondResource"))
        {
            countSecondResourcesInFactory++;
        }
    }

    private void SpawnResource()
    {
        GameObject resource = Instantiate(thirdResourcePrefab, transform.position, Quaternion.identity);

        ResourceMover resourceMover = resource.GetComponent<ResourceMover>();
        resourceMover.targetPoint = warehouse;
        production = false;
    }

    private void UpdateCountResourcesText()
    {
        countFirstResourcesText.text = "(" + countFirstResourcesInFactory.ToString() + "/4)";
        countSecondResourcesText.text = "(" + countSecondResourcesInFactory.ToString() + "/2)";
    }
}
