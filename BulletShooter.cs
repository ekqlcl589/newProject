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


    public void Shot()
    {
        // BulletShooter �� ������ �������� ����� �� Bullet�� Prefab�� ���� �纻�� �����ϴ� ������ �ϱ� ������ 
        // �ٸ� ���� ���� shot �Լ��� ���� �ٸ� ���� ���� �纻�� �����ϰ� ���� 
        Bullet bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
    }

}
