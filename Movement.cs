using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    //지정된 타겟이 있으면 타겟을 향해 움직이는 기능
    public void MoveToTarget(GameObject target)
    {
        // target 의 위치와 Movement 컴포넌트를 가지고 있는 객체의 위치를 빼서 방향 벡터를 구해줌
        Vector3 direction = target.transform.position - transform.position;

        // 방향 벡터의 y 값은 Constant.ZERO_POINT(0) 으로 고정
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
        // Random 클래스의 반경 1을 가지는 구의 임의의 지점에 Constant.INSIDEUNITSPHERE(5)를 곱해서 5의 반경을 갖는 임의의 벡터를 구해줌
        Vector3 randomDirection = Random.insideUnitSphere * Constant.INSIDEUNITSPHERE;
        // y 값은 Constant.ZERO_POINT(0) 으로 고정
        randomDirection.y = Constant.ZERO_POINT;

        // randomForce float 변수에 1 에서 4 까지의 임의의 값 대입
        float randomForce = Random.Range(Constant.MOVE_SPEED, Constant.FIND_TARGET);
        // randomDirection 벡터의 정규화 값과 randomForce, 마지막 프레임에서 현재 프레임까지의 시간값을 곱해서 움직일 방향을 결정할 move 변수에 대입
        Vector3 move = randomDirection.normalized * randomForce * Time.deltaTime;
        // move 변수의 값을 Movement 를 소유한 객체의 위치값에 더한 후 그 결과를 객체의 위치값에 대입
        transform.position += move;

    }
}
