using System.Collections;
using TMPro;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countFirstResourceText;
    [SerializeField] private TextMeshProUGUI countSecondResourceText;
    [SerializeField] private TextMeshProUGUI countThirdResourceText;

    [SerializeField] private GameObject firstResourcePrefab;
    [SerializeField] private GameObject secondResourcePrefab;
    [SerializeField] private GameObject thirdResourcePrefab;

    [SerializeField] private Transform stackPlayer;

    [SerializeField] private FirstFactoryController firstFactory;

    public GameObject[,] objectsArray = new GameObject[5, 5];

    public int countFirstResources = 0;
    public int countSecondResources = 0;
    public int countThirdResources = 0;

    public int maxCountFirstResources = 10;
    public int maxCountSecondResources = 8;
    public int maxCountThirdResources = 5;

    public float xPos = 0;
    public float yPos = 0;

    public int xIndex = 0;
    public int yIndex = 0;

    private float resourcesPerSecond = 1f;
    
    private void Update()
    {
        UpdateCountResources();

        if (countFirstResources >= maxCountFirstResources)
        {
            countFirstResources = maxCountFirstResources;
        }

        if(countSecondResources >= maxCountSecondResources)
        {
            countSecondResources = maxCountSecondResources;
        }

        if(countThirdResources >= maxCountThirdResources)
        {
            countThirdResources = maxCountThirdResources;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FirstResource"))
        {
            countFirstResources++;
        }

        if (other.gameObject.CompareTag("SecondResource"))
        {
            countSecondResources++;
        }

        if (other.gameObject.CompareTag("ThirdResource"))
        {
            countThirdResources++;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ProcessResourcesCoroutine(other));
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && countFirstResources > 0)
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
        if (countFirstResources > 0)
        {
            yield return StartCoroutine(TransferFirstResourcesCoroutine(other.GetComponent<ResourceStack>()));
        }

        if (countSecondResources > 0)
        {
            yield return StartCoroutine(TransferSecondResourcesCoroutine(other.GetComponent<ResourceStack>()));
        }

        if (countThirdResources > 0)
        {
            yield return StartCoroutine(TransferThirdResourcesCoroutine(other.GetComponent<ResourceStack>()));
        }

        StopAllCoroutines();
    }

    private IEnumerator TransferFirstResourcesCoroutine(ResourceStack playerResourceStack)
    {
        while (countFirstResources > 0 && playerResourceStack.currentAmountFirstResource != playerResourceStack.maxCountFirstResource)
        {
            firstFactory.resourceCount--;
            countFirstResources--;

            GameObject firstResource = Instantiate(firstResourcePrefab, stackPlayer);

            firstResource.transform.localPosition = new Vector3(xPos, yPos, 0);

            ChangeData();

            objectsArray[xIndex, yIndex] = firstResource;

            playerResourceStack.AddFirstResources(resourcesPerSecond);

            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }
    private IEnumerator TransferSecondResourcesCoroutine(ResourceStack playerResourceStack)
    {
        while (countSecondResources > 0 && playerResourceStack.currentAmountSecondResource != playerResourceStack.maxCountSecondResource)
        {
            countSecondResources--;

            GameObject secondResource = Instantiate(secondResourcePrefab, stackPlayer);

            secondResource.transform.localPosition = new Vector3(xPos, yPos, 0);

            ChangeData();
            
            objectsArray[xIndex, yIndex] = secondResource;

            playerResourceStack.AddSecondResources(resourcesPerSecond);

            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }

    private IEnumerator TransferThirdResourcesCoroutine(ResourceStack playerResourceStack)
    {
        while (countThirdResources > 0 && playerResourceStack.currentAmountThirdResource != playerResourceStack.maxCountThirdResource)
        {
            countThirdResources--;

            GameObject thirdResource = Instantiate(thirdResourcePrefab, stackPlayer);

            thirdResource.transform.localPosition = new Vector3(xPos, yPos, 0);

            ChangeData();
            objectsArray[xIndex, yIndex] = thirdResource;

            playerResourceStack.AddThirdResources(resourcesPerSecond);

            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }

    private void UpdateCountResources()
    {
        countFirstResourceText.text = "(" + countFirstResources.ToString() + "/" + maxCountFirstResources + ")";
        countSecondResourceText.text = "(" + countSecondResources.ToString() + "/" + maxCountSecondResources + ")";
        countThirdResourceText.text = "(" + countThirdResources.ToString() + "/" + maxCountThirdResources + ")";
    }

    private void ChangeData()
    {
        yIndex++;
        yPos++;
        yPos = yPos - 0.5f;

        if (yPos == 2.5f)
        {
            yPos = 0;
        }

        if (yIndex >= 5)
        {
            yIndex = 0;
            xIndex++;

            xPos++;
            xPos = xPos - 0.75f;
        }
    }
}