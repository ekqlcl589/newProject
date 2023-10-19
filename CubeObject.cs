using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeObject : MonoBehaviour
{
    private GameObject target;

    private Health health;

    private Queue<Health> potentialTargets = new Queue<Health>();

    private Movement movement;

    private BulletShooter bulletShooter;

    private void Awake()
    {
        health = GetComponent<Health>();
        movement = GetComponent<Movement>();
        bulletShooter = GetComponent<BulletShooter>();

    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateBullet());
        StartCoroutine(CheckTarget());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        ObjectFixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();

        if (bullet != null && health != null)
            health.SetMinusHp -= bullet.BulletDamage;
    }

    private void OnTriggerEnter(Collider other) // 트리거에 들어 왔으면 큐 에 저장만 한다
    {
        Health damageableTarget = other.GetComponent<Health>();

        if (damageableTarget != null)
        {
            potentialTargets.Enqueue(damageableTarget);
        }
    }

    // 주기적으로 타겟을 체크 하면서 타겟이 null 이면 큐에서 하나씩 빼서 타겟을 지정해준다.
    IEnumerator CheckTarget()
    {
        while (true) 
        {
            if (target == null && potentialTargets.Count > Constant.ZERO_COUNT)
            {
                target = potentialTargets.Dequeue().gameObject;

            }

            yield return new WaitForSeconds(Constant.WAIT_FOR_ONESECOND + 3f);
        }
    }

    // 초기화 시 필요한 함수들은 한 곳에 모아두고 
    IEnumerator CreateBullet()
    {
        while(bulletShooter != null) 
        {
            if (target != null)
            {
                bulletShooter.CreateBullet1();
            }

            yield return new WaitForSeconds(Constant.BULLET_ATTACK_DELAY);
        }
    }
    // update, fixed 등등 도 똑같음 
    private void ObjectFixedUpdate()
    {
        if (movement != null)
        {
            if(target != null) 
            {
                movement.MoveToTarget(target);
            }
            else
            {
                movement.RandomMove();
            }
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(CreateBullet());
        StopCoroutine(CheckTarget());

    }
}
