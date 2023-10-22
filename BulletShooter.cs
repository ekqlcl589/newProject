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


    public void Shot()
    {
        // BulletShooter 의 목적은 원본으로 만들어 둔 Bullet의 Prefab을 통해 사본을 생성하는 역할을 하기 때문에 
        // 다른 조건 없이 shot 함수를 통해 다른 조건 없이 사본을 생성하게 설계 
        Bullet bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
    }

}
