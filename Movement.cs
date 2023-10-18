using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    //private GameObject target;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }


    public void MoveUpdate(GameObject target)
    {
        if(target == null)
            StartCoroutine(Set_RandomMove(target));
        else
            MoveToTarget(target);
    }

    private void MoveToTarget(GameObject target)
    {
        //if (target != null) ��Į������ ����� 
        //{
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
        //}
    }
    private IEnumerator Set_RandomMove(GameObject target)
    {
        //if (target == null)
        //{
            if (rigidBody != null)
            {
                Vector3 randomDirection = Random.insideUnitSphere * Constant.INSIDEUNITSPHERE;
                randomDirection.y = Constant.ZERO_POINT;

                float randomForce = Random.Range(Constant.MOVE_SPEED, Constant.FIND_TARGET);

                rigidBody.AddForce(randomForce * randomDirection.normalized);
            }
            else
            {
                // rigidBody�� null �̸� �ڷ�ƾ�� ����
                yield break;
            }

            yield return new WaitForSeconds(Constant.MOVE_TIME);
        //}
    }
}
