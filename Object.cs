using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
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
        BulletFire();
        health.onDestroy += SetChangeTarget;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();

        if (bullet != null && bullet.IsAttack)
            health.MinusHp -= Constant.DAMAGE;

        Debug.Log(health.MinusHp);
    }

    private void OnTriggerEnter(Collider other) // 트리거 됐을 때 그 기능만 불러 온다 
    {
        Health damageableTarget = other.GetComponent<Health>();

        if (damageableTarget != null)
        {
            potentialTargets.Enqueue(damageableTarget);
            damageableTarget.onDestroy += SetChangeTarget;
            if (target == null)
            {
                target = potentialTargets.Dequeue().gameObject;

            }
        }

    }

    private void SetChangeTarget()
    {
        // 다른 잠재적인 타겟이 있는 동안 계속 반복
        while (potentialTargets.Count > 0)
        {
            Health potentialTarget = potentialTargets.Dequeue();

            // 만약 잠재적인 타겟이 살아있다면
            if (potentialTarget != null)
            {
                target = potentialTarget.gameObject;
                return; 
            }
        }

        // 모든 잠재적인 타겟이 죽었거나 null일 경우, target을 null로 설정하고 큐를 비웁니다.
        target = null;
        potentialTargets.Clear();
    }

    private void Move()
    {
        if (movement != null)
        {
            movement.MoveUpdate(target);
        }
    }

    private void BulletFire()
    {
        if (bulletShooter != null)
        {
            bulletShooter.BulletCreate();
        }

    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
