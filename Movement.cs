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
        //rigidbody = new Rigidbody(); // ������Ʈ�� ������ �� �� ������Ʈ�� ������ �۵��� �� �Ǵϱ� ������ �ٵ� �����ؼ� ����ϰų� (c++), ��ӹ޾Ƽ� ����Ѵ� 

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

            rigidbody.MovePosition(rigidbody.position + vLook.normalized * -1f); // �ӽð� ������ ���� ����ֱ� 
        }

    }

    public void check()
    {


    }
    //public void StopMove()
    //{
    //    if(��� ť�갡 ���ٸ�)
    //            return;

    //    if(���� ť���� �Ÿ��� ���� �Ÿ��� <= distance)
    //    {
    //        navMeshAgent.speed = 0f;
    //    }
        
    //}

}
