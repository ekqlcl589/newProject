using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private GameObject target;

    private Health temporaryObject;

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    private void Update()
    {
        MoveToTarget();
        StartCoroutine(Set_RandomMove());
    }

    private void MoveToTarget()
    {
        if (target != null)
        {
            // ���� ����
            Vector3 direction = target.transform.position - transform.position;

            direction.y = Constant.ZERO_POINT;

            // Ÿ���� ���� ȸ��

            Quaternion rotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = rotation;

            //Ÿ�ٰ��� �Ÿ� ���
            float distanceToPosition = Vector3.Distance(transform.position, target.transform.position);

            // Ÿ�ٰ��� �Ÿ��� distance ���� ũ�ٸ� �̵� 
            if (distanceToPosition > Constant.DISTANCE)
            {
                Vector3 move = direction.normalized * Constant.MOVE_SPEED * Time.deltaTime;
                transform.position += move;
            }
        }
    }
    // Ʈ���ſ� ������ ���� ������ �����ؼ� ������ �ִ´� 
    private void OnTriggerEnter(Collider other)
    {
        if(target == null)
        {
            Health damageableTarget = other.GetComponent<Health>(); // ���� ��¥ ���� ����� ���� ������� �����ؾ� �ϴµ� �׷� �迭�� �޾Ƽ� �����ߴٰ� 
            // ù ��° �迭 ���� ������� exit ���� �ҷ��;� �Ѵ�.

            if(damageableTarget != null )
                target = damageableTarget.gameObject;
        }
        else if( target != null && temporaryObject == null)
        {
            // Ÿ���� �̹� ������ �ְ� �ӽ� ��ü�� ��� �ִٸ�
            temporaryObject = other.GetComponent<Health>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �׷��ٰ� target �� �׾ null �� �ǰ� �ӽ÷� ����� temp ��ü�� null �� �ƴ϶��?
        if (target == null && temporaryObject != null)
        {
            // ���ο� Ÿ���� temp �� �ǰ� 
            target = temporaryObject.gameObject;

            // �ӽ� ��ü���� temp �� null �� ����� �ش�
            temporaryObject = null;
        }
    }
    IEnumerator Set_RandomMove()
    { // ���� �ؾ� ��
        if(target == null) 
        {
            Vector3 randomDirection = Random.insideUnitSphere * Constant.INSIDEUNITSPHERE;

            randomDirection.y = Constant.ZERO;
            
            float randomForce = Random.Range(Constant.MOVE_SPEED, Constant.FIND_TARGET);

            rigidBody.AddForce(randomForce * randomDirection);

            yield return new WaitForSeconds(Constant.MOVE_TIME);
        }
    }
}
