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

    private void Start()
    {
    }
    // Update is called once per frame
    private void Update()
    {
        StartCoroutine(s());
        //CreateBullet();
    }

    private void CreateBullet()
    {
        if (!bulletList.Any() && Time.time > nextShootTime)
        {
            Bullet Bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);

            bulletList.Add(Bullet);

            nextShootTime = Time.deltaTime + Constant.ATTACK_COLLTIME;

            Bullet.onDelete += () => bulletList.Remove(Bullet);
        }
    }

    private IEnumerator s()
    {
        if (!bulletList.Any())// && Time.time > nextShootTime)
        {
            Bullet Bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);

            bulletList.Add(Bullet);

            nextShootTime = Time.deltaTime + Constant.ATTACK_COLLTIME;

            Bullet.onDelete += () => bulletList.Remove(Bullet);
            
            yield return new WaitForSeconds(nextShootTime);
        }
    }
}
