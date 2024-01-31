using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{//player이동에 대한 책임을 가진 객체 생성

    /*
        목표 : 플레이어를 이동하고 싶다.
        필요 속성 :
                - 이동속도
         순서:
                1. 키보드 입력을 받는다
                2. 키보드 입력에 따라 이동할 방향을 계산한다
                3. 이동할 방향과 이동 속도에 따라 플레이어를 이동시킨다
     */

    //은닉화
    private float Speed = 3f;      //speed가 의미하는 것 -> 속도 : 초당 3unit만큼 이동한다는 뜻

    public const float MinX = -3f;
    public const float MaxX = 3f;
    public const float MinY = -6f;
    public const float MaxY = 0f;

    public Animator MyAnimator;     //애니메이터에대한속성추가

    private void Awake()
    {
        MyAnimator = this.gameObject.GetComponent<Animator>();     
    }

    void Update()
    {
        //transform.Translate(Vector2.up * Speed * Time.deltaTime);                   
        //게임오브젝트의 transform을 가져와서 사용한다. transfom을 translate해준다
        //vector.up : (0,1) 위쪽 방향만을 나타냄
        //(0 ,1) * 3 -> (0 , 3) 
        //deltaTime 은 프레임 간 시간 간격을 반환한다
        //30fps : d-> 0.03초   
        //60fps : d-> 0.016초

        //1.키보드 입력을 받는다
        //Input.GetAxis   vs   Input.GetAxisRaw
        //float h = Input.GetAxis("Horizontal");                 //키보드 좌우 입력에 따라 -1.0f  ~ +1.0f 까지 사이의 값을 반환(부드러운이동 = 연속적인 값)
        //float v = Input.GetAxis("Vertical");                    //키보드 위아래 입력에 따라 -1.0f  ~ +1.0f 까지 사이의 값을 반환(부드러운이동)

        float h = Input.GetAxisRaw("Horizontal");           //키보드 좌우 입력에 따라 -1.0f  , 0 ,  +1.0f 까지 사이의 값을 반환(불연속적인 값)
        float v = Input.GetAxisRaw("Vertical");               //키보드 위아래 입력에 따라 -1.0f  , 0 ,  +1.0f 까지 사이의 값을 반환
        //Debug.Log($"h: {h}, v: {v}");


        //애니메이터에게 파라미터 값을 넘겨줌
        MyAnimator.SetInteger("h", (int)h);


        //2.키보드 입력에 따라 이동할 방향을 계산한다
        //방향을 각 성분으로 제작
        Vector2 dir = new Vector2(h,v);                 //  = Vector2 dir = Vector2.right * h + Vector2.up * v;  =  ( (1,0)  * h +       (0, 1)    *  v; )
        //Debug.Log($"정규화 전 : {dir.magnitude}");
        //이동 방향을 정규화 (방향은 같지만 길이를 1로 만들어줌)
        dir = dir.normalized;
        //Debug.Log ($"정규화 후 : {dir.sqrMagnitude}");

        //3.이동할 방향과 이동 속도에 따라 플레이어를 이동시킨다
        //transform.Translate( dir * Speed * Time.deltaTime);
        //공식을 이용한 이동
        //새로운 위치 = 현재 위치 + 속도 * 시간
        Vector2  newPosition = transform.position + (Vector3)(dir * Speed * Time.deltaTime);
       /* newPosition.x = 
        newPosition.y= */

      /* if (newPosition.x <MinX)
        {
            newPosition.x = MinX;
        }
        else if(newPosition.x > MaxX)
        {
            newPosition.x = MaxX;
        }*/
     
       //오브젝트 이동 제한하기
       //1. Mathf 수학 라이브러리 이용
       // newPosition.y = Mathf.Max(MinY, newPosition.y);
        //newPosition.y = Mathf.Min(newPosition.y, MaxY);

        //2. clamp 함수 이용
       // newPosition.y = Mathf.Clamp(newPosition.y , MinY, MaxY);
        //newPosition.x = Mathf.Clamp(newPosition.x , MinX, MaxX);

        /*3. if 조건문 이용
         if (newPosition.y >MaxY) 
         {
             newPosition.y = MaxY;
         }
        else if( newPosition.y < MinY)
         {
             newPosition.y = MinY;
         }*/


        //오브젝트 위치 바꾸기
        // if 조건문 이용
        if (newPosition.x >= MaxX)
        {
            newPosition.x = MinX;
        }
        else if (newPosition.x <= MinX)
        {
            newPosition.x = MaxX;
        }

        transform.position = newPosition;           //플레이어의 위치 = 새로운 위치    
        //4. 현재 위치 출력
       // Debug.Log(newPosition);


        if (Input.GetKeyDown(KeyCode.E))
        {
            Speed++;
            // = Speed += 1;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed--;
            // = Speed -= 1;
        }
        //getKey = 누르고 있는 동안
        //getKeyDown = 처음 눌렀을 때만
    }


    public float GetSpeed()
    {
        return Speed;
    }
    public void SetSpeed(float speed)
    {
         Speed = speed;
    }
    public void AddSpeed(float speed)
    {
        Speed += speed;
        Debug.Log($"속도 : {Speed}");
    }
}

/*업데이트 함수 : 이동, 스피드 업 다운
나눌 수 잇음 -> 기능별로 모듈화 = 가독성 높고 재사용률 높아짐
 */
