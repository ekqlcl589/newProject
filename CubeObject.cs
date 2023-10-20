using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CubeObject : MonoBehaviour
{
    // ������, ���� ���� ������ ������Ʈ�� �����Լ� target �� ����
    private GameObject target;
    // ü���� �����ϴ� ������Ʈ
    private Health health;
    // ������ ����� �����ϴ� ������Ʈ
    private Movement movement;
    // �Ѿ� �߻� ����� �����ϴ� ������Ʈ 
    private BulletShooter bulletShooter;

    //Ʈ���ſ� "�����ϴ� �������" (ü���� ����)Ÿ���� ���� ���ֱ� ���� ���Լ���(<-������<-) ���¸� ���� ť(��⿭)�� �����̳ʸ� ����
    private Queue<Health> potentialTargets = new Queue<Health>();

    // �ڷ�ƾ�� ���� �Ǿ�� �� �� �������� �ʴ� �ڷ�ƾ ���� ��ž�ڷ�ƾ�� �ɾ "���ʿ��� ������ ���̱� ����" Coroutine �� ��ȯ�ϴ� ��ü���� ����
    private Coroutine CreateBulletCoroutin;
    private Coroutine TargetSettingCoroutin;

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
        // CreateBulletCoroutin���� �ڷ�ƾ �޼��尡 ����(true)�� ���� �ƴ���(false) �����ϱ� ���� 
        CreateBulletCoroutin = StartCoroutine(CreateBullet());
        TargetSettingCoroutin = StartCoroutine(TargetSetting());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // update �� �� ������ ���� ȣ���� �Ǽ� �����ӿ� ���� ȣ��Ǵ� ������ �ٸ���
        // FixedUpdate �� ������ ������(0.2) ���� ȣ���� �ؼ� ������ �������� ������ �� �� ���� -> input ���� �����̴� �� �ƴ϶� 
        MoveFixedUpdate();
    }

    // �ݸ����� �����ϴ� ���� �� �ѹ� ü���� �𿩾� �ϱ� ������ Enter ���� ü�� ����
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� Bullet ������ �ƴ��� �Ǻ��ϰ�, Bullet �� �̰� health �� �����Ѵٸ� �������� �ֱ� ���� ColligionEnter���� ����
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        // ������ ��ü�� null �� �ƴϰų� ü���� null �� �ƴ϶��
        if (bullet != null && health != null)
            // �Ѿ��� ���ݷ� ��ŭ ü�� ����
            health.SetMinusHp -= bullet.BulletDamage;

    }

    // OnTriggerEnter�� Ʈ���� "������ �Ͼ�� �������� �� ���� ȣ��"�� �Ǳ� ������ Ÿ�� ������ �ٸ� ������ �ϰ� enter ������ ť���� �־��ش�
    // ���⼭ Ÿ��(target) ���� ������ ������ Ʈ���ſ� �ٸ� ������ Ÿ�ٵ�(CubeObject)�� ������ ���� Ÿ���� ���ο� Ÿ������ ���� ������ ����
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

    // �� �Լ��� ���� ��ü�� Ʈ���Ÿ� Ż���� ������Ʈ�� ť���� ���� �ϱ� ���� ����, target = null �� ������ ������ target = null �� ������ �ƴ�
    private void OnTriggerExit(Collider other)
    {
        Health exitTarget = other.GetComponent<Health>();

        if (exitTarget != null)
        {
            Queue<Health> newQueue = new Queue<Health>();

            // ���� ť�� ��ȸ�ϸ鼭 ���ϴ� Ÿ���� �ǳ� ��
            while (potentialTargets.Count > Constant.ZERO_COUNT)
            {
                // ���ο� Ÿ�ٿ� ���� ť�� ������ ���ڸ� ����
                Health target = potentialTargets.Dequeue();
                // ���ο� Ÿ���� Ʈ���� ������ ������ Ÿ�ٰ� �ٸ��ٸ�
                if (target != exitTarget)
                {
                    newQueue.Enqueue(target);

                }
            }
            
            // ť ����
            potentialTargets = newQueue;

            if (exitTarget.gameObject == target)
            {
                target = null;
            }
        }
    }
    // Ÿ���� �����ϴ� ������ ���ؾ� �ϴµ� Update �� FixedUpdate ���� ��� ȣ�� �ϴ� �� ����ȭ �κп��� �ſ� ���� �ʱ� ������ 
    // �ֱ������� ȣ���� �� �ִ� �ڷ�ƾ���� �Լ��� ����
    // �� �Լ��� Ÿ���� ���� ���ִ� �� ������ �Լ��� �̸� ����
    IEnumerator TargetSetting()
    {
        // CheckTarget �Լ��� ������Ʈ�� �����ϴ� �� ��� �˻縦 �ϱ� �ؾ��ؼ� while �� true �� �ְ� �ݺ���Ŵ -> ����� OnDestroy ���� ����
        while (true)
        {
            // ť�� �� ���ڸ� Ÿ������ ��ƾ� �ϹǷ� ť�� �ϳ��� ���ڰ��� ��� �ִٸ�
           if(potentialTargets.Count > Constant.ZERO_COUNT)
            {
                // ť�� ���ڴ� �� ������ Ÿ���� �̹� ���� �ִ� ���¶�� Ÿ�� ������ ������ �ʾƵ� ��
                // �׸��� ���⼭ ť�� ���ڰ� null ���� �Ǵ� ���� �ʾƵ� �� �̹� TriggerEnter ���� null üũ�� �ϰ� ť�� �־��ֱ� ����
                if (target == null)
                {       
                    target = potentialTargets.Dequeue().gameObject;
                }
            }
            yield return new WaitForSeconds(Constant.WAIT_FOR_ONESECOND);
        }
    }

    // �Ѿ� �߻�� Ÿ���� �ִٸ� �ֱ������� ��� ������ ����� �ؼ� �Լ��� ���� ��Ű�� �ٽ� ������ �� �ִ� �ڷ�ƾ�� ���
    IEnumerator CreateBullet()
    {
        // �߻� ��Ű�� ������Ʈ�� ���� �ؾ߸� Bullet �� ������ �� �����Ƿ� while ������ bulletShooter != null ���� ����
        while (bulletShooter != null) 
        {
            // �Ѿ��� Ÿ���� ������ �߻� �ϰ� ������ �������� �ʾƵ� �� -> ������� ������ ���� 
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
            // Ÿ���� ������ ���� ������ �������� ���� ��, target �� ���� �����̴� �ൿ�� �� �� �����ϱ� ���� if, else �� �Լ� ����
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
        // ���������� ���� �Լ��� ���� �ϴ� ��ȿ�� ���� �ൿ�� �� �ϱ� ���� null üũ
        if(TargetSettingCoroutin != null)
        // CheckTarget �ڷ�ƾ ����
            StopCoroutine(TargetSetting());

    }
}
