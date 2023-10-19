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

    private void OnTriggerEnter(Collider other) // Ʈ���ſ� ��� ������ ť �� ���常 �Ѵ�
    {
        Health damageableTarget = other.GetComponent<Health>();

        if (damageableTarget != null)
        {
            potentialTargets.Enqueue(damageableTarget);
        }
    }

    // �ֱ������� Ÿ���� üũ �ϸ鼭 Ÿ���� null �̸� ť���� �ϳ��� ���� Ÿ���� �������ش�.
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

    // �ʱ�ȭ �� �ʿ��� �Լ����� �� ���� ��Ƶΰ� 
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
    // update, fixed ��� �� �Ȱ��� 
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
