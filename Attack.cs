using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private float nextAttackTime;

    private Health targetHealth;

    public Bullet bullet;

    private IDamageable damageableTarget;

    private void Start()
    {
        damageableTarget = GetComponent<IDamageable>();
        var health = GetComponent<Health>();
        if (health != null)
        {
            health.OnTakeAttack += HandleTakeAttack;
        }
    }
    private void Awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextAttackTime && targetHealth != null)
        {
            damageableTarget.OnDamage(Constant.DAMAGE);
            //bullet.TakeAttack();

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

    private void HandleTakeAttack()
    {
        if (Time.time > nextAttackTime)
        {
            // 공격 로직
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
