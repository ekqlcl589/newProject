using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;

public class Health : MonoBehaviour
{ 
    private float currentHp = Constant.MAX_HP;

    public float CurrentHp // 변수 자체를 건드리는 게 아니라 함수를 통해 접근?
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

        Debug.Log(currentHp);
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}

// 생명 관련 기능을 수행하는 컴포넌트