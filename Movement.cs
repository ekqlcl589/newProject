using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveMent : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (target != null)
        {
            // ���� ����
            Vector3 direction = target.position - transform.position;

            direction.y = Constant.ZERO_point;

            // Ÿ���� ���� ȸ��
            Quaternion rotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = rotation;

            //Ÿ�ٰ��� �Ÿ� ���
            float distanceToPosition = Vector3.Distance(transform.position, target.position);

            // Ÿ�ٰ��� �Ÿ��� distance ���� ũ�ٸ� �̵� 
            if (distanceToPosition > Constant.DISTANCE)
            {
                Vector3 move = direction.normalized * Constant.MOVE_SPEED * Time.deltaTime;
                transform.position += move;
            }
            else
            {
                //����
            }
        }
    }

    //�ٸ� Ÿ������ ������ �� ����� �Լ� 
    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null && gameObject == null)
            return;

        target = newTarget;
    }

}
