using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;

public class Health : MonoBehaviour
{ 
    private float currentHp = Constant.MAX_HP;

    public float CurrentHp
    {
        get { return currentHp; }
        set 
        {
            OnDamage(value);
        }
    }

    private void OnDamage(float damage)
    {
        if (currentHp <= Constant.DIE_HP)
            Die();

        currentHp -= damage;

    }
    private void Die()
    {
        Destroy(gameObject);
    }
}

// ���� ���� ����� �����ϴ� ������Ʈ