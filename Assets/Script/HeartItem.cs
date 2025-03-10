using UnityEngine;

public class HeartItem : MonoBehaviour
{
    public float healAmount = 20f; // ȸ����

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