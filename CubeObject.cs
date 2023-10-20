using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // �ڷ�ƾ�� ���� ������ Ȯ�� �ϱ� ���� ����
    private Coroutine CreateBulletCoroutin;
    private Coroutine CheckTargetCoroutin;

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
        CreateBulletCoroutin = StartCoroutine(CreateBullet());
        // ������ Ÿ�� ���� �ڷ�ƾ ����
        CheckTargetCoroutin = StartCoroutine(CheckTarget());
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
        // �浹�� ������Ʈ�� Bullet Ŭ������ ��ü�� ����
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        // ������ ��ü�� null �� �ƴϰų� ü���� null �� �ƴ϶��
        if (bullet != null && health != null)
            // �Ѿ��� ���ݷ� ��ŭ ü�� ����
            health.SetMinusHp -= bullet.BulletDamage;

    }

    private void OnTriggerEnter(Collider other) // Ʈ���ſ� ��� ������ ť �� ���常 �Ѵ�
    {
        // Ʈ���ſ� �浹�� ������Ʈ�� Health ��ü�� ����
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
        // Ʈ���ſ� �浹�� ������Ʈ�� Health ��ü�� ����
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

            // ���� ���� Ÿ���� ������ Ÿ���̸� ���� Ÿ�ٵ� null �� ����
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
            if (target == null && potentialTargets.Count() > Constant.ZERO_COUNT)
            {
                // ť�� ���ڰ� null �� �ƴϰ� ���� Ÿ���� ť�� ���ڿ� ���� �ʴٸ�
                if (potentialTargets.Dequeue().gameObject != null && potentialTargets.Dequeue().gameObject != target)
                // Ÿ�ٿ� ť�� �� ù ��° ������ ������Ʈ ������ ����
                    target = potentialTargets.Dequeue().gameObject;


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
                // �Ѿ� �߻� ������Ʈ�� Shot �Լ� ����
                bulletShooter.Shot();
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

    private void OnDestroy()
    {
        // �ڷ�ƾ �Լ��� ���� ������ Ȯ�� �ϰ� 
        if (CreateBulletCoroutin != null)
        // CreateBullet �ڷ�ƾ ����
            StopCoroutine(CreateBullet());

        if(CheckTargetCoroutin != null)
        // CheckTarget �ڷ�ƾ ����
            StopCoroutine(CheckTarget());

    }
}
