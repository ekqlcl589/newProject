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

    private float currentHp;
    private void Start()
    {
        currentHp = Constant.MAX_HP;
    }

    public float CurrentHp
    {
        set 
        {
            if (currentHp > Constant.DIE_HP)
            {
                currentHp -= value;

                if (currentHp <= Constant.DIE_HP)
                {
                    currentHp = Constant.DIE_HP;
                    Die();
                }
            }
        }
    }
    
    private void Die()
    {
        Destroy(gameObject);
        onDestroy?.Invoke();
    }
}

// ���� ���� ����� �����ϴ� ������Ʈ