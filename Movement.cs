using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveMent : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    private const float moveSpeed = 1f;
    private const float distance = 3f;

    private const float zeroPoint = 0f;
    void Start()
    {

    }

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

            direction.y = zeroPoint;

            // Ÿ���� ���� ȸ��
            Quaternion rotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = rotation;

            //Ÿ�ٰ��� �Ÿ� ���
            float distanceToPosition = Vector3.Distance(transform.position, target.position);

            // Ÿ�ٰ��� �Ÿ��� distance ���� ũ�ٸ� �̵� 
            if (distanceToPosition > distance)
            {
                Vector3 move = direction.normalized * moveSpeed * Time.deltaTime;
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
