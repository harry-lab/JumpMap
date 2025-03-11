using UnityEngine;

public class DiamondItem : MonoBehaviour
{
    public float jumpBoost = 5f; // ������ ������
    public float boostDuration = 20f; // ���� �ð�
    public float rotationSpeed = 50f; // ȸ�� �ӵ�
    public float floatSpeed = 1f; // ���Ʒ� �����̴� �ӵ�
    public float floatHeight = 0.5f; // �����̴� ����

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position; // �ʱ� ��ġ ����
    }

    private void Update()
    {
        // ���̾Ƹ�� ȸ��
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // ���Ʒ��� �ε巴�� �����̱�
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
                playerController.StartCoroutine(playerController.JumpBoost(jumpBoost, boostDuration));
                Destroy(gameObject); // ������ ��� �� ����
            }
        }
    }
}