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
            // 방향 벡터
            Vector3 direction = target.position - transform.position;

            direction.y = Constant.ZERO_point;

            // 타겟을 향해 회전
            Quaternion rotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = rotation;

            //타겟과의 거리 계산
            float distanceToPosition = Vector3.Distance(transform.position, target.position);

            // 타겟과의 거리가 distance 보다 크다면 이동 
            if (distanceToPosition > Constant.DISTANCE)
            {
                Vector3 move = direction.normalized * Constant.MOVE_SPEED * Time.deltaTime;
                transform.position += move;
            }
            else
            {
                //공격
            }
        }
    }

    //다른 타겟으로 변경할 때 사용할 함수 
    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null && gameObject == null)
            return;

        target = newTarget;
    }

}
