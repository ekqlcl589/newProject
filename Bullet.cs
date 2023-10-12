using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Attack attack;

    private void Start()
    {
        attack = FindObjectOfType<Attack>();

    }

    private void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(attack != null)
            attack.OnTakeAttack();

        Debug.Log("ªË¡¶");
       Destroy(gameObject);

    }

}
