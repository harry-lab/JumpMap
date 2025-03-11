using UnityEngine;

public class HeartItem : MonoBehaviour
{
    public float healAmount = 20f; // ȸ����
    public float rotationSpeed = 50f; // ȸ�� �ӵ�
    public float floatSpeed = 1f; // ���Ʒ� �����̴� �ӵ�
    public float floatHeight = 0.1f; // ���Ʒ� �����̴� ����

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position; // �ʱ� ��ġ ����
    }

    private void Update()
    {
        // ��Ʈ ȸ��
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // ���Ʒ� �����̱�
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCondition playerCondition = other.GetComponent<PlayerCondition>();
            if (playerCondition != null)
            {
                playerCondition.Heal(healAmount);
                Destroy(gameObject); // ������ ��� �� ����
            }
        }
    }
}