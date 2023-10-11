using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private const float damage = 10f;

    private const float attackCoolTime = 1f;

    private const float colliderRange = 1f;

    private float nextAttack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextAttack)
        {
            Attacks();
        }
    }
    
    void Attacks()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, colliderRange); // 1 범위 충돌체 탐색

        for (int i = 0; i < collider.Length; i++) // 타겟의 콜라이더에 부딪히면 공격을 한다 -> 타겟의 콜라이더를 찾는다 
        {
            Health target = collider[i].GetComponent<Health>();


            if (target != null && gameObject != null) // 찾은 콜라이더가 null이 아니면 공격
            {
                target.TakeDamage(damage);
            }
            //다음 공격을 위한 시간 설정
        }
        nextAttack = attackCoolTime + Time.time;

    }
}
