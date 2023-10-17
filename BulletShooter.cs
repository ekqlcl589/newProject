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
    private float nextShootTime = Constant.ZERO;

    // ������ �Ѿ��� ������ �����ϱ� ���� ���� 
    private int bulletCount;

    private void Start()
    {
        // �ʱ� �ð��� ���� ���� �ð� + ��Ÿ�� 3�� �� ���� 
        //nextShootTime = Time.time + Constant.ATTACK_COLLTIME;
        StartCoroutine(CreateBullet());      
    }

    private IEnumerator CreateBullet()
    {
        while (true) 
        {
            // ������ �Ѿ��� ���� �� 
            if (bulletCount == Constant.ZERO_COUNT)// && Time.time >= nextShootTime)
            {
                // ù ���� �ٷ� �߻� ����  Time.deltaTime * Constant.ATTACK_COLLTIME ��ŭ ��
                yield return new WaitForSeconds(nextShootTime);
                // �Ѿ� ���� 
                Bullet bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
                // ī��Ʈ ++
                bulletCount++;

                // Time.time �� �̿��ؼ� ���� ��� �ð��� üũ

                nextShootTime = Time.deltaTime + Constant.ATTACK_COLLTIME;

                // �Ѿ��� �浹 �ϰų� �Ÿ��� �־����� �����Ǹ� ī��Ʈ �ٿ�
                bullet.onDelete += () => bulletCount--;
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
}
