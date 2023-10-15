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

    private float nextShootTime = Constant.ZERO;

    private List<Bullet> bulletList = new List<Bullet>();

    // Update is called once per frame
    private void Update()
    {
        StartCoroutine(CreateBullet());
    }

    private IEnumerator CreateBullet()
    {
        if (!bulletList.Any())
        {
            Bullet Bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);

            bulletList.Add(Bullet);

            nextShootTime = Time.deltaTime + Constant.ATTACK_COLLTIME;

            Bullet.onDelete += () => bulletList.Remove(Bullet);
            
            yield return new WaitForSeconds(nextShootTime);
        }
    }
}
