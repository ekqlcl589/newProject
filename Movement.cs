using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public GameObject target;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    private void Update()
    {
        Move();

        StartCoroutine(Set_RandomMove());

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
    }

    private void OnTriggerStay(Collider other) // 같은 코드를 두 번 쓸 필요가 있을까 그냥 스테이만 가지고 있어도 되지 않을까 
    {
        if (target == null)
        {
            Health damageableTarget = other.GetComponent<Health>();

            if (damageableTarget != null)
                target = damageableTarget.gameObject;
        }


    }

    IEnumerator Set_RandomMove()
    {
        if(target == null)
        {
            Vector3 randomDisrtection = Random.insideUnitSphere * 5f;
            float randomForce = Random.Range(Constant.MOVE_SPEED, Constant.FIND_TARGET);

            Vector3 randomMove = new Vector3(randomForce, Constant.ZERO_POINT, randomForce); // 랜덤 무브 여기도 좀 다르게 해야하지 않을까 업데이트에서 계속 도니까 덜덜 떨리기만 하는데 

            rb.AddForce(randomForce * randomDisrtection);

            yield return new WaitForSeconds(5f);

        }
    }
}
// 만약 큐브가 3개라면 다른 타겟 잡는것도 해야함
// 그렇다면 큐브 중심에서 다른 콜라이더를 탐지하는 걸 추가해서 콜라이더를 탐지하면 다시 타겟으로 변경  