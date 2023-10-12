using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    // Start is called before the first frame update

    private Bullet bullet;

    private float nextShootTime;

    void Start()
    {
        bullet = FindObjectOfType<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShootTime)
            shot();
    }

    void shot()
    {
        if (bullet != null)
            bullet.TakeAttack();

        nextShootTime = Time.time + Constant.ATTACK_COLLTIME;
    }
}
