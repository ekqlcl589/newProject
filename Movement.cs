using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private GameObject target;

    private List<Health> potentialTargets = new List<Health>();

    Health damageableTarget;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        StartCoroutine(Set_RandomMove());
    }

    // Update is called once per frame
    private void Update()
    {
        MoveToTarget();

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
    IEnumerator Set_RandomMove()
    {
        while(target == null) 
        {
            Vector3 randomDirection = Random.insideUnitSphere * Constant.INSIDEUNITSPHERE;

            randomDirection.y = Constant.ZERO;
            
            float randomForce = Random.Range(Constant.MOVE_SPEED, Constant.FIND_TARGET);

            rigidBody.AddForce(randomForce * randomDirection);

            yield return new WaitForSeconds(Constant.MOVE_TIME);
        }
    }

     //Ʈ���ſ� ������ ���� ������ �����ؼ� ������ �ִ´� 
    private void OnTriggerEnter(Collider other)
    {
        Health damageableTarget = other.GetComponent<Health>();

        if (damageableTarget != null)
        {
            potentialTargets.Add(damageableTarget);

            if (target == null)
            {
                target = damageableTarget.gameObject;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Health damageableTarget = other.GetComponent<Health>();

        if (damageableTarget != null)
        {
            potentialTargets.Remove(damageableTarget);

            if (target == damageableTarget.gameObject)
            {
                foreach (Health potentialTarget in potentialTargets)
                {
                    if (potentialTarget.gameObject.activeSelf)
                    {
                        target = potentialTarget.gameObject;
                        return;
                    }
                }

                target = null;
            }
        }
    }
    private void OnDestroy()
    {
        if (target != null && target.gameObject == this.gameObject)
        {
            // ������ Ÿ���� ���� Ÿ���̶��
            if (potentialTargets.Count > 0)
            {
                // ���� ������ ������Ʈ�� Ÿ������ ����
                target = potentialTargets[0].gameObject;
            }
            else
            {
                // �ٸ� ������Ʈ�� ���� ���, Ÿ���� null �� ����
                target = null;
            }
        }
    }
}
