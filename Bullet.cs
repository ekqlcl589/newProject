using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform bulletPoint;
    //public GameObject bulletPrefab;

    private Attack attack;
    Vector3 move;

    public System.Action shoot;
    private void Start()
    {
        attack = FindObjectOfType<Attack>();

        shoot += TakeAttack;
         move = new Vector3 (0,0,1);
    }

    private void Update()
    {
        //TakeAttack();
        
    }
    public void TakeAttack()
    {
        //if (bulletPoint != null)// && bulletPrefab != null)
        //{
        //    //GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);

        //    Rigidbody rb = GetComponent<Rigidbody>();

        //    if (rb != null)
        //        rb.AddForce(bulletPoint.forward * Constant.BULLET_POWER);//, ForceMode.VelocityChange); // ������ٵ� ���� ������ �����ϰ� ���������� �ӵ��� ��ȭ�� �ִ� ��� -> ���������� ������ �ӵ��� ��ȭ�� �̸�Ŵ
        //}
        transform.position += move * Time.deltaTime * Constant.MOVE_SPEED;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(attack != null)
            attack.OnTakeAttack.Invoke(); // ���⼭ ��ϵ� �̺�Ʈ�� ������� ����

        Debug.Log("����");
       Destroy(gameObject);

    }

}
