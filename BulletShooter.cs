using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    // Start is called before the first frame update

    private Bullet bullet;

    public Transform bulletPoint;
    public Bullet bulletPrefab;
    Vector3 move;

    private float nextShootTime;

    void Start()
    {
        bullet = FindObjectOfType<Bullet>();

         move = new Vector3 (0,0,1);

    }

    // Update is called once per frame
    void Update()
    {
        //if (Time.time > nextShootTime)
            shot();
    }

    void shot()
    {
        if(bulletPrefab != null)
            bulletPrefab.transform.position += move * Time.deltaTime * Constant.MOVE_SPEED;

        nextShootTime = Time.time + Constant.ATTACK_COLLTIME;
    }
}

