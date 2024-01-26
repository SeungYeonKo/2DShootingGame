using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyType       //�� Ÿ�� ������
{
    Basic,       //�⺻ Ÿ�� ��
    Target,     // ���� ������� ��
    Follow,    //������ �ٲ㵵 ���� ������� �� 
}

public class Enemy : MonoBehaviour
{
    //enemy�� �̵��ӵ�
    public float Speed = 1f;
    //enemy�� ü��
    public int Health = 2;

    public EnemyType EType;

    private Vector2 _dir;

    private GameObject _target;

    public GameObject ItemPrefab;       //health

    public GameObject ItemPrefab2;      //speed

    private Animator MyAnimator;

    public AudioSource DieSound;

    public GameObject ExplosionVFXPrefab;

    //��ǥ
    //Basic  Ÿ�� : �Ʒ��� �̵�
    //Target Ÿ�� : ó�� �¾�� �� �÷��̾ �ִ� �������� �̵�
    //�ʿ�Ӽ�
    //-EnemyType Ÿ��
    //�������� :
    //1. ������ �� ������ ���Ѵ�
    //2. ������ ���� �̵��Ѵ�
    // ��, ������ ���� �� �÷��̾ �ִ� �������� �̵��ؾ��Ѵ�
    void Start()
    {
        // scene�� �ִ� ����ҽ��� �ִ� ������Ʈ
        GameObject SoundController = GameObject.Find("SoundController_EnemyDie");
        // �� ������Ʈ���� audiosource component�� ��������
        DieSound = SoundController.GetComponent<AudioSource>();


        //ĳ�� : ���� ���� �����͸� �� ����� ��ҿ� �����صΰ� �ʿ��� �� ������ ���� ��
        //������ �� �÷��̾ ã�Ƽ� ����صд�
        _target = GameObject.Find("Player");

        MyAnimator = GetComponent<Animator>();

        if (EType == EnemyType.Target)
        {
            //1. ������ �� ������ ���Ѵ�
            //1-1. �÷��̾ ã�´�(find)
            //GameObject target = GameObject.Find("Player");

            // = GameObject.FindGameObjectsWithTag("Player");
            //1-2. ������ ���Ѵ�
            //�÷��̾���ġ - ����ġ = ����
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize();   //���� ���͸� ���� ( ������ ũ�⸦ 1�� �����)

            //1.������ ���Ѵ�
            //tan@ = y/x  -> @ = y/x * atan
            float radin = Mathf.Atan2(_dir.y, _dir.x);
            //Debug.Log(radin);       //ȣ���� -> ���� ��
            float degree = radin * Mathf.Rad2Deg;
            //Debug.Log(degree);

            //2.������ �°� ȸ���Ѵ�
            transform.eulerAngles = new Vector3(0,0,degree+90);
            // = transform.rotation = Quaternion.Euler( new Vector3(0,0,degree + 90));   //�̹��� ���ҽ��� �°� 90���� ���Ѵ�
        }
        else if (EType == EnemyType.Basic)
        {
            _dir = Vector2.down;
            _dir.Normalize();
        }
    }

