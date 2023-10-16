using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    // �� ���� �ʱ� ��ġ
    public Transform bulletPoint;
    // ������ �� ���� ������
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

                // Time.time �� �̿��ؼ� ���� ��� �ð��� üũ
                nextShootTime = Time.time + Constant.ATTACK_COLLTIME;

                bullet.onDelete += () => bulletCount--;

                yield return new WaitForSeconds(Constant.BULLET_CREATION_DELAY);

            }
            // ������ ���
            yield return null;
        }
    }
}
