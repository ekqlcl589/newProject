using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    private GameObject target;
    
    private Queue<Health> potentialTargets = new Queue<Health>();

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MoveToTarget();

        StartCoroutine(Set_RandomMove());
    }

    private void MoveToTarget()
    {
        if (target != null)
        {
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
        }
    }
    IEnumerator Set_RandomMove()
    {
        if (target == null)
        {
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
        }
    }

     //트리거에 접촉한 순간 정보를 저장해서 가지고 있는다 
    private void OnTriggerEnter(Collider other)
    {
        Health damageableTarget = other.GetComponent<Health>();

        if (damageableTarget != null)
        {
            potentialTargets.Enqueue(damageableTarget);
            damageableTarget.onDestroy += SetChangeTarget;

            if (target == null)
            {
                target = damageableTarget.gameObject;
            }
        }
    }

    private void SetChangeTarget()
    {
        foreach (Health potentialTarget in potentialTargets)
        {
            potentialTargets.Dequeue();

            if (potentialTargets.Count > Constant.ZERO_COUNT)
            {
                target = potentialTargets.Peek().gameObject;
            }
            else
            {
                target = null;
                potentialTargets.Clear();
            }

            return;
        }
    }
}
