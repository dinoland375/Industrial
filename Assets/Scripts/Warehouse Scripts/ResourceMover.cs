using UnityEngine;

public class ResourceMover : MonoBehaviour
{
    public Transform targetPoint = null;
    public float moveSpeed = 10f;

    private Transform player = null;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        // вычисляем направление к игроку
        Vector3 direction = player.position - transform.position;

        direction.y = 0; // чтобы враг не поворачивался по вертикали

        // поворачиваем врага к игроку
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void FixedUpdate()
    {
        if (targetPoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
            
            if (transform.position == targetPoint.position)
            {
                Destroy(gameObject);
            }
        }
    }
}
