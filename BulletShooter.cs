using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    // 공 생성 초기 위치
    public Transform bulletPoint;
    // 생성할 공 원본 프리팹
    public Bullet bulletPrefab;

    private bool fire = true;

    public void CreateBulletStart(GameObject target)
    {
        StartCoroutine(CreateBullet(target));
    }
    private IEnumerator CreateBullet(GameObject target) // 이건 타겟이 있을 때만 발사 굳이 한발씩만 쏠 필요는 없다 
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
