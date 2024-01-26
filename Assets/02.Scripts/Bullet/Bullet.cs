using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum BulletType // �Ѿ� Ÿ�Կ� ���� ������(����� ����ϱ� ���� �ϳ��� �̸����� �׷�ȭ�ϴ� ��)
{//�ؿ��� bulletType ������ �� 0,1,2 �̷� ���� �ѹ��� ����ϸ� �򰥸��ų� ��Ա� ������ �������� ���
    Main = 0,
    Sub = 1,
    Pet = 2,
}


public class Bullet : MonoBehaviour
{
    //public int BType = 0;
    public  BulletType BType = BulletType.Main;     // 0�̸� ���Ѿ�, 1�̸� �����Ѿ�, 2�� ���� ��� �Ѿ�

    // [�Ѿ� �̵� ����]
    // ��ǥ: �Ѿ��� ���� ��� �̵��ϰ� �ʹ�.
    // �Ӽ�:
    // - �ӷ�
    // ���� ����
    // 1. �̵��� ������ ���Ѵ�.
    // 2. �̵��Ѵ�.

    public float Speed;


    void Update()  //��ư��� �� ��� ȣ��Ǵ� �Լ��� Update�̹Ƿ� �̰��� ����
    {
        // 1. �̵��� ������ ���Ѵ�.
        Vector2 dir = Vector2.up;    // = Vector2 dir = new Vector2(0,1);

        // 2. �̵��Ѵ�.
        // ���ο� ��ġ = ������ġ * �ӵ� * �ð�
        //transform.Translate(dir * Speed * Time.deltaTime);
        transform.position += (Vector3)(dir * Speed) * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(collision.collider.gameObject);
        Destroy(this.gameObject);
    }
}
