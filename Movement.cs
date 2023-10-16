using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private GameObject target;

    private List<Health> potentialTargets = new List<Health>();

    Health damageableTarget;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        StartCoroutine(Set_RandomMove());
    }

    // Update is called once per frame
    private void Update()
    {
        MoveToTarget();

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
        while(target == null) 
        {
            Vector3 randomDirection = Random.insideUnitSphere * Constant.INSIDEUNITSPHERE;

            randomDirection.y = Constant.ZERO;
            
            float randomForce = Random.Range(Constant.MOVE_SPEED, Constant.FIND_TARGET);

            rigidBody.AddForce(randomForce * randomDirection);

            yield return new WaitForSeconds(Constant.MOVE_TIME);
        }
    }

     //트리거에 접촉한 순간 정보를 저장해서 가지고 있는다 
    private void OnTriggerEnter(Collider other)
    {
        Health damageableTarget = other.GetComponent<Health>();

        if (damageableTarget != null)
        {
            potentialTargets.Add(damageableTarget);

            if (target == null)
            {
                target = damageableTarget.gameObject;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Health damageableTarget = other.GetComponent<Health>();

        if (damageableTarget != null)
        {
            potentialTargets.Remove(damageableTarget);

            if (target == damageableTarget.gameObject)
            {
                foreach (Health potentialTarget in potentialTargets)
                {
                    if (potentialTarget.gameObject.activeSelf)
                    {
                        target = potentialTarget.gameObject;
                        return;
                    }
                }

                target = null;
            }
        }
    }
    private void OnDestroy()
    {
        if (target != null && target.gameObject == this.gameObject)
        {
            // 삭제된 타겟이 현재 타겟이라면
            if (potentialTargets.Count > 0)
            {
                // 다음 순서의 오브젝트를 타겟으로 설정
                target = potentialTargets[0].gameObject;
            }
            else
            {
                // 다른 오브젝트가 없는 경우, 타겟을 null 로 설정
                target = null;
            }
        }
    }
}
