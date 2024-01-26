using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float Speed = 1;

    private void Update()
    {
        //��潺ũ��
        //-> ���� ȭ�鿡�� ��� �̹����� ������ �ӵ��� ������
        // ĳ���ͳ� ���� ���� �������� �� �������� ������ִ� ���
        // (ĳ���ʹ� �״�� �ΰ� ��游 �������� ĳ���Ͱ� �����̴� �� ó�� �������� �Ѵ�)

        // ��ǥ : �Ʒ��� �̵��ϰ� �ʹ�
        // ���� :
        // 1. ������ ���ϰ�
        Vector2 dir = Vector2.down;



        // 2. �̵��Ѵ�
        Vector2 newPosition = transform.position + (Vector3)(dir * Speed) * Time.deltaTime;

        if (newPosition.y < -11.87)
        {
            newPosition.y = 11.95f;
        }
        transform.position = newPosition;
    }
}
