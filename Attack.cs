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
        bullet = GetComponent<Bullet>(); // �̷��� bullet ���� Attack �� ���������� ����� �� ����, ������Ʈȭ x
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

    // �ݸ����� �浹 �� Ÿ���� ����
    //private void OnCollisionEnter(Collision collision)
    //{
    //    //if(collision.collider.CompareTag("Cube") && collision.gameObject != gameObject)
    //    //{
    //    //        targetHealth = GetComponent<Health>();
    //    //
    //    //    //if(targetHealth != null)
    //    //    //    targetHealth.TakeDamage(damage);// = GetComponent<Health>();
    //    //    //targetHealth.OnDamage(damage);
    //    //    Debug.Log("�浹");
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
