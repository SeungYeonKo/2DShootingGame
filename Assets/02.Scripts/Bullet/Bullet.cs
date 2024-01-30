using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum BulletType // 총알 타입에 대한 열거형(상수를 기억하기 좋게 하나의 이름으로 그룹화하는 것)
{//밑에서 bulletType 지정할 때 0,1,2 이런 매직 넘버를 사용하면 헷갈리거나 까먹기 때문에 열거형을 사용
    Main = 0,
    Sub = 1,
    Pet = 2,
}


public class Bullet : MonoBehaviour
{
    //public int BType = 0;
    public  BulletType BType = BulletType.Main;     // 0이면 주총알, 1이면 보조총알, 2면 펫이 쏘는 총알

    // [총알 이동 구현]
    // 목표: 총알이 위로 계속 이동하고 싶다.
    // 속성:
    // - 속력
    // 구현 순서
    // 1. 이동할 방향을 구한다.
    // 2. 이동한다.

    public float Speed;


    void Update()  //살아가는 중 계속 호출되는 함수는 Update이므로 이곳에 구현
    {
        // 1. 이동할 방향을 구한다.
        Vector2 dir = Vector2.up;    // = Vector2 dir = new Vector2(0,1);

        // 2. 이동한다.
        // 새로운 위치 = 현재위치 * 속도 * 시간
        //transform.Translate(dir * Speed * Time.deltaTime);
        transform.position += (Vector3)(dir * Speed) * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(collision.collider.gameObject);
        //Destroy(this.gameObject);
    }
}
