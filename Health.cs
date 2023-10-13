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
            if(currentHp > Constant.DIE_HP)
            {
                currentHp = value;

                if (currentHp <= Constant.DIE_HP)
                    Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

// 생명 관련 기능을 수행하는 컴포넌트