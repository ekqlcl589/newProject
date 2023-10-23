using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bullet ��ü�� �ִ� RigidBody ������Ʈ ��ü
    private Rigidbody rigidBody;

    // Bullet ��ü�� �ʱ� ��ġ���� ������ ����
    private Vector3 startPosition;

    // �ڷ�ƾ�� ���� �Ǿ�� �� �� �������� �ʴ� �ڷ�ƾ ���� ��ž�ڷ�ƾ�� �ɾ "���ʿ��� ������ ���̱� ����" Coroutine �� ��ȯ�ϴ� ��ü���� ����
    private Coroutine DeleteByDistanceCoroutine;

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
    }

    private void Start()
    {
        // �ʱ� ��ġ���� MonoBehaviour Ŭ������ �ʱ�ȭ �� ���� ��ġ���� ����
        startPosition = transform.position;
        // bulletDamage ������ Constant.DAMAGE(10f) �� ����
        bulletDamage = Constant.DAMAGE;
        // �Ÿ��� �־����� ������Ű�� �ڷ�ƾ ����
        DeleteByDistanceCoroutine = StartCoroutine(DeleteByDistance());
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

    // 3�ʿ� �� ���� bullet �� ���ư� �Ÿ��� ����ϱ� ���� �ڵ� 
    IEnumerator DeleteByDistance()
    {
        // rigidBody ������Ʈ�� null �� �ƴ� ���¿����� �ڷ�ƾ�� ����Ǿ�� ��
        while(rigidBody != null) 
        {
            // ���ư� �Ÿ��� ���ϱ� ���� Vector3.Distance �Լ��� ���� ���� ������ 
            float distanceToStartPosition = Vector3.Distance(startPosition, rigidBody.position);
            // ���� �Ÿ��� Constant.BULLET_DELETE_DISTANCE(15f) ���� ũ�ٸ� ���� ��Ű�� ����
            if (distanceToStartPosition > Constant.BULLET_DELETE_DISTANCE)
            {
                // ������Ʈ�� ���� ��Ű�� �Լ� ����
                Delete();
                // �ڷ�ƾ ����
                yield break;
            }
            // ��� �Ÿ��� ���ϴ� ���� �ƴ϶� �ڷ�ƾ���� Constant.BULLET_DELETE_TIME(3f) ��ŭ ���� �� �ٽ� ���
            // ��, 3�� ���� �ѹ��� ���
            yield return new WaitForSeconds(Constant.BULLET_DELETE_TIME);
        }
    }

    private void BulletMove()
    {
        // rigidBody ������Ʈ�� null �̶��
        if (rigidBody == null)
            return; // ����

        // rigidBody�� null �� �ƴ϶�� bullet �� ������ ���ư��⸸ �ϸ� �Ǳ� ������ AddForce�� ���� ������
        rigidBody.AddForce(transform.forward * Constant.BULLET_POWER);
        

    }
    private void Delete()
    {
        // Delete()�Լ��� ȣ��� �� DeleteByDistanceCoroutine�� �������̶�� �ڷ�ƾ ����
        if (DeleteByDistanceCoroutine != null)
            StopCoroutine(DeleteByDistance());
        // Bullet ��ü ���� 
        Destroy(gameObject);
    }
}
