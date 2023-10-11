using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveMent : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    private const float moveSpeed = 1f;
    private const float distance = 1f;

    private const float zeroPoint = 0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            Move();
    }

    public void Move()
    {
        // 방향 벡터
        Vector3 direction = target.position - transform.position;

        direction.y = zeroPoint;

        // 타겟을 향해 회전
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);
        transform.rotation = rotation;

        //타겟과의 거리 계산
        float distanceToPosition = Vector3.Distance(transform.position, target.position);

        // 타겟과의 거리가 distance 보다 크다면 이동 
        if (distanceToPosition > distance)
        {
            Vector3 move = direction.normalized * moveSpeed * Time.deltaTime;
            transform.position += move;
        }
    }

    //다른 타겟으로 변경할 때 사용할 함수 
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

}
