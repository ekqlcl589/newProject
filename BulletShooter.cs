using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    // 공 생성 초기 위치
    public Transform bulletPoint;
    // 생성할 공 원본 프리팹
    public Bullet bulletPrefab;

    // 총알 생성 주기를 관리하기 위한 변수 
    private float nextShootTime;

    // 생성된 총알의 갯수를 관리하기 위한 변수 
    private int bulletCount;

    private void Awake()
    {
        nextShootTime = Constant.ATTACK_COLLTIME;
    }

    public void BulletCreate()
    {
        StartCoroutine(CreateBullet());
    }
    private IEnumerator CreateBullet()
    {
        while (true) 
        {
            // 생성된 총알이 없을 때 
            if (bulletCount == Constant.ZERO_COUNT)
            {
                Bullet bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
                
                bulletCount++;

                nextShootTime = Constant.ATTACK_COLLTIME;

                // 총알이 충돌 하거나 거리가 멀어져서 삭제되면 카운트 다운
                bullet.onDelete += () => bulletCount--;

                yield return new WaitForSeconds(nextShootTime);
            }
            // 갯수 초과시 코루틴 종료
            if(bulletCount >= Constant.BULLET_DELETE_COUNT) 
            {
                yield break;
            }
            // 프레임 대기
            yield return null;
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(CreateBullet());
    }
}
