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

    private float nextShootTime;

    private int bulletCount;

    private void Start()
    {
        nextShootTime = Time.time + Constant.ATTACK_COLLTIME;
        StartCoroutine(CreateBullet());      
    }

    private IEnumerator CreateBullet()
    {
        while (true) 
        {
            if (bulletCount == Constant.ZERO_COUNT && Time.time >= nextShootTime)
            {
                Bullet bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);

                bulletCount++;

                // Time.time 을 이용해서 실제 경과 시간을 체크
                nextShootTime = Time.time + Constant.ATTACK_COLLTIME;

                bullet.onDelete += () => bulletCount--;

                yield return new WaitForSeconds(Constant.BULLET_CREATION_DELAY);

            }
            // 프레임 대기
            yield return null;
        }
    }
}
