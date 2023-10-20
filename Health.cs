using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // ���� ü���� �����ϴ� ���� 
    private float currentHp;
    private void Start()
    {
        // ���� ü���� Constant.MAX_HP(100)���� ����
        currentHp = Constant.MAX_HP;
    }
    
    // ���� ü���� �����ϴ� ������Ƽ 
    public float SetMinusHp // ���� ���� �� -=���� ����� ��
    {
        // ���� ü�� ��ȯ
        get { return currentHp; }
        set 

        {
            // ���� ���� ü���� Constant.DIE_HP(0) ���� ���ٸ�
            if (currentHp > Constant.DIE_HP)
            {
                // ���� ü���� setter �� �޾ƿ��� ü������ ����
                currentHp = value;

                // ���� ���� ü���� Constant.DIE_HP(0) ���� �۴ٸ�
                if (currentHp <= Constant.DIE_HP)
                {
                    // ���� ü���� Constant.DIE_HP(0) ���� ����
                    currentHp = Constant.DIE_HP;
                    // ������Ʈ ����
                    Destroy(gameObject);
                }
            }
        }
    }
    
}

// ���� ���� ����� �����ϴ� ������Ʈ