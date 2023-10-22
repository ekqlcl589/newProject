using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    // Movement ������Ʈ�� ����� Movement�� ������ ��ü���� target�� �ִ� ���ٸ� �Ǻ��� �ִٸ� MoveToTarget,
    // ���ٸ� RandomMove �Լ��� ȣ���ϴ� ��
    public void MoveToTarget(GameObject target)
    {
        // target�� ��(Movement�� ������ ��ü)�� ���θ� ���� �̵��ϱ� ���� ���⺤�͸� ���ϰ� 
        // ������ ���⺤�͸� ������� ȸ���ϸ� Ư�� �Ÿ� ���� ũ�ٸ� �̵� ��Ű�� ������ �Լ� 

        // target �� ��ġ�� Movement ������Ʈ�� ������ �ִ� ��ü�� ��ġ�� ���� ���� ���͸� ������
        Vector3 direction = target.transform.position - transform.position;

        // �ٴڿ� �پ� ������ y ���� +,-�� Ƣ�� �� �Ǳ� ������ ������ y ���� Constant.ZERO_POINT(0) ���� ����
        direction.y = Constant.ZERO_POINT;

        // ��ġ ���͸� ����ȭ �� ���� ���ʹϾ� ȸ�� ������ ���ϴ� �Լ��� ���ؼ� 
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);
        // Ʈ������ ������Ʈ�� �����̼� ���� Ÿ���� ���� ȸ��
        transform.rotation = rotation;

        //Movement ������Ʈ�� ������ ��ü�� ��ġ ���� Ÿ���� ��ġ ���� ���� Ÿ�ٰ��� �Ÿ� ���
        float distanceToPosition = Vector3.Distance(transform.position, target.transform.position);

        // Ÿ�ٰ��� �Ÿ��� Constant.DISTANCE ���� ũ�ٸ�
        if (distanceToPosition > Constant.DISTANCE)
        {
            // ���⺤���� ����ȭ(ũ�⸸�� ����) �� Constant.MOVE_SPEED(1f) ������ �����ӿ��� ���� �����ӱ����� �ð����� ���ؼ� ������ ������ ������ move ������ ����
            Vector3 move = direction.normalized * Constant.MOVE_SPEED * Time.deltaTime;
            // move ������ ���� Movement �� ������ ��ü�� ��ġ���� ���� �� �� ����� ��ü�� ��ġ���� ���� 
            transform.position += move;
        }
    }
    
    public void RandomMove()
    {
        // target�� �������� �ʴ´ٸ�, ������ �������� �����̴� ������ �Լ� 

        // ������ �������� ���� �� ���� ������ ������ ��ȯ�ϴ� Random.insideUnitSphere �� ��ǥ�� ���ϰ�
        Vector3 randomDirection = Random.insideUnitSphere * Constant.INSIDEUNITSPHERE;
        // y ���� Constant.ZERO_POINT(0) ���� ����
        randomDirection.y = Constant.ZERO_POINT;

        // ȣ��� ������ ������ �������� ��(Force)�� �༭ ������ �������� �ƴ� �� ������ �������� ����
        // Random.Range �Լ��� ���� ������ ���� ������ 
        float randomForce = Random.Range(Constant.MOVE_SPEED, Constant.FIND_TARGET);

        // ���� ��ǥ�� ���� ��, �ð����� ���� ������ ������ ����
        Vector3 move = randomDirection.normalized * randomForce * Time.deltaTime;

        // move ������ ���� Movement �� ������ ��ü�� ��ġ���� ���� �� �� ����� ��ü�� ��ġ���� ����
        transform.position += move;
        
    }
}
