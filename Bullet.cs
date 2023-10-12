using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamageable
{
   // public Transform bulletPoint;
    //public GameObject bulletPrefab;

    private Attack attack;

    private void Start()
    {
        attack = FindObjectOfType<Attack>();
    }
    public void OnDamage(float damage)
    {
        //throw new System.NotImplementedException();
    }

    //public void TakeAttack()
    //{
    //    if (bulletPoint != null && bulletPrefab != null)
    //    {
    //        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
    //
    //        Rigidbody rb = bullet.GetComponent<Rigidbody>();
    //
    //        if (rb != null)
    //            rb.AddForce(bulletPoint.forward * Constant.BULLET_POWER);//, ForceMode.VelocityChange); // 리지드바디가 가진 질량을 무시하고 직접적으로 속도의 변화를 주는 모드 -> 순간적으로 지정한 속도로 변화를 이르킴
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        //Attack attack = GetComponent<Attack>();
        //attack.TakeAttack();
        attack.onTakeDamageable += Test;

        Debug.Log("삭제");

       // Destroy(gameObject);
    }

    private void Test()
    {
        Debug.Log("test");
    }
}
