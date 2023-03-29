using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float lerpRate = 10f;

    public void UpdateCameraPosition()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * lerpRate);
    }
}
