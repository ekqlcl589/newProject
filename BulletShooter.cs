using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
        // �������� ���ٸ� BulletPrefab�� ã�Ƽ� �ٽ� ����
        GameObject newBulletPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefab/BulletPrefab.prefab", typeof(GameObject));

        return newBulletPrefab.GetComponent<Bullet>();
    }
}
