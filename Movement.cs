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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if(transform != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0f; // 0 고정

            // 타겟을 향해 회전
            Quaternion rotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = rotation;

            float distanceToPosition = Vector3.Distance(transform.position, target.position);

            // 타겟과의 거리가 distance 보다 크다면 이동 
            if(distanceToPosition > distance)
            {
                Vector3 move = direction.normalized * moveSpeed * Time.deltaTime;
                transform.position += move;
            }
            else
            {

            }
        }
    }


}
