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
        Collider[] collider = Physics.OverlapSphere(transform.position, colliderRange); // 1 ���� �浹ü Ž��

        for (int i = 0; i < collider.Length; i++) // Ÿ���� �ݶ��̴��� �ε����� ������ �Ѵ� -> Ÿ���� �ݶ��̴��� ã�´� 
        {
            Health target = collider[i].GetComponent<Health>();


            if (target != null && gameObject != null) // ã�� �ݶ��̴��� null�� �ƴϸ� ����
            {
                target.TakeDamage(damage);
            }
            //���� ������ ���� �ð� ����
        }
        nextAttack = attackCoolTime + Time.time;

    }
}
