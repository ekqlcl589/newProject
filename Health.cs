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

    public virtual void OnDamage(float damage) // ������ ��...�� IDamageable �� ȣ���� ���� ���µ�..?
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

// ���� ���� ����� �����ϴ� ������Ʈ