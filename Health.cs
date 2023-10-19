using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float currentHp;
    private void Start()
    {
        currentHp = Constant.MAX_HP;
    }

    
    public float SetMinusHp // 값을 받을 때 -=으로 해줘야 함
    {
        get { return currentHp; }
        set 
        {
            if (currentHp > Constant.DIE_HP)
            {
                currentHp = value;

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
    }
}

// 생명 관련 기능을 수행하는 컴포넌트