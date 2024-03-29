using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{//목표 : 다른 물체와 충돌하면 충돌한 물체를 파괴(삭제)한다
 //구현 순서:
 // 1. 만약 다른 물체와 충돌하면
 // 2. 충돌한 물체를 파괴한다

    //1. 만약에 다른 물체와 충돌하면
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Bullet" || otherCollider.tag == "Enemy")
        {
            otherCollider.gameObject.SetActive(false);      //bullet이라면 삭제가아닌 꺼준다
        }
        else
        {
            //2. 충돌한 물체를 파괴한다
            Destroy(otherCollider.gameObject);
        }
    }
}
