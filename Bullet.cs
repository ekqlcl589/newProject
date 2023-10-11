using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamageable
{
    public Transform bulletPoint;
    public GameObject bulletPrefab;

    public void OnDamage(float damage)
    {
        //throw new System.NotImplementedException();
    }

    public void TakeAttack()
    {
        if (bulletPoint != null && bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddForce(bulletPoint.forward * Constant.BULLET_POWER);//, ForceMode.VelocityChange); // ������ٵ� ���� ������ �����ϰ� ���������� �ӵ��� ��ȭ�� �ִ� ��� -> ���������� ������ �ӵ��� ��ȭ�� �̸�Ŵ
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�ϸ� ���� , �Ҹ��� �����Ǹ� cube �� ���� ������ -> ������Ʈȭ x
        Debug.Log("����");

        Destroy(gameObject);
    }
}
