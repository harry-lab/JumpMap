using UnityEngine;

public class StarPickup : MonoBehaviour
{
    public float speedBoost = 5f; // ������ �ӵ� ��
    public float boostDuration = 5f; // ���� �ð�
    public float rotationSpeed = 50f; // ȸ�� �ӵ�
    public float floatSpeed = 1f; // ���Ʒ� �����̴� �ӵ�
    public float floatHeight = 0.1f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position; // �ʱ� ��ġ ����
    }

    private void Update()
    {
        // ȸ��
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // ���Ʒ� �����̱�
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.StartCoroutine(playerController.SpeedBoost(speedBoost, boostDuration));
                Destroy(gameObject); // �� ������Ʈ ����
            }
        }
    }
}