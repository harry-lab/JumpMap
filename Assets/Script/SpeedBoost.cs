using UnityEngine;

public class StarPickup : MonoBehaviour
{
    public float speedBoost = 5f; // 증가할 속도 값
    public float boostDuration = 5f; // 지속 시간
    public float rotationSpeed = 50f; // 회전 속도
    public float floatSpeed = 1f; // 위아래 움직이는 속도
    public float floatHeight = 0.1f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position; // 초기 위치 저장
    }

    private void Update()
    {
        // 회전
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // 위아래 움직이기
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
                Destroy(gameObject); // 별 오브젝트 제거
            }
        }
    }
}