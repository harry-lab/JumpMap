using UnityEngine;

public class DiamondItem : MonoBehaviour
{
    public float jumpBoost = 5f; // 점프력 증가량
    public float boostDuration = 20f; // 지속 시간
    public float rotationSpeed = 50f; // 회전 속도
    public float floatSpeed = 1f; // 위아래 움직이는 속도
    public float floatHeight = 0.5f; // 움직이는 높이

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position; // 초기 위치 저장
    }

    private void Update()
    {
        // 다이아몬드 회전
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // 위아래로 부드럽게 움직이기
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
                Destroy(gameObject); // 아이템 사용 후 삭제
            }
        }
    }
}