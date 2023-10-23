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
    private List<Health> potentialTargets = new List<Health>();

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
        // FixedUpdate �� ������ ������(0.02) ���� ȣ���� �ؼ� ������ �������� ������ �� �� ���� -> input ���� �����̴� �� �ƴ϶� 
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
            potentialTargets.Add(damageableTarget);
        }
    }

    // �� �Լ��� ���� ��ü�� Ʈ���Ÿ� Ż���� ������Ʈ�� ť���� ���� �ϱ� ���� ����, target = null �� ������ ������ target = null �� ������ �ƴ�
    // ���ο� ť�� �Ҵ� �����ν� ���� �����͸� ��Ű�鼭 ���ο� ť�� ����� �ʿ��� �׸� �����ϸ� ���� ť�� ���� �����ϴ� ������ ���� �� �־ ���ο� ť�� ����
    private void OnTriggerExit(Collider other)
    {
        Health exitTarget = other.GetComponent<Health>();

        if (exitTarget != null)
        {
            // ����Ʈ�� ����ϸ� �ʿ��� ������ ������ remove �ϸ� ��
            // �������� Ÿ���� ���� Ÿ�ٰ� ���� �ϴٸ� ����Ʈ���� ������ �ְ� target �� null �� ����� ��
            potentialTargets.Remove(exitTarget); // ���� �� Ÿ���� ����Ʈ�� ��ϵ��� ���� Ÿ���̶��? �� �̹� enter ���� health �� ���� ����Ʈ�� �߰��� �� ������ �� �� üũ �� �ʿ䰡 ����
            
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
        // CheckTarget �Լ��� ������Ʈ�� �����ϴ� �� ��� �˻縦 �ϱ� �ؾ��ؼ� gameObject(��ü)�� null �� �ƴ϶�� �ݺ���Ŵ -> ����� OnDestroy ���� ����
        while (gameObject != null)
        {
            // ť�� �� ���ڸ� Ÿ������ ��ƾ� �ϹǷ� ť�� �ϳ��� ���ڰ��� ��� �ִٸ�
           if(potentialTargets.Count > Constant.ZERO_COUNT)
            {
                // ť�� ���ڴ� �� ������ Ÿ���� �̹� ���� �ִ� ���¶�� Ÿ�� ������ ������ �ʾƵ� ��
                // �׸��� ���⼭ ť�� ���ڰ� null ���� �Ǵ� ���� �ʾƵ� �� �̹� TriggerEnter ���� null üũ�� �ϰ� ť�� �־��ֱ� ����
                if (target == null)
                {       
                    target = potentialTargets[0].gameObject; // ����Ʈ�� �� �ִ� ù ��° ���ڸ� Ÿ������ ����
                    potentialTargets.RemoveAt(0);
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
    // movement ������Ʈ�� ���� ������ �������� �����ϴ� �Լ����� 2������ target �� �ֳ� ���Ŀ� ���� ������ �� �ֱ� ������ if, else ������ üũ
    private void MoveFixedUpdate()
    {
        // FixedUpdate ���� ȣ��� �� movement ������Ʈ�� null �̶�� return ���Ѽ� ȣ�� ����
        if (movement == null)
            return;

        // Ÿ���� ������ ���� ������ �������� ���� ��, target �� ���� �����̴� �ൿ�� �� �� �����ϱ� ���� if, else �� �Լ� ����
        if (target != null)
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
