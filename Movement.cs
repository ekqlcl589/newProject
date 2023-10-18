using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private GameObject target;

    private List<Health> potentialTargets = new List<Health>();

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()

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
    IEnumerator Set_RandomMove()
    {
        if (target == null)
        {
            if (rigidBody != null)
            {
                Vector3 randomDirection = Random.insideUnitSphere * Constant.INSIDEUNITSPHERE;
                randomDirection.y = Constant.ZERO;

                float randomForce = Random.Range(Constant.MOVE_SPEED, Constant.FIND_TARGET);

                rigidBody.AddForce(randomForce * randomDirection);
            }
            else
            {
                // rigidBody�� null �̸� �ڷ�ƾ�� ����
                yield break;
            }

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

            damageableTarget.onDestroy += SetChangeTarget;

            if (target == null)
            {
                target = damageableTarget.gameObject;
            }
        }
    }

    private void SetChangeTarget()
    {
        foreach (Health potentialTarget in potentialTargets)
        {
            potentialTargets.Remove(potentialTarget);

            if (potentialTargets.Count > 0)
                target = potentialTargets[0].gameObject;
            else
                target = null;

            return;
        }
    }
}
