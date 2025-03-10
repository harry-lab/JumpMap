using System;
using UnityEngine;
using System.Collections;

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

    private bool isInWater = false; //  ���� �ִ��� Ȯ���ϴ� ����
    private Coroutine waterDamageCoroutine; //  ü�� ���� �ڷ�ƾ ����

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            if (!isInWater) // �̹� �� �ȿ� �ִٸ� �ߺ� ���� ����
            {
                isInWater = true;
                waterDamageCoroutine = StartCoroutine(WaterDamageRoutine()); //  1�ʸ��� ü�� ���� �ڷ�ƾ ����
                Debug.Log("���� ��! ü�� ���� ����");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            if (isInWater)
            {
                isInWater = false;
                if (waterDamageCoroutine != null)
                {
                    StopCoroutine(waterDamageCoroutine); //  ü�� ���� �ߴ�
                }
                Debug.Log("������ ����! ü�� ���� �ߴ�");
            }
        }
    }

    //  1�ʸ��� ü���� 20�� �����ϴ� �ڷ�ƾ
    private IEnumerator WaterDamageRoutine()
    {
        while (isInWater)
        {
            Heal(-20f); //  ü�� 20 ����
            Debug.Log("�� �ӿ��� ü�� -20 ����");
            yield return new WaitForSeconds(1f); //  1�ʸ��� ����
        }
    }
}