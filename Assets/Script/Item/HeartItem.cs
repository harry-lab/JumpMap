using UnityEngine;

public class HeartItem : MonoBehaviour
{
    public float healAmount = 20f; // 회복량
    public float rotationSpeed = 50f; // 회전 속도
    public float floatSpeed = 1f; // 위아래 움직이는 속도
    public float floatHeight = 0.1f; // 위아래 움직이는 높이

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position; // 초기 위치 저장
    }

    private void Update()
    {
        // 하트 회전
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // 위아래 움직이기
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
                Destroy(gameObject); // 아이템 사용 후 삭제
            }
        }
    }
}