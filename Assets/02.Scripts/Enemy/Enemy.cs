using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyType       //적 타입 열거형
{
    Basic,       //기본 타입 적
    Target,     // 나를 따라오는 적
    Follow,    //방향을 바꿔도 나를 따라오는 적 
}

public class Enemy : MonoBehaviour
{
    //enemy의 이동속도
    public float Speed = 1f;
    //enemy의 체력
    public int Health = 2;

    public EnemyType EType;

    private Vector2 _dir;

    private GameObject _target;

    public GameObject ItemPrefab;       //health

    public GameObject ItemPrefab2;      //speed

    private Animator MyAnimator;

    public AudioSource DieSound;

    public GameObject ExplosionVFXPrefab;

    //목표
    //Basic  타입 : 아래로 이동
    //Target 타입 : 처음 태어났을 때 플레이어가 있는 방향으로 이동
    //필요속성
    //-EnemyType 타입
    //구현순서 :
    //1. 시작할 때 방향을 구한다
    //2. 방향을 향해 이동한다
    // 단, 방향을 구할 때 플레이어가 있는 방향으로 이동해야한다
    void Start()
    {
        // scene에 있는 사운드소스가 있는 오브젝트
        GameObject SoundController = GameObject.Find("SoundController_EnemyDie");
        // 그 오브젝트에서 audiosource component를 가져오기
        DieSound = SoundController.GetComponent<AudioSource>();


        //캐싱 : 자주 쓰는 데이터를 더 가까운 장소에 저장해두고 필요할 때 가져다 쓰는 것
        //시작할 때 플레이어를 찾아서 기억해둔다
        _target = GameObject.Find("Player");

        MyAnimator = GetComponent<Animator>();

        if (EType == EnemyType.Target)
        {
            //1. 시작할 때 방향을 구한다
            //1-1. 플레이어를 찾는다(find)
            //GameObject target = GameObject.Find("Player");

            // = GameObject.FindGameObjectsWithTag("Player");
            //1-2. 방향을 구한다
            //플레이어위치 - 내위치 = 방향
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize();   //단위 벡터를 구함 ( 방향의 크기를 1로 만든다)

            //1.각도를 구한다
            //tan@ = y/x  -> @ = y/x * atan
            float radin = Mathf.Atan2(_dir.y, _dir.x);
            //Debug.Log(radin);       //호도법 -> 라디안 값
            float degree = radin * Mathf.Rad2Deg;
            //Debug.Log(degree);

            //2.각도에 맞게 회전한다
            transform.eulerAngles = new Vector3(0,0,degree+90);
            // = transform.rotation = Quaternion.Euler( new Vector3(0,0,degree + 90));   //이미지 리소스에 맞게 90도를 더한다
        }
        else if (EType == EnemyType.Basic)
        {
            _dir = Vector2.down;
            _dir.Normalize();
        }
    }

    private void Update()
    {
        // 1. 방향을 구한다.
       // Vector2 dir = Vector2.down;
        // 2. 이동한다.
        transform.position += (Vector3)(_dir * Speed) * Time.deltaTime;
        if (EType == EnemyType.Follow)      //매 프레임마다 나를 찾아야하기때문에  update에
        {
            //GameObject target = GameObject.Find("Player");
            _dir = _target.transform.position - this.transform.position; //방향을 구한다 (target - me)
            _dir.Normalize();   //단위 벡터를 구함 ( 방향의 크기를 1로 만든다)

            //1.각도를 구한다
            //tan@ = y/x  -> @ = y/x * atan
            float radin = Mathf.Atan2(_dir.y, _dir.x);
            //Debug.Log(radin);       //호도법 -> 라디안 값
            float degree = radin * Mathf.Rad2Deg;
            //Debug.Log(degree);

            //2.각도에 맞게 회전한다
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90));   //이미지 리소스에 맞게 90도를 더한다
        }
    }

    //OnCollisionEnter : 물체와의 충돌을 감지하는 함수
    private void OnCollisionEnter2D(Collision2D collision)      
    {
        //충돌을 시작했을 때
        //Debug.Log("Enter");



        //플레이어와의 충돌 체크
        if (collision.collider.tag == "Player")
        {
            //충돌한 객체로부터 Player 컴포넌트 가져오기
            Player player = collision.collider.GetComponent<Player>();
            //플레이어 체력 감소
            player.MinusHealth(1);
            
            //Health -= 1;

            //플레이어 체력이 0 이하일 경우 플레이어 죽음
            if (Health <= 0)
            {
                Death();
            }
            Death();
        }

        //총알과의 충돌 체크
        else if (collision.collider.tag == "Bullet")
        {
            //충돌한 객체로부터 Bullet 컴포넌트 가져오기
            Bullet bullet = collision.collider.GetComponent<Bullet>();
            Destroy(collision.collider.gameObject);
            

            //주 총알인 경우 적의 체력을 0으로 *************************
            if (bullet.BType == BulletType.Main)
            {
                Health -= 1;
            }
            else if(bullet.BType == BulletType.Sub)
            {
                Health -= 1;
            }
            if(Health <= 0)
            {
                DieSound.Play();
                Death();
                MakeItem();
            }
            else
            {
                    MyAnimator.Play("Hit"); 
            }
    
        }
    }

    // 만약에 적을 잡으면 = 적이 죽으면 
    public void Death()
    {
        Destroy(this.gameObject);
        GameObject vfx = Instantiate(ExplosionVFXPrefab);
        vfx.transform.position = this.transform.position;


        //목표 : 스코어를 증가시키고 싶다
        //1. Score를 증가시키기 위혀서는 씬에서 ScoreManager 오브젝트를 찾아온다
        GameObject smGameObject = GameObject.Find("ScoreManager");
        // 2. ScoreManager 게임 오브젝트에서 ScoreManager 스크립트 컴포넌트를 얻어온다
        ScoreManager scoreManager = smGameObject.GetComponent<ScoreManager>();
        // 3. 컴포넌트의 Score속성을 증가시킨다

      

        //(Get/Set) 캡슐화
        int score = scoreManager.GetScore();
        scoreManager.SetScore(score+1);
        //Debug.Log(scoreManager.GetScore());

    }


    public void MakeItem()
    { //순서 : 
     //1. 아이템 만들고
     //GameObject item = null;
     //GameObject item = Instantiate(ItemPrefab);  //ItemPrefab을 복제하여 만들어줌 -> 복제한 아이를 지칭 : GameObject item
     //위치를 적의 위치로 변경
     //item.transform.position = this.transform.position;  //item박스 안에 든 아이(Instantiate(ItemPrefab))의 위치 = 내 위치
     //this = 나 자신 = enemy
     //50%확률로 체력 올려주는 아이템, 50%확률로 이동속도 올려주는 아이템

        if (Random.Range(0, 2) == 0)
        {
            GameObject item = Instantiate(ItemPrefab);     
            item.transform.position = this.transform.position;
        }
        else
        {
            GameObject item = Instantiate(ItemPrefab2);      
            item.transform.position = this.transform.position;
        }
    }
}