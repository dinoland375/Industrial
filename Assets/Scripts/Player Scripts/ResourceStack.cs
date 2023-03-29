using TMPro;
using UnityEngine;

public class ResourceStack : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCountFirstResource;
    [SerializeField] private TextMeshProUGUI textCountSecondResource;
    [SerializeField] private TextMeshProUGUI textCountThirdResource;

    public float maxCountFirstResource;
    public float maxCountSecondResource;
    public float maxCountThirdResource;

    public float currentAmountFirstResource;
    public float currentAmountSecondResource;
    public float currentAmountThirdResource;

    private void Update()
    {
        UpdateCountResources();
    }

    public bool AddFirstResources(float amount)
    {
        if (currentAmountFirstResource + amount <= maxCountFirstResource)
        {
            currentAmountFirstResource += amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AddSecondResources(float amount)
    {
        if (currentAmountSecondResource + amount <= maxCountSecondResource)
        {
            currentAmountSecondResource += amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AddThirdResources(float amount)
    {
        if (currentAmountThirdResource + amount <= maxCountThirdResource)
        {
            currentAmountThirdResource += amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveFirstResources(float amount)
    {
        if (currentAmountFirstResource > 0)
        {
            currentAmountFirstResource -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveSecondResources(float amount)
    {
        if (currentAmountSecondResource > 0)
        {
            currentAmountSecondResource -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveThirdResources(float amount)
    {
        if (currentAmountThirdResource > 0)
        {
            currentAmountThirdResource -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateCountResources()
    {
        textCountFirstResource.text = "(" + currentAmountFirstResource + "/" + maxCountFirstResource + ")";
        textCountSecondResource.text = "(" + currentAmountSecondResource + "/" + maxCountSecondResource + ")";
        textCountThirdResource.text = "(" + currentAmountThirdResource + "/" + maxCountThirdResource + ")";
    }
}
