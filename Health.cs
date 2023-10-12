using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private float currentHp;

    // Start is called before the first frame update
    private void Start()
    {
        currentHp = Constant.MAX_HP;
    }

    public virtual void OnDamage(float damage)
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