    private void Update()
    {
        // 1. ������ ���Ѵ�.
       // Vector2 dir = Vector2.down;
        // 2. �̵��Ѵ�.
        transform.position += (Vector3)(_dir * Speed) * Time.deltaTime;
        if (EType == EnemyType.Follow)      //�� �����Ӹ��� ���� ã�ƾ��ϱ⶧����  update��
        {
            //GameObject target = GameObject.Find("Player");
            _dir = _target.transform.position - this.transform.position; //������ ���Ѵ� (target - me)
            _dir.Normalize();   //���� ���͸� ���� ( ������ ũ�⸦ 1�� �����)

            //1.������ ���Ѵ�
            //tan@ = y/x  -> @ = y/x * atan
            float radin = Mathf.Atan2(_dir.y, _dir.x);
            //Debug.Log(radin);       //ȣ���� -> ���� ��
            float degree = radin * Mathf.Rad2Deg;
            //Debug.Log(degree);

            //2.������ �°� ȸ���Ѵ�
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90));   //�̹��� ���ҽ��� �°� 90���� ���Ѵ�
        }
    }

    //OnCollisionEnter : ��ü���� �浹�� �����ϴ� �Լ�
    private void OnCollisionEnter2D(Collision2D collision)      
    {
        //�浹�� �������� ��
        //Debug.Log("Enter");



        //�÷��̾���� �浹 üũ
        if (collision.collider.tag == "Player")
        {
            //�浹�� ��ü�κ��� Player ������Ʈ ��������
            Player player = collision.collider.GetComponent<Player>();
            //�÷��̾� ü�� ����
            player.Health -= 1;

            //�÷��̾� ü���� 0 ������ ��� �÷��̾� ����
            if (player.Health <= 0)
            {
                Death();

            }
            Death();
            //�÷��̾�� ����� �� enemy(�� �ڽ�) �״´�
        }

        //�Ѿ˰��� �浹 üũ
        else if (collision.collider.tag == "Bullet")
        {
            //�浹�� ��ü�κ��� Bullet ������Ʈ ��������
            Bullet bullet = collision.collider.GetComponent<Bullet>();
            Destroy(collision.collider.gameObject);
            

            //�� �Ѿ��� ��� ���� ü���� 0���� *************************
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

    // ���࿡ ���� ������ = ���� ������ 
    public void Death()
    {
        Destroy(this.gameObject);
        GameObject vfx = Instantiate(ExplosionVFXPrefab);
        vfx.transform.position = this.transform.position;

        
        //��ǥ : ���ھ ������Ű�� �ʹ�
        //1. Score�� ������Ű�� �������� ������ ScoreManager ������Ʈ�� ã�ƿ´�
        GameObject smGameObject = GameObject.Find("ScoreManager");
        // 2. ScoreManager ���� ������Ʈ���� ScoreManager ��ũ��Ʈ ������Ʈ�� ���´�
        ScoreManager scoreManager = smGameObject.GetComponent<ScoreManager>();
        // 3. ������Ʈ�� Score�Ӽ��� ������Ų��
        scoreManager.Score += 1;
        // ��ǥ : Score�� ȭ�鿡 ǥ���Ѵ�
        scoreManager.ScoreTextUI.text = $"���� : {scoreManager.Score}";
        
        //��ǥ : �ְ� ������ �����ϰ�  UI �� ǥ���ϰ� �ʹ�
        // 1. ���� ���� ������ �ְ� �������� ũ�ٸ�
        if(scoreManager.Score > scoreManager.BestScore) 
        {
            // 2. �ְ� ������ �����ϰ�,
            scoreManager.BestScore = scoreManager.Score;

            // ��ǥ : �ְ� ������ ����
            // PlayerPrefs Ŭ������ ���
            // -> ���� Ű(key)�� ��(value) ���·� �����ϴ� Ŭ����
            // ������ �� �ִ� ������Ÿ�� : int, float, string
            // Ÿ�Ժ��� ����/�ε尡 ������ Set/Get �޼��尡 �ִ�
            PlayerPrefs.SetInt("BestScore", scoreManager.BestScore);



            // 3. UI�� ǥ���Ѵ�
            scoreManager.BestScoreTextUI.text = $"�ְ� ���� : {scoreManager.BestScore}";
        }
       
    }


    public void MakeItem()
    { //���� : 
     //1. ������ �����
     //GameObject item = null;
     //GameObject item = Instantiate(ItemPrefab);  //ItemPrefab�� �����Ͽ� ������� -> ������ ���̸� ��Ī : GameObject item
     //��ġ�� ���� ��ġ�� ����
     //item.transform.position = this.transform.position;  //item�ڽ� �ȿ� �� ����(Instantiate(ItemPrefab))�� ��ġ = �� ��ġ
     //this = �� �ڽ� = enemy
     //50%Ȯ���� ü�� �÷��ִ� ������, 50%Ȯ���� �̵��ӵ� �÷��ִ� ������

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