using System;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

// UI�� ������ �� �ִ� PlayerCondition
// �ܺο��� �ɷ�ġ ���� ����� �̰��� ���ؼ� ȣ��. ���������� UI ������Ʈ ����.
public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }
  // hunger�� 0�϶� ����� �� (value > 0)
    public event Action onTakeDamage;   // Damage ���� �� ȣ���� Action (6�� ������ ȿ�� �� ���)

    private void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);
        health.Add(health.passiveValue * Time.deltaTime);

        if (health.curValue < 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Die()
    {
        Debug.Log("�÷��̾ �׾���.");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }
}