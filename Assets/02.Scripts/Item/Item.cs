using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;


public class Item : MonoBehaviour
{
    public float _timer = 0f;  //시간을 체크할 변수
    public const float EAT_TIME = 1.0f;

    private const float Follow_Time = 3f;

    public int MyType = 0;

    public Animator MyAnimator;

  //  public AudioSource ItemEatSource;

    public GameObject HealthItemVFXPrefab;
    public GameObject SpeedItemVFXPrefab;



    //(다른 콜라이더에 의해) 트리거가 발동될 때
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //어떤 collider가 넘어왔는지에 대한 정보가 넘어옴
        //Debug.Log("트리거시작!");

        //목적 : 플레이어의 체력을 올리고 싶다.


        //순서 : 
        // 1. 플레이어 스크립트 받아오기
        //GameObject playerGameObject = GameObject.Find("Player");
        //Player player = playerGameObject .GetComponent<Player>();
        Player player = otherCollider.gameObject.GetComponent<Player>();

        // 2. 플레이어 체력 올리기
        //player.Health ++;
        //Debug.Log($"현재플레이어의 체력 : {player.Health}");

     /*  다른방법 :  
         if (otherCollider.tag == "Player")
        {
            Player player = otherCollider.GetComponent<Player>(); //***************
            player.Health += 1;
            Debug.Log($"플레이어체력 : {player.Health}");
        }*/
    }

    //(다른 콜라이더에 의해) 트리거가 발동 중일 때
    private void OnTriggerStay2D(Collider2D otherCollider)
    {
       //Debug.Log("트리거중!");
       // _timer += Time.deltaTime;
        //Debug.Log(_timer);

        if (_timer >= EAT_TIME)
        {
            //타입이 0이면 플레이어의 체력 올려주기
            if (MyType == 0)
        {
                //ItemEatSource.Play();
                Player player = otherCollider.GetComponent<Player>();
                player.Health += 1;
                Debug.Log($"현재플레이어의 체력 : {player.Health}");

                Destroy(this.gameObject);
                GameObject vfx = Instantiate(HealthItemVFXPrefab);
                vfx.transform.position = this.transform.position;

            }
            else if (MyType == 1)
            {
                //ItemEatSource.Play();
                //타입이 1이면 플레이어의 스피드 올려주기
                PlayerMove playerMove = otherCollider.GetComponent<PlayerMove>();
                playerMove.Speed += 1;
                Debug.Log($"현재플레이어의 속도 : {playerMove.Speed}");
                
                Destroy(this.gameObject);
                GameObject vfx = Instantiate(SpeedItemVFXPrefab);
                vfx.transform.position = this.transform.position;

            }
            }
    }
    //(다른 콜라이더에 의해) 트리거가 끝났을 때
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        //_timer = 0f;        //trigger을 벗어나면 timer는 0으로 초기화
        //Debug.Log("트리거종료!");

     
    }

    private void Start()
    {
        _timer = 0f;
        MyAnimator = GetComponent<Animator>();
        MyAnimator.SetInteger("ItemType", MyType);

        // scene에 있는 사운드소스가 있는 오브젝트
        GameObject SoundController = GameObject.Find("SoundController_item");
        // 그 오브젝트에서 audiosource component를 가져오기
        //ItemEatSource = SoundController.GetComponent<AudioSource>();
    }



    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >=Follow_Time)
        {
            //1.플레이어를찾고
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            //2.방향을정하고
            Vector3 dir = target.transform.position - this.transform.position;
            dir.Normalize();
            //3.스피드에 맞게 이동
            Vector3 newPosition = transform.position + dir * 10f * Time.deltaTime;
            this.transform.position = newPosition;
        }
    }
}
//collision 충돌
//collider 충돌체