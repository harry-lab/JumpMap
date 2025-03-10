using UnityEngine;

public class HeartItem : MonoBehaviour
{
    public float healAmount = 20f; // 회복량

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