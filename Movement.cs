using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveMent : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rigidbody;

    public GameObject enemy;
    private const float moveSpeed = 1f;
    private const float distance = 1f;

    Vector3 vector = Vector3.zero;
    Vector3 vLook;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        //rigidbody = new Rigidbody(); // 컴포넌트로 받으면 또 그 컴포넌트가 없으면 작동이 안 되니까 리지드 바디를 생성해서 사용하거나 (c++), 상속받아서 사용한다 

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        //if (navMeshAgent == null)
        //    return;

        //navMeshAgent.speed = moveSpeed;
        if(Input.GetKeyDown(KeyCode.W)) 
        {
            vector = enemy.transform.position;

            Vector3 moveDist = moveSpeed * transform.forward * moveSpeed * Time.deltaTime;

            vLook = moveDist - vector;

            Quaternion rot = Quaternion.LookRotation(vLook.normalized);

            transform.rotation = rot;

            rigidbody.MovePosition(rigidbody.position + vLook.normalized * -1f); // 임시값 변수로 만들어서 집어넣기 
        }

    }

    public void check()
    {


    }
    //public void StopMove()
    //{
    //    if(상대 큐브가 없다면)
    //            return;

    //    if(상대방 큐브의 거리와 나의 거리가 <= distance)
    //    {
    //        navMeshAgent.speed = 0f;
    //    }
        
    //}

}
