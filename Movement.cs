using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public GameObject target;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    private void Update()
    {
        Move();

        StartCoroutine(Set_RandomMove());

    }

    private void Move()
    {
        if (target != null)
        {
            // ���� ����
            Vector3 direction = target.transform.position - transform.position;

            direction.y = Constant.ZERO_POINT;

            // Ÿ���� ���� ȸ��
            Quaternion rotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = rotation;
            //transform.Rotate(direction * Time.deltaTime * Constant.MOVE_SPEED);

            //Ÿ�ٰ��� �Ÿ� ���
            float distanceToPosition = Vector3.Distance(transform.position, target.transform.position);

            // Ÿ�ٰ��� �Ÿ��� distance ���� ũ�ٸ� �̵� 
            if (distanceToPosition > Constant.DISTANCE)
            {
                Vector3 move = direction.normalized * Constant.MOVE_SPEED * Time.deltaTime;
                transform.position += move;
            }
        }
    }

    private void OnTriggerStay(Collider other) // ���� �ڵ带 �� �� �� �ʿ䰡 ������ �׳� �����̸� ������ �־ ���� ������ 
    {
        if (target == null)
        {
            Health damageableTarget = other.GetComponent<Health>();

            if (damageableTarget != null)
                target = damageableTarget.gameObject;
        }


    }

    IEnumerator Set_RandomMove()
    {
        if(target == null)
        {
            Vector3 randomDisrtection = Random.insideUnitSphere * 5f;
            float randomForce = Random.Range(Constant.MOVE_SPEED, Constant.FIND_TARGET);

            Vector3 randomMove = new Vector3(randomForce, Constant.ZERO_POINT, randomForce); // ���� ���� ���⵵ �� �ٸ��� �ؾ����� ������ ������Ʈ���� ��� ���ϱ� ���� �����⸸ �ϴµ� 

            rb.AddForce(randomForce * randomDisrtection);

            yield return new WaitForSeconds(5f);

        }
    }
}
// ���� ť�갡 3����� �ٸ� Ÿ�� ��°͵� �ؾ���
// �׷��ٸ� ť�� �߽ɿ��� �ٸ� �ݶ��̴��� Ž���ϴ� �� �߰��ؼ� �ݶ��̴��� Ž���ϸ� �ٽ� Ÿ������ ����  