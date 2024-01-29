using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawner : MonoBehaviour
{//역할 : 일정 시간마다 적을 프리팹으로부터 생성해서 내 위치에 갖다 놓고 싶다
 //필요속성 :
 //-적프리팹
 //-일정시간
 //-현재시간
 //구현순서 : 
 //1. 시간이 흐르다가
 //2. 만약 시간이 일정 시간이 되면
 //3. 프리팹으로부터 적을 생성한다
 //4. 생성한 적의 위치를 내 위치로 바꾼다

    [Header("적 프리팹")]
    public GameObject EnemyPrefab;              // EnemyBasic 프리팹
    public GameObject EnemyTargetPrefab;   // EnemyTarget 프리팹
    public GameObject EnemyFollowPrefab;   // EnemyFollow 프리팹

    [Header("타이머")]
    public float SpawnTime = 0.8f;      //스폰 간격 시간
    public float CurrentTimer = 0f;      //현재 경과 시간
    public float Enemyrate;                  //적 생성 확률

    //목표 : 적 생성 시간을 랜덤하게 하고 싶다
    //필요 속성 :
    //-최소시간
    //-최대시간
    public float MinTime = 0.5f;        //최소 스폰 시간
    public float MaxTime = 1.5f;       //최대 스폰 시간

    private void Start()
    { //시작할 때 적 생성 시간을 랜덤하게 설정한다
        SetRandomTime();
    }

    private void SetRandomTime()   //랜덤스폰함수
    {
        SpawnTime = Random.Range(MinTime, MaxTime);     //최소와 최대 스폰 시간 사이에서 랜덤한 시간을 선택
    }

    private void SetRandomRate()   //랜덤스폰함수
    {
        GameObject enemy = null;
        Enemyrate = Random.Range(0, 10);
        if (Enemyrate <1)
        {
             enemy = Instantiate(EnemyFollowPrefab);
        }
        else if (Enemyrate <4  &&  Enemyrate >1)
        {
           enemy = Instantiate(EnemyTargetPrefab);
        }
        else
        {
           enemy = Instantiate(EnemyPrefab);
        }
        enemy.transform.position = this.transform.position;
    }

    void Update()
    {//구현순서
        //1.시간이 흐르다가
        CurrentTimer += Time.deltaTime;     //경과시간을 업데이트
        //2.일정시간이되면
        if (CurrentTimer >= SpawnTime)      //일정시간이 지나면
        {
            CurrentTimer = 0f;
            //다음 생성 시간을 랜덤하게 설정
            SetRandomTime();
            // 프리팹으로부터 적을 생성
            SetRandomRate();
            //타이머 초기화
        }
    }
}