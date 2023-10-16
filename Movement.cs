using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private GameObject target;

    private List<Health> potentialTargets = new List<Health>();

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
        //�浹 ���� �� ó�� Ÿ������ ��� 
        Health damageableTarget = other.GetComponent<Health>();

        if (damageableTarget != null)
        {
            potentialTargets.Add(damageableTarget);

            // ���� ���� Ÿ���� null �̶�� ù ��° ������Ʈ�� Ÿ������ ����
            if (target == null)
            {
                target = damageableTarget.gameObject;
                //target = potentialTargets[0].gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Health damageableTarget = other.GetComponent<Health>();

        if (damageableTarget != null)
        {
            potentialTargets.Remove(damageableTarget);

            // ���� ���� Ÿ���� �� ������Ʈ�� ���
            if (target == damageableTarget.gameObject)
                {
                // �ٸ� ������Ʈ�� Ʈ���� �ȿ� ������ �� �� �ϳ��� ���ο� Ÿ������ ����
                if (potentialTargets.Count > Constant.COUNT_ZERO)
                {
                    //for(int i = 0; i <potentialTargets.Count; i++)
                        target = potentialTargets[0].gameObject;
                }
                else
                {
                    // �ٸ� ������Ʈ�� ������ Ÿ���� null �� ����
                    target = null;
                }
            }
        }
    }
    IEnumerator Set_RandomMove()
    {
        //while ������ �ٲ㼭 target == null �̸� �ڷ�ƾ ���� != �̸� ����
        while(target == null) 
        {
            Vector3 randomDirection = Random.insideUnitSphere * Constant.INSIDEUNITSPHERE;

            randomDirection.y = Constant.ZERO;
            
            float randomForce = Random.Range(Constant.MOVE_SPEED, Constant.FIND_TARGET);

            rigidBody.AddForce(randomForce * randomDirection);

            yield return new WaitForSeconds(Constant.MOVE_TIME);
        }
    }
}
