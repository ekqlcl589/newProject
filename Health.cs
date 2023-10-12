using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private float currentHp = Constant.MAX_HP;

    // Start is called before the first frame update
    private void Start()
    {

    }

    public virtual void OnDamage(float damage) // 셋으로 빼...면 IDamageable 로 호출할 수가 없는데..?
    {
        if (currentHp <= Constant.DIE_HP)
            Die();

        currentHp -= damage;
        //Debug.Log(currentHp);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

// 생명 관련 기능을 수행하는 컴포넌트