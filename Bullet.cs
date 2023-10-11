using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform bulletPoint;
    public GameObject bulletPrefab;

    public void TakeAttack()
    {
        if (bulletPoint != null && bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddForce(bulletPoint.forward * Constant.BULLET_POWER);//, ForceMode.VelocityChange); // 리지드바디가 가진 질량을 무시하고 직접적으로 속도의 변화를 주는 모드 -> 순간적으로 지정한 속도로 변화를 이르킴
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌하면 삭제 , 불릿이 삭제되면 cube 도 같이 삭제됨 -> 컴포넌트화 x
        Debug.Log("삭제");

        Destroy(gameObject);
    }
}
