using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    // Bullet 프리팹의 초기 생성 위치
    public Transform bulletPoint;
    // 생성할 Bullet 의 원본 프리팹
    public Bullet bulletPrefab;


    public void CreateBullet1() 
    {
        // Bullet 의 객체에 원본 프리팹과 초기 위치값, 초기 회전값을 가지는 사본 생성
        Bullet bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
    }

}
