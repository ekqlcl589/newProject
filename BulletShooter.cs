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

    private bool fire = true;

    public void CreateBulletStart(GameObject target)
    {
        StartCoroutine(CreateBullet(target));
    }
    private IEnumerator CreateBullet(GameObject target) // �̰� Ÿ���� ���� ���� �߻� ���� �ѹ߾��� �� �ʿ�� ���� 
    {
        if (target != null && fire)
        {
            Bullet bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);

            fire = false;
            yield return new WaitForSeconds(Constant.ATTACK_COLLTIME);
            fire = true;

        }
    }
}
