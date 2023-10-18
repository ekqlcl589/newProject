using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    //private GameObject target;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }


    public void MoveUpdate(GameObject target)
    {
        if(target == null)
            StartCoroutine(Set_RandomMove(target));
        else
            MoveToTarget(target);
    }

    private void MoveToTarget(GameObject target)
    {
        //if (target != null) 스칼렛한테 물어보기 
        //{
            // 방향 벡터
            Vector3 direction = target.transform.position - transform.position;

            direction.y = Constant.ZERO_POINT;

            // 타겟을 향해 회전
            Quaternion rotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = rotation;

            //타겟과의 거리 계산
            float distanceToPosition = Vector3.Distance(transform.position, target.transform.position);

            // 타겟과의 거리가 distance 보다 크다면 이동 
            if (distanceToPosition > Constant.DISTANCE)
            {
                Vector3 move = direction.normalized * Constant.MOVE_SPEED * Time.deltaTime;
                transform.position += move;
            }
        //}
    }
    private IEnumerator Set_RandomMove(GameObject target)
    {
        //if (target == null)
        //{
            if (rigidBody != null)
            {
                Vector3 randomDirection = Random.insideUnitSphere * Constant.INSIDEUNITSPHERE;
                randomDirection.y = Constant.ZERO_POINT;

                float randomForce = Random.Range(Constant.MOVE_SPEED, Constant.FIND_TARGET);

                rigidBody.AddForce(randomForce * randomDirection.normalized);
            }
            else
            {
                // rigidBody가 null 이면 코루틴을 종료
                yield break;
            }

            yield return new WaitForSeconds(Constant.MOVE_TIME);
        //}
    }
}
