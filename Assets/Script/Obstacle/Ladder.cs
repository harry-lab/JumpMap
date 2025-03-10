using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어인지 확인
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.StartClimbing(); //  플레이어에게 사다리 등반 시작 요청
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) //  플레이어인지 확인
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.StopClimbing(); //  플레이어에게 사다리 등반 종료 요청
            }
        }
    }
}
