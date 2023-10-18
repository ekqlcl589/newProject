using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //오브젝트 파괴 시 호출할 이벤트
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

// 생명 관련 기능을 수행하는 컴포넌트