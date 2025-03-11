using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 15f; // 점프대의 힘

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
