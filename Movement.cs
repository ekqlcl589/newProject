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
            // 방향 벡터
            Vector3 direction = target.transform.position - transform.position;

            direction.y = Constant.ZERO_POINT;

            // 타겟을 향해 회전
            Quaternion rotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = rotation;
            //transform.Rotate(direction * Time.deltaTime * Constant.MOVE_SPEED);

            //타겟과의 거리 계산
            float distanceToPosition = Vector3.Distance(transform.position, target.transform.position);

            // 타겟과의 거리가 distance 보다 크다면 이동 
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
// 만약 큐브가 3개라면 다른 타겟 잡는것도 해야함
// 그렇다면 큐브 중심에서 다른 콜라이더를 탐지하는 걸 추가해서 콜라이더를 탐지하면 다시 타겟으로 변경  