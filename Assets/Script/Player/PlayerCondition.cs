using System;
using UnityEngine;
using System.Collections;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

// UI를 참조할 수 있는 PlayerCondition
// 외부에서 능력치 변경 기능은 이곳을 통해서 호출. 내부적으로 UI 업데이트 수행.
public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }
  // hunger가 0일때 사용할 값 (value > 0)
    public event Action onTakeDamage;   // Damage 받을 때 호출할 Action (6강 데미지 효과 때 사용)

    private bool isInWater = false; //  물에 있는지 확인하는 변수
    private Coroutine waterDamageCoroutine; //  체력 감소 코루틴 관리

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
        Debug.Log("플레이어가 죽었다.");
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
            if (!isInWater) // 이미 물 안에 있다면 중복 실행 방지
            {
                isInWater = true;
                waterDamageCoroutine = StartCoroutine(WaterDamageRoutine()); //  1초마다 체력 감소 코루틴 시작
                Debug.Log("물에 들어감! 체력 감소 시작");
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
                    StopCoroutine(waterDamageCoroutine); //  체력 감소 중단
                }
                Debug.Log("물에서 나옴! 체력 감소 중단");
            }
        }
    }

    //  1초마다 체력이 20씩 감소하는 코루틴
    private IEnumerator WaterDamageRoutine()
    {
        while (isInWater)
        {
            Heal(-20f); //  체력 20 감소
            Debug.Log("물 속에서 체력 -20 감소");
            yield return new WaitForSeconds(1f); //  1초마다 실행
        }
    }
}