using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private GameObject target;

    private List<Health> potentialTargets = new List<Health>();

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
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
    // 트리거에 접촉한 순간 정보를 저장해서 가지고 있는다 
    private void OnTriggerEnter(Collider other)
    {
        //충돌 했을 때 처음 타겟으로 잡고 
        Health damageableTarget = other.GetComponent<Health>();

        if (damageableTarget != null)
        {
            potentialTargets.Add(damageableTarget);

            // 만약 현재 타겟이 null 이라면 첫 번째 오브젝트를 타겟으로 지정
            if (target == null)
            {
                target = damageableTarget.gameObject;
                //target = potentialTargets[0].gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Health damageableTarget = other.GetComponent<Health>();

        if (damageableTarget != null)
        {
            potentialTargets.Remove(damageableTarget);

            // 만약 현재 타겟이 이 오브젝트인 경우
            if (target == damageableTarget.gameObject)
                {
                // 다른 오브젝트가 트리거 안에 있으면 그 중 하나를 새로운 타겟으로 지정
                if (potentialTargets.Count > Constant.COUNT_ZERO)
                {
                    //for(int i = 0; i <potentialTargets.Count; i++)
                        target = potentialTargets[0].gameObject;
                }
                else
                {
                    // 다른 오브젝트가 없으면 타겟을 null 로 설정
                    target = null;
                }
            }
        }
    }
    IEnumerator Set_RandomMove()
    {
        //while 문으로 바꿔서 target == null 이면 코루틴 수행 != 이면 종료
        while(target == null) 
        {
            Vector3 randomDirection = Random.insideUnitSphere * Constant.INSIDEUNITSPHERE;

            randomDirection.y = Constant.ZERO;
            
            float randomForce = Random.Range(Constant.MOVE_SPEED, Constant.FIND_TARGET);

            rigidBody.AddForce(randomForce * randomDirection);

            yield return new WaitForSeconds(Constant.MOVE_TIME);
        }
    }
}
