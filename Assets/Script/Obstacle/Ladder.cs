using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾����� Ȯ��
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.StartClimbing(); //  �÷��̾�� ��ٸ� ��� ���� ��û
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) //  �÷��̾����� Ȯ��
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.StopClimbing(); //  �÷��̾�� ��ٸ� ��� ���� ��û
            }
        }
    }
}
