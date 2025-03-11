using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 15f; // �������� ��

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ForceJump(jumpForce);
            }
        }
    }
}
