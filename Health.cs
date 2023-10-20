using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // 현재 체력을 관리하는 변수 
    private float currentHp;
    private void Start()
    {
        // 현재 체력을 Constant.MAX_HP(100)으로 대입
        currentHp = Constant.MAX_HP;
    }
    
    // 현재 체력을 관리하는 프로퍼티 
    public float SetMinusHp // 값을 받을 때 -=으로 해줘야 함
    {
        // 현재 체력 반환
        get { return currentHp; }
        set 

        {
            // 만약 현재 체력이 Constant.DIE_HP(0) 보다 높다면
            if (currentHp > Constant.DIE_HP)
            {
                // 현재 체력은 setter 로 받아오는 체력으로 대입
                currentHp = value;

                // 만약 현재 체력이 Constant.DIE_HP(0) 보다 작다면
                if (currentHp <= Constant.DIE_HP)
                {
                    // 현재 체력은 Constant.DIE_HP(0) 으로 대입
                    currentHp = Constant.DIE_HP;
                    // 오브젝트 삭제
                    Destroy(gameObject);
                }
            }
        }
    }
    
}

// 생명 관련 기능을 수행하는 컴포넌트