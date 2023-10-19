using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bullet 객체에 있는 RigidBody 컴포넌트 객체
    private Rigidbody rigidBody;

    // Bullet 객체의 초기 위치값을 저장한 변수
    private Vector3 startPosition;

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
        // 초기 위치값을 MonoBehaviour 클래스가 초기화 될 때의 위치값을 대입
        startPosition = transform.position;
        // bulletDamage 변수에 Constant.DAMAGE(10f) 값 대입
        bulletDamage = Constant.DAMAGE;
    }

    private void Start()
    {
        // 거리가 멀어지면 삭제시키는 코루틴 시작
        StartCoroutine(DeleteByDistance());
    }
    
    private void FixedUpdate()
    {
        // rigidBody의 AddForce 로 힘을 가해주는 기능을 하는 함수를 호출
        BulletMove();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌 시 오브젝트를 삭제시키는 함수 실행
        Delete();
    }

    private void Delete()
    {
        // 코루틴 종료
        StopCoroutine(DeleteByDistance());
        // Bullet 객체 삭제 
        Destroy(gameObject);
    }

    IEnumerator DeleteByDistance()
    {
        // rigidBody 컴포넌트가 null 이 아니라면
        while(rigidBody != null) 
        {
            // startPosition 값에 rigidBody.position의 위치 값을 빼고 제곱한 값을 distanceToStartPosition 값에 대입
            float distanceToStartPosition = Vector3.Distance(startPosition, rigidBody.position);
            // 만약 distanceToStartPosition 값이 Constant.BULLET_DELETE_DISTANCE(15f) 보다 크다면
            if (distanceToStartPosition > Constant.BULLET_DELETE_DISTANCE)
            {
                // 오브젝트를 삭제 시키는 함수 실행
                Delete();
                // 코루틴 멈춤
                yield break;
            }
            // Constant.BULLET_DELETE_TIME(3f) 만큼 멈춤
            yield return new WaitForSeconds(Constant.BULLET_DELETE_TIME);
        }
    }

    private void BulletMove()
    {
        // rigidBody 컴포넌트가 null 이 아니라면
        if (rigidBody != null)
        {
            // Bullet 컴포넌트의 앞쪽 방향을 향해 Constant.BULLET_POWER(1f) 만큼 곱해서 힘을 가해줌
            rigidBody.AddForce(transform.forward * Constant.BULLET_POWER);
        }

    }
}
