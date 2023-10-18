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

    private void OnTriggerEnter(Collider other) // Ʈ���� ���� �� �� ��ɸ� �ҷ� �´� 
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
        foreach (Health potentialTarget in potentialTargets)
        {
            potentialTargets.Dequeue(); // ������ �� target �� �ƴҼ��� �ִ�? �׸��� ���� ������Ʈ�� ���� Ÿ���� �����ϰ� �ִ� 

            if (potentialTargets.Count > Constant.ZERO_COUNT)
            {
                target = potentialTargets.Peek().gameObject;
            }

            else
            {
                target = null;
                potentialTargets.Clear();
            }

            return;
        }
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
