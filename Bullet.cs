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
        //        rb.AddForce(bulletPoint.forward * Constant.BULLET_POWER);//, ForceMode.VelocityChange); // 리지드바디가 가진 질량을 무시하고 직접적으로 속도의 변화를 주는 모드 -> 순간적으로 지정한 속도로 변화를 이르킴
        //}
        transform.position += move * Time.deltaTime * Constant.MOVE_SPEED;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(attack != null)
            attack.OnTakeAttack.Invoke(); // 여기서 등록된 이벤트들 순서대로 실행

        Debug.Log("삭제");
       Destroy(gameObject);

    }

}
