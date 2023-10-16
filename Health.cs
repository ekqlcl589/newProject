using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //������Ʈ �ı� �� ȣ���� �̺�Ʈ
    public System.Action onDestroy; 

    private float currentHp = Constant.MAX_HP;

    public float CurrentHp
    {
        set 
        {
            OnDamage(value);
        }
    }
    
    private void OnDamage(float damage)
    {
        currentHp -= damage;

        if (currentHp <= Constant.DIE_HP)
        {
            currentHp = Constant.DIE_HP;
            Die();
        }

    }
    private void Die()
    {
        Destroy(gameObject);
        onDestroy?.Invoke();
    }
}

// ���� ���� ����� �����ϴ� ������Ʈ