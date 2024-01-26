using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround2 : MonoBehaviour
{
    // 목표 : Material을 이용해서 배경 스크롤이 되도록 하고싶다
    // 필요속성
    // - 머터리얼
    // - 스크롤 속도

    public Material MyMaterial;
    public float ScrollSpeed = 0.2f;      //초당 0.2정도 이동한다

    // 구현 순서

    
    // 0. 매 프레임마다
    private void Update()
    {
        // 1. 방향을 구한다
        Vector2 dir = Vector2.up;

        // 2. (오프셋을 변경해서) 스크롤 한다
        MyMaterial.mainTextureOffset += dir * ScrollSpeed * Time.deltaTime; 
    }
}
