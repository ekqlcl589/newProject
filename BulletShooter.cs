using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{

    // 공 생성 초기 위치
    public Transform bulletPoint;
    // 생성할 공 원본 프리팹
    public Bullet bulletPrefab;

    private float nextShootTime = Constant.ZERO;

    private List<Bullet> bulletList = new List<Bullet>();

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        shot();
    }

    private void shot()
    {
        if (bulletList.Count == Constant.ZERO && Time.time > nextShootTime) // 기호 수정 0보다 작아질 수 없는데 왜 <=를 썼냐
            CreateBullet();
    }

    private void CreateBullet()
    {
        Bullet Bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);

        bulletList.Add(Bullet);

        nextShootTime = Time.time + Constant.ATTACK_COLLTIME;

        float distanceToPoint = Vector3.Distance(bulletPoint.position, Bullet.transform.position);

        Bullet.onDelete += () => bulletList.Remove(Bullet);
    }
}

// 슈터 자체는 공을 생성하는 역할 특정 시간 마다
// 공은 생성되면 적을 향해(앞으로) 나가서 충돌하는 역할
// 체력은 최대, 현재 체력을 관리하고 죽으면 오브젝트가 삭제 되도록 하는 역할 
// 공격은 공에 붙어서 충돌 시 체력을 깎는 역할? 공이 공격 그 자체

// 슈터, 체력, 움직임은 큐브에 붙어서 독립적으로 행동
// 어택은 공에 붙어서 공격 성공 여부 체크 
