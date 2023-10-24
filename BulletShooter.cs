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
        // BulletShooter �� ������ �������� ����� �� Bullet �� Prefab �� ���� �纻�� �����ϴ� ������ �ϱ� ������ 
        // �ٸ� ���� ���� shot �Լ��� ���� �ٸ� ���� ���� �纻�� �����ϰ� ���� 

        if (bulletPoint != null)
        {
            if (bulletPrefab == null)
            {
                // bulletPrefab�� null �� ���, ���ο� �������� �����ؼ� ���� ó��
                bulletPrefab = CreateNewBulletPrefab();
            }

            Bullet bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        }
    }

    private Bullet CreateNewBulletPrefab()
    {
        GameObject newBulletPrefab = new GameObject("BulletPrefab");
        // ���ϴ� ���� ��Ҹ� newBulletPrefab�� �߰�

        return newBulletPrefab.GetComponent<Bullet>();
    }
}
