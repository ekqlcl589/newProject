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
        // �Ÿ� ��� ���� �� ��꿡 �ʿ��� �ʱ� ��ġ���� MonoBehaviour Ŭ������ �ʱ�ȭ �� ���� ��ġ���� ����
        startPosition = transform.position;
        // bulletDamage ������ Constant.DAMAGE(10f) �� ����
        bulletDamage = Constant.DAMAGE;
        // �ڷ�ƾ �޼��尡 ����(true)�� ���� �ƴ���(false) �����ϱ� ���� �� ����
        DeleteByDistanceCoroutine = StartCoroutine(DeleteByDistance());
    }
    
    private void FixedUpdate()
    {
        BulletMove();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // bullet �� �浹 �� �߰� �浹�� ���� ���� ����
        Delete();
    }

    private void Delete()
    {
        // DeleteByDistanceCoroutine�� �������̶�� �ڷ�ƾ ����
        if (DeleteByDistanceCoroutine != null)
            StopCoroutine(DeleteByDistance());

        Destroy(gameObject);
    }

    // ù �߻�� ��ġ�� ���ư��� Bullet �� �Ÿ��� ���Ͽ� �Ÿ��� �־����ٸ� Bullet �� ������Ű��, �ڷ�ƾ�� ���߱� ���� �ڵ�   
    IEnumerator DeleteByDistance()
    {
        // rigidBody ������Ʈ�� null �̸� ������ �� �����Ƿ� �ݺ��� �������� ���
        while(rigidBody != null) 
        {
            // ���ư� �Ÿ��� ���ϱ� ���� Vector3.Distance �Լ��� ���� ���� ����ϰ� �ı� �������� ��� 
            float distanceToStartPosition = Vector3.Distance(startPosition, rigidBody.position);
            // ���� distanceToStartPosition ���� ������(Constant.BULLET_DELETE_DISTANCE(15f)) ���� ũ�ٸ� ���� ��Ű�� ������ �༭ �Ÿ��� �ִٸ� ����
            if (distanceToStartPosition > Constant.BULLET_DELETE_DISTANCE)
            {
                Delete();
            }
            // ��� �Ÿ��� ���ϴ� ���� �ƴ϶� �ڷ�ƾ���� Constant.BULLET_DELETE_TIME(3f) ��ŭ ���� �� �ٽ� ���
            yield return new WaitForSeconds(Constant.BULLET_DELETE_TIME);
        }
    }

    private void BulletMove()
    {
        // rigidBody ������Ʈ null üũ�� ���� �Լ� ���� ����
        if (rigidBody == null)
            return;

        // rigidBody�� null �� �ƴ϶�� bullet �� ������ ���ư��⸸ �ϸ� �Ǳ� ������ AddForce�� ���� ������
        rigidBody.AddForce(transform.forward * Constant.BULLET_POWER);
        

    }
}
