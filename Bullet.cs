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
    //            rb.AddForce(bulletPoint.forward * Constant.BULLET_POWER);//, ForceMode.VelocityChange); // ������ٵ� ���� ������ �����ϰ� ���������� �ӵ��� ��ȭ�� �ִ� ��� -> ���������� ������ �ӵ��� ��ȭ�� �̸�Ŵ
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        //Attack attack = GetComponent<Attack>();
        //attack.TakeAttack();
        attack.onTakeDamageable += Test;

        Debug.Log("����");

       // Destroy(gameObject);
    }

    private void Test()
    {
        Debug.Log("test");
    }
}
