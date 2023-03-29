using System.Collections;
using TMPro;
using UnityEngine;

public class WarehouseForFactories : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countFirstResourceText;
    [SerializeField] private TextMeshProUGUI countSecondResourceText;
    [SerializeField] private TextMeshProUGUI countThirdResourceText;

    [SerializeField] private Transform secondFactory;
    [SerializeField] private Transform thirdFactory;

    [SerializeField] private GameObject firstResourcePrefab;
    [SerializeField] private GameObject secondResourcePrefab;
    [SerializeField] private GameObject thirdResourcePrefab;
    [SerializeField] private GameObject endMenu;

    [SerializeField] private ResourceStack resourceStackScript;
    [SerializeField] private Warehouse warehouseScript;

    public int countFirstResources = 0;
    public int countSecondResources = 0;
    public int countThirdResources = 0;

    private float spawnResourceInterval = 1f;
    private int resourcesPerSecond = 1;

    private bool transferResourcesToSecondFactory;
    private bool transferResourcesToThirdFactory;

    private int maxCountFirstResources = 20;
    private int maxCountSecondResources = 15;
    private int maxCountThirdResources = 10;

    private float transferFirstResourceTimer = 0f;
    private float transferSecondResourceTimer = 0f;
    private float countCreatedSecondResource;
    private float countCreatedThirdResource;

    private void Update()
    {
        UpdateCountResources();

        if(countThirdResources == maxCountThirdResources)
        {
            endMenu.SetActive(true);
        }

        if (countFirstResources > 0) // Отправка ресурсов на второй завод
            transferResourcesToSecondFactory = true;

        if (countFirstResources >= 2 && countSecondResources >= 1) // Отправка ресурсов на третий завод
            transferResourcesToThirdFactory = true;
        
        if (transferResourcesToSecondFactory) 
        {
            transferFirstResourceTimer += Time.deltaTime;

            if (transferFirstResourceTimer >= spawnResourceInterval)
            {
                transferFirstResourceTimer = 0f;
                TransferResourcesToSecondFactory();
            }

            if (countCreatedSecondResource >= 0)
            {
                countCreatedSecondResource = 0;
                transferResourcesToSecondFactory = false;
            }
        }

        if (transferResourcesToThirdFactory)
        {
            transferSecondResourceTimer += Time.deltaTime;

            if (transferSecondResourceTimer >= 1f)
            {
                transferSecondResourceTimer = 0f;
                TransferResourcesToThirdFactory();
            }

            if (countCreatedThirdResource >= 0)
            {
                countCreatedThirdResource = 0;
                transferResourcesToThirdFactory = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ProcessResourcesCoroutine(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator ProcessResourcesCoroutine(Collider other)
    {
        if (resourceStackScript.currentAmountFirstResource > 0)
        {
            yield return StartCoroutine(RemoveFirstResourcesCoroutine(other.GetComponent<ResourceStack>()));
        }

        if (resourceStackScript.currentAmountSecondResource > 0)
        {
            yield return StartCoroutine(RemoveSecondResourcesCoroutine(other.GetComponent<ResourceStack>()));
        }

        if (resourceStackScript.currentAmountThirdResource > 0)
        {
            yield return StartCoroutine(RemoveThirdResourcesCoroutine(other.GetComponent<ResourceStack>()));
        }

        StopAllCoroutines();
    }

    private IEnumerator RemoveFirstResourcesCoroutine(ResourceStack playerResourceStack)
    {
        while (resourceStackScript.currentAmountFirstResource > 0 && countFirstResources != maxCountFirstResources)
        {
            playerResourceStack.RemoveFirstResources(resourcesPerSecond);

            Destroy(warehouseScript.objectsArray[warehouseScript.xIndex, warehouseScript.yIndex]);

            ChangeData();

            countFirstResources++;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator RemoveSecondResourcesCoroutine(ResourceStack playerResourceStack)
    {
        while (resourceStackScript.currentAmountSecondResource > 0 && countSecondResources != maxCountSecondResources)
        {
            playerResourceStack.RemoveSecondResources(resourcesPerSecond);

            Destroy(warehouseScript.objectsArray[(int)warehouseScript.xIndex, warehouseScript.yIndex]);

            ChangeData();

            countSecondResources++;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator RemoveThirdResourcesCoroutine(ResourceStack playerResourceStack)
    {
        while (resourceStackScript.currentAmountThirdResource > 0 && countThirdResources != maxCountThirdResources)
        {
            playerResourceStack.RemoveThirdResources(resourcesPerSecond);

            Destroy(warehouseScript.objectsArray[(int)warehouseScript.xIndex, warehouseScript.yIndex]);

            ChangeData();

            countThirdResources++;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void TransferResourcesToSecondFactory()
    {
        GameObject resource = Instantiate(firstResourcePrefab, transform.position, Quaternion.identity);

        ResourceMover resourceMover = resource.GetComponent<ResourceMover>();
        resourceMover.targetPoint = secondFactory;

        countFirstResources--;
        countCreatedSecondResource++;
    }

    private void TransferResourcesToThirdFactory()
    {
        countSecondResources--;
        countFirstResources -= 2;
        countCreatedThirdResource++;

        GameObject secondResource = Instantiate(firstResourcePrefab, transform.position, Quaternion.identity);
        ResourceMover secondResourceMover = secondResource.GetComponent<ResourceMover>();
        secondResourceMover.targetPoint = thirdFactory;

        GameObject secondResourceClone = Instantiate(firstResourcePrefab, transform.position, Quaternion.identity);
        ResourceMover secondResourceCloneMover = secondResourceClone.GetComponent<ResourceMover>();
        secondResourceCloneMover.targetPoint = thirdFactory;
        secondResourceClone.transform.position = new Vector3(secondResourceClone.transform.position.x + 1f, secondResourceClone.transform.position.y, secondResourceClone.transform.position.z);

        GameObject thirdResource = Instantiate(secondResourcePrefab, transform.position, Quaternion.identity);
        ResourceMover thirdResourceMover = thirdResource.GetComponent<ResourceMover>();
        thirdResourceMover.targetPoint = thirdFactory;
        thirdResource.transform.position = new Vector3(thirdResource.transform.position.x + 2f, thirdResource.transform.position.y, thirdResource.transform.position.z);

       
    }

    private void UpdateCountResources()
    {
        countFirstResourceText.text = "(" + countFirstResources.ToString() + "/" + maxCountFirstResources + ")";
        countSecondResourceText.text = "(" + countSecondResources.ToString() + "/" + maxCountSecondResources + ")";
        countThirdResourceText.text = "(" + countThirdResources.ToString() + "/" + maxCountThirdResources + ")";
    }

    private void ChangeData()
    {
        warehouseScript.yIndex--;
        warehouseScript.yPos -= 0.5f;

        if (warehouseScript.yIndex < 0)
        {
            warehouseScript.xIndex--;
            warehouseScript.yIndex = 4;
            warehouseScript.xPos -= 0.25f;
        }

        if (warehouseScript.yPos < 0)
        {
            warehouseScript.yPos = 2;
        }
    }

}