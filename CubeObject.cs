using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CubeObject : MonoBehaviour
{
    // 움직임, 공격 등을 지정할 오브젝트로 전역함수 target 을 만듦
    private GameObject target;
    // 체력을 관리하는 컴포넌트
    private Health health;
    // 움직임 기능을 관리하는 컴포넌트
    private Movement movement;
    // 총알 발사 기능을 관리하는 컴포넌트 
    private BulletShooter bulletShooter;

    // 트리거에 접촉하는 순서대로 (체력을 가진)타겟을 지정 해주기 위해 리스트로 컨테이너를 만듦
    private List<Health> potentialTargets = new List<Health>();

    // 코루틴이 종료 되어야 할 때 동작하지 않는 코루틴 까지 스탑코루틴을 걸어서 "불필요한 동작을 줄이기 위해" Coroutine 을 반환하는 객체들을 만듦
    private Coroutine CreateBulletCoroutin;
    private Coroutine TargetSettingCoroutin;

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
        // CreateBulletCoroutin으로 코루틴 메서드가 동작(true)중 인지 아닌지(false) 구분하기 위해 
        CreateBulletCoroutin = StartCoroutine(CreateBullet());
        TargetSettingCoroutin = StartCoroutine(TargetSetting());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // update 는 매 프레임 마다 호출이 되서 프레임에 따라 호출되는 정도가 다른데
        // FixedUpdate 는 고정된 프레임(0.02) 마다 호출을 해서 일정한 움직임을 가지게 할 수 있음 -> input 으로 움직이는 게 아니라서 
        MoveFixedUpdate();
    }

    // 콜리전과 접촉하는 순간 딱 한번 체력이 깎여야 하기 때문에 Enter 에서 체력 관리
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 Bullet 형인지 아닌지 판별하고, health 가 존재한다면 데미지를 주기 위해 ColligionEnter에서 구현
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        // 대입한 객체가 null 이 아니거나 체력이 null 이 아니라면
        if (bullet != null && health != null)
            // 총알의 공격력 만큼 체력 깎음
            health.SetMinusHp -= bullet.BulletDamage;

    }

    // OnTriggerEnter는 트리거 "접촉이 일어나는 순간에만 한 번만 호출"이 되기 때문에 타겟 지정은 다른 함수를 통해 지정하고 enter 에서는 리스트에만 추가한다
    // 여기서 타겟(target) 까지 지정해 버리면 트리거에 다른 잠재적 타겟들(CubeObject)가 들어오면 기존 타겟이 새로운 타겟으로 덮혀 버리기 때문
    // 리스트에 등록될 때 중복 타겟이 들어가지 않게 하기 위해 Contains 로 같은 값이 들어가 있는지 중복 체크 
    private void OnTriggerEnter(Collider other)
    {
        // 트리거와 충돌한 오브젝트를 Health 객체에 대입
        Health damageableTarget = other.GetComponent<Health>();
        // 대입한 Health 객체가 null 이 아니고 중복된 값이 아니라면 
        if (damageableTarget != null && !potentialTargets.Contains(damageableTarget))
        {
            // 잠재적 타겟들을 관리하는 컨테이너(리스트)에 저장
            potentialTargets.Add(damageableTarget);
        }
    }

    // Exit 에서는 빠져 나간 타겟을 리스트에서 제거 해주고 그 빠져 나간 타겟이 현재 타겟과 같다면 target 을 null 로 돌리는 작업
    // exit 시 리스트만 제거를 해주고 혹시나 그 값이 현재 타겟과 같다면 target 을 null 로 만들어 주면 된다
    private void OnTriggerExit(Collider other)
    {
        Health exitTarget = other.GetComponent<Health>();
        // 리스트가 존재하고 exitTarget 이 리스트 안에 존재 한다면 리스트에서 삭제
        if (potentialTargets.Count > Constant.ZERO_COUNT && exitTarget != null && potentialTargets.Contains(exitTarget))
        {
            // 리스트를 사용하면 필요한 지점의 정보만 remove 가 가능해짐
            potentialTargets.Remove(exitTarget); 

            // 빠져나간 타겟이 현재 타겟과 동일 하다면 target 을 null 로 만들어 줘야 함
            if (exitTarget.gameObject == target)
            {
                target = null;
            }
        }
    }
    // 타겟을 지정하는 행위를 행해야 하는데 Update 나 FixedUpdate 에서 계속 호출 하는 건 최적화 부분에서 매우 좋지 않기 때문에 
    // 주기적으로 호출할 수 있는 코루틴으로 함수를 설계
    // 이 함수는 타겟을 세팅 해주는 게 목적인 함수라서 이름 변경
    IEnumerator TargetSetting()
    {
        // CheckTarget 함수는 오브젝트가 존재하는 한 계속 검사를 하긴 해야해서 gameObject(객체)가 null 이 아니라면 반복시킴 -> 종료는 OnDestroy 에서 종료
        while (gameObject != null)
        {
            // 리스트에 들어간 인자를 타겟으로 잡아야 하므로 리스트에 하나라도 인자값이 들어 있다면
           if(potentialTargets.Count > Constant.ZERO_COUNT && target == null)
            {
                // target 이 null 일 때만 타겟을 리스트에 들어가 있는 첫 번째 데이터로 지정해 주고 지정된 리스트를 삭제 해주면 타겟이 죽었을 때에 대한 관리를 추가로 해주지 않아도 됨
                // 여러 개의 타겟을 동시에 설정 하는게 아니라 들어온 순서 대로 하나씩 타겟으로 지정할 것이기 때문에 리스트에 들어가 있는 첫 번째 인자를 타겟으로 설정
                target = potentialTargets[0].gameObject;
                // 리스트에 들어가 있던 첫 번째 데이터가 target 으로 지정이 되면 저장된 0 번째 배열을 리스트에서 제거해 줘서 리스트에 중복 제거
                potentialTargets.RemoveAt(0);
            }
            yield return new WaitForSeconds(Constant.WAIT_FOR_ONESECOND);
        }
    }

    // 총알 발사는 타겟이 있다면 주기적으로 계속 생성을 해줘야 해서 함수를 정지 시키고 다시 실행할 수 있는 코루틴을 사용
    IEnumerator CreateBullet()
    {
        // 발사 시키는 컴포넌트가 존재 해야만 Bullet 을 생성할 수 있으므로 while 조건을 bulletShooter != null 으로 잡음
        while (bulletShooter != null) 
        {
            // 총알은 타겟이 있으면 발사 하고 없으면 동작하지 않아도 됨 -> 쓸모없는 동작을 줄임 
            if (target != null)
            {
                // 총알 발사 컴포넌트의 Shot 함수 실행
                bulletShooter.Shot();
            }
            // Constant.BULLET_ATTACK_DELAY(2초) 만큼 정지
            yield return new WaitForSeconds(Constant.BULLET_ATTACK_DELAY);
        }
    }
    // movement 컴포넌트를 통해 구현된 움직임을 관여하는 함수들은 2가지로 target 이 있냐 없냐에 따라서 구분할 수 있기 때문에 if, else 문으로 체크
    private void MoveFixedUpdate()
    {
        // FixedUpdate 에서 호출될 때 movement 컴포넌트가 null 이라면 return 시켜서 호출 중지
        if (movement == null)
            return;

        // 타겟의 유무를 통해 랜덤한 움직임을 가질 지, target 을 향해 움직이는 행동을 할 지 결정하기 위해 if, else 로 함수 구분
        if (target != null)
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

    private void OnDestroy()
    {
        // 코루틴 함수가 실행 중인지 확인 하고, 실행중이지 않은 함수를 종료 하는 비효율 적인 행동을 안 하기 위해 null 체크
        if (CreateBulletCoroutin != null)
        // CreateBullet 코루틴 종료
            StopCoroutine(CreateBullet());
      
        if(TargetSettingCoroutin != null)
        // CheckTarget 코루틴 종료
            StopCoroutine(TargetSetting());
    }
}
