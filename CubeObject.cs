using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeObject : MonoBehaviour
{
    // 움직임, 공격 등을 지정할 오브젝트
    private GameObject target;
    // 체력을 관리하는 컴포넌트
    private Health health;
    // 잠재적 타겟들을 관리하는 컨테이너 할당
    private Queue<Health> potentialTargets = new Queue<Health>();
    // 움직임 기능을 관리하는 컴포넌트
    private Movement movement;
    // 총알 발사 기능을 관리하는 컴포넌트 
    private BulletShooter bulletShooter;

    private void Awake()
    {
        // 체력 기능 컴포넌트 가져오기
        health = GetComponent<Health>();
        // 움직임 기능 컴포넌트 가져오기
        movement = GetComponent<Movement>();
        // 총알 발사 기능 컴포넌트 가져오기
        bulletShooter = GetComponent<BulletShooter>();

    }
    // Start is called before the first frame update
    void Start()
    {
        // 총알 발사 코루틴 시작
        StartCoroutine(CreateBullet());
        // 잠재적 타겟 지정 코루틴 시작
        StartCoroutine(CheckTarget());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // 움직이는 함수 동작
        MoveFixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 총알 컴포넌트가 Bullet 클래스의 객체로 대입
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        // 대입한 객체가 null 이 아니거나 체력이 null 이 아니라면
        if (bullet != null && health != null)
            // 총알의 공격력 만큼 체력 깎음
            health.SetMinusHp -= bullet.BulletDamage;
    }

    private void OnTriggerEnter(Collider other) // 트리거에 들어 왔으면 큐 에 저장만 한다
    {
        // 트리거와 충돌한 체력 관리 기능을 Health 객체에 대입
        Health damageableTarget = other.GetComponent<Health>();
        // 대입한 Health 객체가 null 이 아니라면
        if (damageableTarget != null)
        {
            // 잠재적 타겟들을 관리하는 컨테이너(큐)에 저장
            potentialTargets.Enqueue(damageableTarget);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 트리거 영역에서 타겟이 나갈 때 처리
        Health exitTarget = other.GetComponent<Health>();

        // 트리거에서 나가는 타겟이 null 이 아니라면
        if (exitTarget != null)
        {
            // 새로운 큐 생성
            Queue<Health> newQueue = new Queue<Health>();

            // 기존 큐를 순회하면서 원하는 타겟은 건너 뜀
            while (potentialTargets.Count > Constant.ZERO_COUNT)
            {
                // 새로운 타겟에 기존 큐의 마지막 인자를 대입
                Health target = potentialTargets.Dequeue();
                // 새로운 타겟이 트리거 밖으로 나가는 타겟과 다르다면
                if (target != exitTarget)
                {
                    // 새로운 큐에 새로운 큐를 저장
                    newQueue.Enqueue(target);
                }
            }

            // 기존 큐를 새로운 큐로 대체
            potentialTargets = newQueue;

            // 만약 현재 타겟이 나가는 타겟이면 현재 타겟도 null로 설정
            if (exitTarget.gameObject == target)
            {
                target = null;
            }
        }
    }
    // 주기적으로 타겟을 체크 하면서 타겟이 null 이면 큐에서 하나씩 빼서 타겟을 지정해준다.
    IEnumerator CheckTarget()
    {
        // start 에서 무한 반복하면서
        while (true) 
        {
            // 타겟이 null 이고, 잠재적 타겟들을 관리하는 컨테이너의 저장된 인자가 0개 이상이라면
            if (target == null && potentialTargets.Count > Constant.ZERO_COUNT)
            {
                // 타겟을 컨테이너의 처음 들어간 인자의 오브젝트 정보를 대입
                target = potentialTargets.Dequeue().gameObject;
                // 만약 타겟이 null 이라면
                if(target == null)
                    // 코루틴 정지 
                    yield break;

            }
            // Constant.WAIT_FOR_ONESECOND(1초) 만큼 정지 
            yield return new WaitForSeconds(Constant.WAIT_FOR_ONESECOND);
        }
    }

    
    IEnumerator CreateBullet()
    {
        // 불러온 bulletShooter 컴포넌트가 null 이 아니라면 start 에서 무한 반복
        while (bulletShooter != null) 
        {
            // 만약 타겟이 null 이 아니라면
            if (target != null)
            {
                // 총알 발사 컴포넌트의 CreateBullet 함수 실행
                bulletShooter.CreateBullet1();
            }
            // Constant.BULLET_ATTACK_DELAY(2초) 만큼 정지
            yield return new WaitForSeconds(Constant.BULLET_ATTACK_DELAY);
        }
    }
    // FixedUpdate 에서 호출될 움직임 함수들을 target 의 유무에 따라 관리
    private void MoveFixedUpdate()
    {
        // movement 컴포넌트가 null 이 아니라면
        if (movement != null)
        {
            // 타겟이 null 이 아니라면
            if(target != null) 
            {
                // 타겟을 향해 움직이는 함수 실행(CubeObject에서 지정된 target 의 정보를 인자값으로 넘겨줌)
                movement.MoveToTarget(target);
            }
            // 타겟이 null 이라면
            else
            {
                // 랜덤한 움직임을 구현한 함수 실행
                movement.RandomMove();
            }
        }
    }

    // CubeObject 가 삭제되면 
    private void OnDestroy()
    {
        // CreateBullet 코루틴 종료
        StopCoroutine(CreateBullet());
        // CheckTarget 코루틴 종료
        StopCoroutine(CheckTarget());

    }
}
