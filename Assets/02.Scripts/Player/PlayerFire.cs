using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // [총알 발사 제작]
    // 목표: 총알을 만들어서 발사하고 싶다.
    // 속성:
    //   - 총알 프리팹
    //   - 총구
    // 구현 순서
    // 1. 발사 버튼을 누르면
    // 2. 프리팹으로부터 총알을 동적으로 만들고,
    // 3. 만든 총알의 위치를 총구의 위치로 바꾼다.

    [Header("총알 프리팹")]
    public GameObject BulletPrefab;  // 총알 프리팹
    public GameObject SubBulletPrefab;  // 보조 총알 프리팹

    // 목표 : 태어날 때 풀에다가 총알을 '풀 사이즈'개 생성한다
    // 속성 : 
    // - 풀 사이즈 
    public int PoolSize = 100;
    // - 오브젝트(Bullet) 풀
    public List<Bullet> _bulletPool = null;
    //public List<GameObject> _subbulletPool = null;

    // 순서 :
    // 1. 태어날 때 : Awake
    private void Awake()
    {
        // 2. 오브젝트 풀 할당해주고 
        _bulletPool = new List<Bullet>();
        //_subbulletPool = new List<GameObject>();

        // 3. 총알 프리팹으로부터 총알을 풀 사이즈만큼 생성해준다
        for (int i = 0; i < PoolSize; i++)
        {
           GameObject bullet = Instantiate(BulletPrefab);

            // 4. 생성한 총알을 풀에다가 넣는다
            _bulletPool.Add(bullet.GetComponent<Bullet>());

            // 5. 끈다
            bullet.SetActive(false);
        }
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject subBullet = Instantiate(SubBulletPrefab);

            // 4. 생성한 총알을 풀에다가 넣는다
            _bulletPool.Add(subBullet.GetComponent<Bullet>());

            // 5. 끈다
            subBullet.SetActive(false);
        }
    }

    [Header("총구들")]
    public List<GameObject>Muzzles;
    public List<GameObject>SubMuzzles;

   // public GameObject[] Muzzles;     // 총구들
   // public GameObject[] SubMuzzles;  // 총구들

    [Header("타이머")]
    public float Timer = 10f;       //총알발사용타이머
    public const float COOL_TIME = 0.6f;        //총알발사용타이머

    public float BoomTime = 0f;
    public const float BoomCoolTime = 5f;

    [Header("자동 모드")]
    public bool AutoMode = false;

    public AudioSource FireSource;
    public GameObject BoomSkill;

    private void Start()
    {
        Timer = 0f;
        AutoMode = false;
    }

    void Update()
    {// 타이머 계산
        Timer -= Time.deltaTime;
        BoomTime += Time.deltaTime;

        CheckAutoMode();

        bool ready = AutoMode || Input.GetKeyDown(KeyCode.Space);
        if (Timer <= 0 && ready)
        {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Boom();
        }
    }

    private void Boom()
    {
            if(BoomTime >= BoomCoolTime)
            { 
                GameObject Boom = Instantiate(BoomSkill);
                Boom.transform.position = Vector3.zero;
                BoomTime = 0;
            }
    }

    private void CheckAutoMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("자동 공격 모드");
            AutoMode = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("수동 공격 모드");
            AutoMode = false;
        }
    }
    private void Fire()
    {  
            FireSource.Play();

        // 타이머 초기화
        Timer = COOL_TIME;

        // 3-1. 메인총알
        for (int i = 0; i < Muzzles.Count; i++)
            {
                // 1. 꺼져있는 총알을 찾아 꺼낸다
                Bullet bullet = null;
                foreach (Bullet b in _bulletPool)
                {
                    //만약에 꺼져(비활성화되어)있고 && 메인총알이라면
                    if (b.gameObject.activeInHierarchy == false && b.BType == BulletType.Main)
                    {
                        bullet = b;
                        break;  //찾았기 때문에 그 뒤까지 찾을 필요가 없다
                    }
                }
                // 2. 꺼낸 총알의 위치를 각 총구의 위치로 바꾼다
                bullet.transform.position = Muzzles[i].transform.position;
                // 3. 총알을 킨다(발사)
                bullet.gameObject.SetActive(true);
            }
            // 3-2. 서브총알
            for (int i = 0; i < SubMuzzles.Count; i++)
            {
                // 1. 꺼져있는 총알을 찾아 꺼낸다
                Bullet SubBullet = null;
                foreach (Bullet b in _bulletPool)
                {
                    //만약에 꺼져있다면
                    if (b.gameObject.activeInHierarchy == false && b.BType == BulletType.Sub)
                    {
                        SubBullet = b;
                        break;  //찾았기 때문에 그 뒤까지 찾을 필요가 없다
                    }
                }
                // 2. 꺼낸 총알의 위치를 각 총구의 위치로 바꾼다
                SubBullet.transform.position = SubMuzzles[i].transform.position;
                // 3. 총알을 킨다(발사)
                SubBullet.gameObject.SetActive(true);
            }
    }

/*    private void ClickAutoMode()
    {
        if(AutoMode == true)
        {
            AutoMode = false;
        }
        else if(AutoMode == false)
        {
            AutoMode = true;
        }
    }*/

    //조이스틱
    // 총알 발사
    public void OnClickXButton()
    {
        Debug.Log("X 버튼이 클릭되었습니다");
        if (Timer <= 0)
        {
            Fire();
        }
    }

    // 자동 공격 On/Off
    public void OnClickYButton()
    {
        Debug.Log("Y 버튼이 클릭되었습니다");
        AutoMode = !AutoMode;
    }

    // 궁극기 사용
    public void OnClickBButton()
    {
        Debug.Log("B 버튼이 클릭되었습니다");
        Boom();
    }

}
    // 2. 프리팹으로부터 총알을 만든다.
    //GameObject bullet1 = Instantiate(BulletPrefab);
    //GameObject bullet2 = Instantiate(BulletPrefab);

    // 3. 만든 총알의 위치를 총구의 위치로 바꾼다.
    //bullet1.transform.position = Muzzle.transform.position;
    //bullet2.transform.position = Muzzle2.transform.position;

    // 목표 : 총구 개수 만큼 총알을 플에서 꺼내 쓴다
    // 순서 : 
    // 1. 꺼져있는 총알을 꺼낸다
    // 2. 꺼낸 총알의 위치를 각 총구의 위치로 바꾼다
    // 3. 총알을 킨다(발사)