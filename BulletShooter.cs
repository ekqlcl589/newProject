using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    // Bullet �������� �ʱ� ���� ��ġ
    public Transform bulletPoint;
    // ������ Bullet �� ���� ������
    public Bullet bulletPrefab;


    public void CreateBullet1() 
    {
        // Bullet �� ��ü�� ���� �����հ� �ʱ� ��ġ��, �ʱ� ȸ������ ������ �纻 ����
        Bullet bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
    }

}
