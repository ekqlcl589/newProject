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
        // 스크립트가 활성화 되면, 현재 체력을 Constant.MAX_HP(100)으로 대입
        currentHp = Constant.MAX_HP;
    }
    
    // 현재 체력을 관리하는 프로퍼티 
    public float SetMinusHp // 값을 받을 때 -=으로 해줘야 함
    {
        // 현재 체력 반환
        get { return currentHp; }
        set 

        {
            // 체력은 항상 0 보다 커야 하므로 조건 설정
            if (currentHp > Constant.DIE_HP)
            {
                // 현재 체력은 setter 로 받아오는 체력으로 대입
                currentHp = value;

                // 현재 체력이 0보다 크다가 setter를 통해 데미지를 받아서 0 이하가 되면 죽는 조건을 설정
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