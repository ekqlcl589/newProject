using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private const float damage = 10f;

    private const float attackCoolTime = 1f;

    private float nextAttackTime;

    private Health targetHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextAttackTime && targetHealth != null)
        {
            Attacks();
        }
    }

    void Attacks()
    {
        targetHealth.TakeDamage(damage);

        nextAttackTime = Time.time + attackCoolTime;

    }

    
    // 콜리전과 충돌 시 타겟을 지정
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Cube") && collision.gameObject != gameObject)
            targetHealth = GetComponent<Health>();
    }

   
    private void OnCollisionExit(Collision collision)
    {
        targetHealth = null;
    }

}
