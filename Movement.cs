using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    // Movement 컴포넌트의 기능은 Movement를 소유한 객체에서 target이 있다 없다를 판별해 있다면 MoveToTarget,
    // 없다면 RandomMove 함수를 호출하는 것
    public void MoveToTarget(GameObject target)
    {
        // target과 나(Movement를 보유한 객체)가 서로를 향해 이동하기 위해 방향벡터를 구하고 
        // 구해진 방향벡터를 기반으로 회전하며 특정 거리 보다 크다면 이동 시키는 목적의 함수 

        // target 의 위치와 Movement 컴포넌트를 가지고 있는 객체의 위치를 빼서 방향 벡터를 구해줌
        Vector3 direction = target.transform.position - transform.position;

        // 바닥에 붙어 있지만 y 값이 +,-로 튀면 안 되기 때문에 벡터의 y 값은 Constant.ZERO_POINT(0) 으로 고정
        direction.y = Constant.ZERO_POINT;

        // 위치 벡터를 정규화 한 값을 쿼터니언 회전 각도를 구하는 함수로 구해서 
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);
        // 트랜스폼 컴포넌트의 로테이션 값을 타겟을 향해 회전
        transform.rotation = rotation;

        //Movement 컴포넌트를 소유한 객체의 위치 값과 타겟의 위치 값을 빼서 타겟과의 거리 계산
        float distanceToPosition = Vector3.Distance(transform.position, target.transform.position);

        // 타겟과의 거리가 Constant.DISTANCE 보다 크다면
        if (distanceToPosition > Constant.DISTANCE)
        {
            // 방향벡터의 정규화(크기만을 구한) 와 Constant.MOVE_SPEED(1f) 마지막 프레임에서 현재 프레임까지의 시간값을 곱해서 움직일 방향을 결정할 move 변수에 대입
            Vector3 move = direction.normalized * Constant.MOVE_SPEED * Time.deltaTime;
            // move 변수의 값을 Movement 를 소유한 객체의 위치값에 더한 후 그 결과를 객체의 위치값에 대입 
            transform.position += move;
        }
    }
    
    public void RandomMove()
    {
        // target이 존재하지 않는다면, 랜덤한 방향으로 움직이는 목적의 함수 

        // 랜덤한 움직임을 위해 구 안의 임의의 지점을 반환하는 Random.insideUnitSphere 로 좌표를 구하고
        Vector3 randomDirection = Random.insideUnitSphere * Constant.INSIDEUNITSPHERE;
        // y 값은 Constant.ZERO_POINT(0) 으로 고정
        randomDirection.y = Constant.ZERO_POINT;

        // 호출될 때마다 랜덤한 움직임의 힘(Force)을 줘서 일정한 움직임이 아닌 더 랜덤한 움직임을 위해
        // Random.Range 함수를 통해 랜덤한 힘을 구해줌 
        float randomForce = Random.Range(Constant.MOVE_SPEED, Constant.FIND_TARGET);

        // 랜덤 좌표와 랜덤 힘, 시간값을 통해 랜덤한 움직임 구현
        Vector3 move = randomDirection.normalized * randomForce * Time.deltaTime;

        // move 변수의 값을 Movement 를 소유한 객체의 위치값에 더한 후 그 결과를 객체의 위치값에 대입
        transform.position += move;
        
    }
}
