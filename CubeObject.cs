using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeObject : MonoBehaviour
{
    // ������, ���� ���� ������ ������Ʈ
    private GameObject target;
    // ü���� �����ϴ� ������Ʈ
    private Health health;
    // ������ Ÿ�ٵ��� �����ϴ� �����̳� �Ҵ�
    private Queue<Health> potentialTargets = new Queue<Health>();
    // ������ ����� �����ϴ� ������Ʈ
    private Movement movement;
    // �Ѿ� �߻� ����� �����ϴ� ������Ʈ 
    private BulletShooter bulletShooter;

    private void Awake()
    {
        // ü�� ��� ������Ʈ ��������
        health = GetComponent<Health>();
        // ������ ��� ������Ʈ ��������
        movement = GetComponent<Movement>();
        // �Ѿ� �߻� ��� ������Ʈ ��������
        bulletShooter = GetComponent<BulletShooter>();

    }
    // Start is called before the first frame update
    void Start()
    {
        // �Ѿ� �߻� �ڷ�ƾ ����
        StartCoroutine(CreateBullet());
        // ������ Ÿ�� ���� �ڷ�ƾ ����
        StartCoroutine(CheckTarget());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // �����̴� �Լ� ����
        MoveFixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �Ѿ� ������Ʈ�� Bullet Ŭ������ ��ü�� ����
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        // ������ ��ü�� null �� �ƴϰų� ü���� null �� �ƴ϶��
        if (bullet != null && health != null)
            // �Ѿ��� ���ݷ� ��ŭ ü�� ����
            health.SetMinusHp -= bullet.BulletDamage;
    }

    private void OnTriggerEnter(Collider other) // Ʈ���ſ� ��� ������ ť �� ���常 �Ѵ�
    {
        // Ʈ���ſ� �浹�� ü�� ���� ����� Health ��ü�� ����
        Health damageableTarget = other.GetComponent<Health>();
        // ������ Health ��ü�� null �� �ƴ϶��
        if (damageableTarget != null)
        {
            // ������ Ÿ�ٵ��� �����ϴ� �����̳�(ť)�� ����
            potentialTargets.Enqueue(damageableTarget);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Ʈ���� �������� Ÿ���� ���� �� ó��
        Health exitTarget = other.GetComponent<Health>();

        // Ʈ���ſ��� ������ Ÿ���� null �� �ƴ϶��
        if (exitTarget != null)
        {
            // ���ο� ť ����
            Queue<Health> newQueue = new Queue<Health>();

            // ���� ť�� ��ȸ�ϸ鼭 ���ϴ� Ÿ���� �ǳ� ��
            while (potentialTargets.Count > Constant.ZERO_COUNT)
            {
                // ���ο� Ÿ�ٿ� ���� ť�� ������ ���ڸ� ����
                Health target = potentialTargets.Dequeue();
                // ���ο� Ÿ���� Ʈ���� ������ ������ Ÿ�ٰ� �ٸ��ٸ�
                if (target != exitTarget)
                {
                    // ���ο� ť�� ���ο� ť�� ����
                    newQueue.Enqueue(target);
                }
            }

            // ���� ť�� ���ο� ť�� ��ü
            potentialTargets = newQueue;

            // ���� ���� Ÿ���� ������ Ÿ���̸� ���� Ÿ�ٵ� null�� ����
            if (exitTarget.gameObject == target)
            {
                target = null;
            }
        }
    }
    // �ֱ������� Ÿ���� üũ �ϸ鼭 Ÿ���� null �̸� ť���� �ϳ��� ���� Ÿ���� �������ش�.
    IEnumerator CheckTarget()
    {
        // start ���� ���� �ݺ��ϸ鼭
        while (true) 
        {
            // Ÿ���� null �̰�, ������ Ÿ�ٵ��� �����ϴ� �����̳��� ����� ���ڰ� 0�� �̻��̶��
            if (target == null && potentialTargets.Count > Constant.ZERO_COUNT)
            {
                // Ÿ���� �����̳��� ó�� �� ������ ������Ʈ ������ ����
                target = potentialTargets.Dequeue().gameObject;
                // ���� Ÿ���� null �̶��
                if(target == null)
                    // �ڷ�ƾ ���� 
                    yield break;

            }
            // Constant.WAIT_FOR_ONESECOND(1��) ��ŭ ���� 
            yield return new WaitForSeconds(Constant.WAIT_FOR_ONESECOND);
        }
    }

    
    IEnumerator CreateBullet()
    {
        // �ҷ��� bulletShooter ������Ʈ�� null �� �ƴ϶�� start ���� ���� �ݺ�
        while (bulletShooter != null) 
        {
            // ���� Ÿ���� null �� �ƴ϶��
            if (target != null)
            {
                // �Ѿ� �߻� ������Ʈ�� CreateBullet �Լ� ����
                bulletShooter.CreateBullet1();
            }
            // Constant.BULLET_ATTACK_DELAY(2��) ��ŭ ����
            yield return new WaitForSeconds(Constant.BULLET_ATTACK_DELAY);
        }
    }
    // FixedUpdate ���� ȣ��� ������ �Լ����� target �� ������ ���� ����
    private void MoveFixedUpdate()
    {
        // movement ������Ʈ�� null �� �ƴ϶��
        if (movement != null)
        {
            // Ÿ���� null �� �ƴ϶��
            if(target != null) 
            {
                // Ÿ���� ���� �����̴� �Լ� ����(CubeObject���� ������ target �� ������ ���ڰ����� �Ѱ���)
                movement.MoveToTarget(target);
            }
            // Ÿ���� null �̶��
            else
            {
                // ������ �������� ������ �Լ� ����
                movement.RandomMove();
            }
        }
    }

    // CubeObject �� �����Ǹ� 
    private void OnDestroy()
    {
        // CreateBullet �ڷ�ƾ ����
        StopCoroutine(CreateBullet());
        // CheckTarget �ڷ�ƾ ����
        StopCoroutine(CheckTarget());

    }
}
