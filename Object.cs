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
        ObjectInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CheckTarget());

        ObjectUpdate();
    }

    void FixedUpdate()
    {
        ObjectFixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();

        if (bullet != null)
            health.SetMinusHp -= Constant.DAMAGE;
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
        if (target == null && potentialTargets.Count > Constant.ZERO_COUNT)
        {
            target = potentialTargets.Dequeue().gameObject;
            
            yield return new WaitForSeconds(Constant.WAIT_FOR_ONESECOND);
        }

    }

    // �ʱ�ȭ �� �ʿ��� �Լ����� �� ���� ��Ƶΰ� 
    private void ObjectInitialize()
    {
        if (movement != null)
        {
            movement.MoveInitialize(target);
        }
    }

    private void ObjectUpdate()
    {
        if (bulletShooter != null)
        {
            bulletShooter.CreateBulletStart(target);
        }

    }
    // update, fixed ��� �� �Ȱ��� 
    private void ObjectFixedUpdate()
    {
        if (movement != null)
        {
            movement.MoveUpdate(target);
        }
    }
}
