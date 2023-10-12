using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{

    // �� ���� �ʱ� ��ġ
    public Transform bulletPoint;
    // ������ �� ���� ������
    public Bullet bulletPrefab;

    private float nextShootTime = Constant.ZERO;

    private List<Bullet> bulletList = new List<Bullet>();

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        shot();
    }

    private void shot()
    {
        if (bulletList.Count == Constant.ZERO && Time.time > nextShootTime) // ��ȣ ���� 0���� �۾��� �� ���µ� �� <=�� ���
            CreateBullet();
    }

    private void CreateBullet()
    {
        Bullet Bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);

        bulletList.Add(Bullet);

        nextShootTime = Time.time + Constant.ATTACK_COLLTIME;

        float distanceToPoint = Vector3.Distance(bulletPoint.position, Bullet.transform.position);

        Bullet.onDelete += () => bulletList.Remove(Bullet);
    }
}

// ���� ��ü�� ���� �����ϴ� ���� Ư�� �ð� ����
// ���� �����Ǹ� ���� ����(������) ������ �浹�ϴ� ����
// ü���� �ִ�, ���� ü���� �����ϰ� ������ ������Ʈ�� ���� �ǵ��� �ϴ� ���� 
// ������ ���� �پ �浹 �� ü���� ��� ����? ���� ���� �� ��ü

// ����, ü��, �������� ť�꿡 �پ ���������� �ൿ
// ������ ���� �پ ���� ���� ���� üũ 
