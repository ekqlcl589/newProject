using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bullet 객체에 있는 RigidBody 컴포넌트 객체
    private Rigidbody rigidBody;

    // Bullet 객체의 초기 위치값을 저장한 변수
    private Vector3 startPosition;

    // 코루틴이 종료 되어야 할 때 동작하지 않는 코루틴 까지 스탑코루틴을 걸어서 "불필요한 동작을 줄이기 위해" Coroutine 을 반환하는 객체들을 만듦
    private Coroutine DeleteByDistanceCoroutine;

    // Bullet 의 데미지를 저장할 변수
    private float bulletDamage;

    // Bullet 의 데미지를 저장한 변수의 값을 가져오는 프로퍼티
    public float BulletDamage
    {
        get { return bulletDamage; }
    }
    private void Awake()
    {
        // rigidBody 객체에 RigidBody 값 대입
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // 거리 비례 삭제 시 계산에 필요한 초기 위치값을 MonoBehaviour 클래스가 초기화 될 때의 위치값을 대입
        startPosition = transform.position;
        // bulletDamage 변수에 Constant.DAMAGE(10f) 값 대입
        bulletDamage = Constant.DAMAGE;
        // 코루틴 메서드가 동작(true)중 인지 아닌지(false) 구분하기 위해 값 대입
        DeleteByDistanceCoroutine = StartCoroutine(DeleteByDistance());
    }
    
    private void FixedUpdate()
    {
        BulletMove();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // bullet 은 충돌 시 추가 충돌을 막기 위해 삭제
        Delete();
    }

    private void Delete()
    {
        // DeleteByDistanceCoroutine이 실행중이라면 코루틴 종료
        if (DeleteByDistanceCoroutine != null)
            StopCoroutine(DeleteByDistance());

        Destroy(gameObject);
    }

    // 첫 발사된 위치와 날아가는 Bullet 의 거리를 비교하여 거리가 멀어진다면 Bullet 을 삭제시키고, 코루틴을 멈추기 위한 코드   
    IEnumerator DeleteByDistance()
    {
        // rigidBody 컴포넌트가 null 이면 움직일 수 없으므로 반복의 조건으로 사용
        while(rigidBody != null) 
        {
            // 날아간 거리를 구하기 위해 Vector3.Distance 함수를 통해 값을 계산하고 파괴 조건으로 사용 
            float distanceToStartPosition = Vector3.Distance(startPosition, rigidBody.position);
            // 만약 distanceToStartPosition 값이 지정값(Constant.BULLET_DELETE_DISTANCE(15f)) 보다 크다면 삭제 시키는 조건을 줘서 거리가 멀다면 삭제
            if (distanceToStartPosition > Constant.BULLET_DELETE_DISTANCE)
            {
                Delete();
            }
            // 계속 거리를 비교하는 것이 아니라 코루틴으로 Constant.BULLET_DELETE_TIME(3f) 만큼 멈춘 후 다시 계산
            yield return new WaitForSeconds(Constant.BULLET_DELETE_TIME);
        }
    }

    private void BulletMove()
    {
        // rigidBody 컴포넌트 null 체크를 통해 함수 접근 차단
        if (rigidBody == null)
            return;

        // rigidBody가 null 이 아니라면 bullet 은 앞으로 날아가기만 하면 되기 때문에 AddForce로 힘을 가해줌
        rigidBody.AddForce(transform.forward * Constant.BULLET_POWER);
        

    }
}
