using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public GameObject target;

    // Update is called once per frame
    private void Update()
    {
        Move();

        ChangeTarget();
    }

    private void Move()
    {
        if (target != null)
        {
            // ���� ����
            Vector3 direction = target.transform.position - transform.position;

            direction.y = Constant.ZERO_POINT;

            // Ÿ���� ���� ȸ��
            Quaternion rotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = rotation;
            //transform.Rotate(direction * Time.deltaTime * Constant.MOVE_SPEED);

            //Ÿ�ٰ��� �Ÿ� ���
            float distanceToPosition = Vector3.Distance(transform.position, target.transform.position);

            // Ÿ�ٰ��� �Ÿ��� distance ���� ũ�ٸ� �̵� 
            if (distanceToPosition > Constant.DISTANCE)
            {
                Vector3 move = direction.normalized * Constant.MOVE_SPEED * Time.deltaTime;
                transform.position += move;
            }
        }
        else
        {
            Vector3 randomMove = new Vector3(Random.Range(-Constant.ONE_POINT, Constant.ONE_POINT), Constant.ZERO_POINT, Random.Range(-Constant.ONE_POINT, Constant.ONE_POINT));
            transform.position += randomMove.normalized * Time.deltaTime;
        }
    }

    private void ChangeTarget()
    {
        if(target == null)
        {
            Collider[] collider = Physics.OverlapSphere(transform.position, Constant.FIND_TARGET);

            for(int i = 0; i < collider.Length; i++) 
            {
                Health damageableTarget = collider[i].GetComponent<Health>();

                if(damageableTarget != null && damageableTarget != this && collider[i].CompareTag("Target"))
                    target = damageableTarget.gameObject;
            }
        }
    }
}
// ���� ť�갡 3����� �ٸ� Ÿ�� ��°͵� �ؾ���
// �׷��ٸ� ť�� �߽ɿ��� �ٸ� �ݶ��̴��� Ž���ϴ� �� �߰��ؼ� �ݶ��̴��� Ž���ϸ� �ٽ� Ÿ������ ����  