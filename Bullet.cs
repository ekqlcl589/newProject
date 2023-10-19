using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bullet ��ü�� �ִ� RigidBody ������Ʈ ��ü
    private Rigidbody rigidBody;

    // Bullet ��ü�� �ʱ� ��ġ���� ������ ����
    private Vector3 startPosition;

    // Bullet �� �������� ������ ����
    private float bulletDamage;

    // Bullet �� �������� ������ ������ ���� �������� ������Ƽ
    public float BulletDamage
    {
        get { return bulletDamage; }
    }
    private void Awake()
    {
        // rigidBody ��ü�� RigidBody �� ����
        rigidBody = GetComponent<Rigidbody>();
        // �ʱ� ��ġ���� MonoBehaviour Ŭ������ �ʱ�ȭ �� ���� ��ġ���� ����
        startPosition = transform.position;
        // bulletDamage ������ Constant.DAMAGE(10f) �� ����
        bulletDamage = Constant.DAMAGE;
    }

    private void Start()
    {
        // �Ÿ��� �־����� ������Ű�� �ڷ�ƾ ����
        StartCoroutine(DeleteByDistance());
    }
    
    private void FixedUpdate()
    {
        // rigidBody�� AddForce �� ���� �����ִ� ����� �ϴ� �Լ��� ȣ��
        BulletMove();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �浹 �� ������Ʈ�� ������Ű�� �Լ� ����
        Delete();
    }

    private void Delete()
    {
        // �ڷ�ƾ ����
        StopCoroutine(DeleteByDistance());
        // Bullet ��ü ���� 
        Destroy(gameObject);
    }

    IEnumerator DeleteByDistance()
    {
        // rigidBody ������Ʈ�� null �� �ƴ϶��
        while(rigidBody != null) 
        {
            // startPosition ���� rigidBody.position�� ��ġ ���� ���� ������ ���� distanceToStartPosition ���� ����
            float distanceToStartPosition = Vector3.Distance(startPosition, rigidBody.position);
            // ���� distanceToStartPosition ���� Constant.BULLET_DELETE_DISTANCE(15f) ���� ũ�ٸ�
            if (distanceToStartPosition > Constant.BULLET_DELETE_DISTANCE)
            {
                // ������Ʈ�� ���� ��Ű�� �Լ� ����
                Delete();
                // �ڷ�ƾ ����
                yield break;
            }
            // Constant.BULLET_DELETE_TIME(3f) ��ŭ ����
            yield return new WaitForSeconds(Constant.BULLET_DELETE_TIME);
        }
    }

    private void BulletMove()
    {
        // rigidBody ������Ʈ�� null �� �ƴ϶��
        if (rigidBody != null)
        {
            // Bullet ������Ʈ�� ���� ������ ���� Constant.BULLET_POWER(1f) ��ŭ ���ؼ� ���� ������
            rigidBody.AddForce(transform.forward * Constant.BULLET_POWER);
        }

    }
}
