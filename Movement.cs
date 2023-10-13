using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private GameObject target;

    private Health temporaryObject;

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
        if(target == null)
        {
            Health damageableTarget = other.GetComponent<Health>(); // 적이 진짜 여러 개라면 들어온 순서대로 저장해야 하는데 그럼 배열로 받아서 저장했다가 
            // 첫 번째 배열 부터 순서대로 exit 에서 불러와야 한다.

            if(damageableTarget != null )
                target = damageableTarget.gameObject;
        }
        else if( target != null && temporaryObject == null)
        {
            // 타겟은 이미 정해져 있고 임시 객체만 비어 있다면
            temporaryObject = other.GetComponent<Health>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 그러다가 target 이 죽어서 null 이 되고 임시로 저장된 temp 객체가 null 이 아니라면?
        if (target == null && temporaryObject != null)
        {
            // 새로운 타겟은 temp 가 되고 
            target = temporaryObject.gameObject;

            // 임시 객체였던 temp 는 null 로 만들어 준다
            temporaryObject = null;
        }
    }
    IEnumerator Set_RandomMove()
    { // 수정 해야 함
        if(target == null) 
        {
            Vector3 randomDirection = Random.insideUnitSphere * Constant.INSIDEUNITSPHERE;

            randomDirection.y = Constant.ZERO;
            
            float randomForce = Random.Range(Constant.MOVE_SPEED, Constant.FIND_TARGET);

            rigidBody.AddForce(randomForce * randomDirection);

            yield return new WaitForSeconds(Constant.MOVE_TIME);
        }
    }
}
