using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedMove;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Joystick joy;
    [SerializeField] private CameraMove cameraMove;

    private Vector3 moveVector;

    void FixedUpdate()
    {
        MovePlayer();
        cameraMove.UpdateCameraPosition();
    }

    private void MovePlayer()
    {
        moveVector = Vector3.zero;
        moveVector.x = joy.Horizontal;
        moveVector.z = joy.Vertical;

        moveVector = transform.right * moveVector.x + transform.forward * moveVector.z + transform.up * moveVector.y;
        transform.Translate(moveVector * speedMove * Time.deltaTime, Space.World);
        
        Vector3 rotationVector = new Vector3(joy.Horizontal, 0f, joy.Vertical);

        if (rotationVector.magnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rotationVector);
            playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, targetRotation, 360f * Time.deltaTime);
        }
    }
}
