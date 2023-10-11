using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private float nextAttackTime;

    private Health targetHealth;

    private Bullet bullet;
    private void Awake()
    {
    }
    private void Start()
    {
        bullet = GetComponent<Bullet>(); // 이러면 bullet 없이 Attack 만 독단적으로 사용할 수 없음, 컴포넌트화 x
        targetHealth = GetComponent<Health>();        
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextAttackTime && targetHealth != null)
        {
            bullet.TakeAttack();

            nextAttackTime = Time.time + Constant.ATTACK_COLLTIME;
        }
    }

    private void Attacks()
    { 
        if(targetHealth != null)
        {
            targetHealth.OnDamage(Constant.DAMAGE);
            nextAttackTime = Time.time + Constant.ATTACK_COLLTIME;
        }

    }

    // 콜리전과 충돌 시 타겟을 지정
    //private void OnCollisionEnter(Collision collision)
    //{
    //    //if(collision.collider.CompareTag("Cube") && collision.gameObject != gameObject)
    //    //{
    //    //        targetHealth = GetComponent<Health>();
    //    //
    //    //    //if(targetHealth != null)
    //    //    //    targetHealth.TakeDamage(damage);// = GetComponent<Health>();
    //    //    //targetHealth.OnDamage(damage);
    //    //    Debug.Log("충돌");
    //    //
    //    //}
    //    //Health health = collision.collider.GetComponent<Health>();
    //    //
    //    //if(health != null) 
    //    //{
    //    //    health.OnDamage(damage);
    //    //}
    //    Destroy(gameObject);
    //}
    //
    //
    //private void OnCollisionExit(Collision collision)
    //{
    //    targetHealth = null;
    //}

}
