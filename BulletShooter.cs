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

    // �Ѿ� ���� �ֱ⸦ �����ϱ� ���� ���� 
    private float nextShootTime;

    // ������ �Ѿ��� ������ �����ϱ� ���� ���� 
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
            // ������ �Ѿ��� ���� �� 
            if (bulletCount == Constant.ZERO_COUNT)
            {
                Bullet bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
                
                bulletCount++;

                nextShootTime = Constant.ATTACK_COLLTIME;

                // �Ѿ��� �浹 �ϰų� �Ÿ��� �־����� �����Ǹ� ī��Ʈ �ٿ�
                bullet.onDelete += () => bulletCount--;

                yield return new WaitForSeconds(nextShootTime);
            }
            // ���� �ʰ��� �ڷ�ƾ ����
            if(bulletCount >= Constant.BULLET_DELETE_COUNT) 
            {
                yield break;
            }
            // ������ ���
            yield return null;
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(CreateBullet());
    }
}
