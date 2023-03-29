using TMPro;
using UnityEngine;

public class SecondFactoryController : MonoBehaviour
{
    [SerializeField] private Transform warehouse;
    [SerializeField] private Warehouse warehouseScript;
    [SerializeField] private GameObject secondResourcePrefab;
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private TextMeshProUGUI countFirstResourcesText;

    public int countResourceInFactory;

    private bool production = true;
    private float spawnInterval = 5f;
    private float spawnTimer = 0f;

    private void Update()
    {
        UpdateCountResourcesText();

        if (warehouseScript.countSecondResources == warehouseScript.maxCountSecondResources)
        {
            production = false;
            notificationText.text = "Завод №2 прекратил производство\r\n(Склад переполнен)";
            notificationText.color = Color.red;
        }

        if (countResourceInFactory < 2)
        {
            production = false;
            notificationText.text = "Завод №2 прекратил производство\r\n(Нехватка ресурсов)";
            notificationText.color = Color.red;
        }

        if(countResourceInFactory >= 2)
        {
            production = true;
        }

        if (production)
        {
            notificationText.text = "Завод №2 продолжил производство";
            notificationText.color = Color.green;

            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;

                countResourceInFactory -= 2;
                SpawnResource();
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FirstResource"))
        {
            countResourceInFactory++;
        }
    }

    private void SpawnResource()
    {
        GameObject resource = Instantiate(secondResourcePrefab, transform.position, Quaternion.identity);

        ResourceMover resourceMover = resource.GetComponent<ResourceMover>();
        resourceMover.targetPoint = warehouse;
    }

    private void UpdateCountResourcesText()
    {
        countFirstResourcesText.text = "(" + countResourceInFactory.ToString() + "/2)";
    }
}
