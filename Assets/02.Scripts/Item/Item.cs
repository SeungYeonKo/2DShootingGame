using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;


public class Item : MonoBehaviour
{
    public float _timer = 0f;  //�ð��� üũ�� ����
    public const float EAT_TIME = 1.0f;

    private const float Follow_Time = 3f;

    public int MyType = 0;

    public Animator MyAnimator;

  //  public AudioSource ItemEatSource;

    public GameObject HealthItemVFXPrefab;
    public GameObject SpeedItemVFXPrefab;



    //(�ٸ� �ݶ��̴��� ����) Ʈ���Ű� �ߵ��� ��
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //� collider�� �Ѿ�Դ����� ���� ������ �Ѿ��
        //Debug.Log("Ʈ���Ž���!");

        //���� : �÷��̾��� ü���� �ø��� �ʹ�.


        //���� : 
        // 1. �÷��̾� ��ũ��Ʈ �޾ƿ���
        //GameObject playerGameObject = GameObject.Find("Player");
        //Player player = playerGameObject .GetComponent<Player>();
        Player player = otherCollider.gameObject.GetComponent<Player>();

        // 2. �÷��̾� ü�� �ø���
        //player.Health ++;
        //Debug.Log($"�����÷��̾��� ü�� : {player.Health}");

     /*  �ٸ���� :  
         if (otherCollider.tag == "Player")
        {
            Player player = otherCollider.GetComponent<Player>(); //***************
            player.Health += 1;
            Debug.Log($"�÷��̾�ü�� : {player.Health}");
        }*/
    }

    //(�ٸ� �ݶ��̴��� ����) Ʈ���Ű� �ߵ� ���� ��
    private void OnTriggerStay2D(Collider2D otherCollider)
    {
       //Debug.Log("Ʈ������!");
       // _timer += Time.deltaTime;
        //Debug.Log(_timer);

        if (_timer >= EAT_TIME)
        {
            //Ÿ���� 0�̸� �÷��̾��� ü�� �÷��ֱ�
            if (MyType == 0)
        {
                //ItemEatSource.Play();
                Player player = otherCollider.GetComponent<Player>();
                player.Health += 1;
                Debug.Log($"�����÷��̾��� ü�� : {player.Health}");

                Destroy(this.gameObject);
                GameObject vfx = Instantiate(HealthItemVFXPrefab);
                vfx.transform.position = this.transform.position;

            }
            else if (MyType == 1)
            {
                //ItemEatSource.Play();
                //Ÿ���� 1�̸� �÷��̾��� ���ǵ� �÷��ֱ�
                PlayerMove playerMove = otherCollider.GetComponent<PlayerMove>();
                playerMove.Speed += 1;
                Debug.Log($"�����÷��̾��� �ӵ� : {playerMove.Speed}");
                
                Destroy(this.gameObject);
                GameObject vfx = Instantiate(SpeedItemVFXPrefab);
                vfx.transform.position = this.transform.position;

            }
            }
    }
    //(�ٸ� �ݶ��̴��� ����) Ʈ���Ű� ������ ��
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        //_timer = 0f;        //trigger�� ����� timer�� 0���� �ʱ�ȭ
        //Debug.Log("Ʈ��������!");

     
    }

    private void Start()
    {
        _timer = 0f;
        MyAnimator = GetComponent<Animator>();
        MyAnimator.SetInteger("ItemType", MyType);

        // scene�� �ִ� ����ҽ��� �ִ� ������Ʈ
        GameObject SoundController = GameObject.Find("SoundController_item");
        // �� ������Ʈ���� audiosource component�� ��������
        //ItemEatSource = SoundController.GetComponent<AudioSource>();
    }



    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >=Follow_Time)
        {
            //1.�÷��̾ã��
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            //2.���������ϰ�
            Vector3 dir = target.transform.position - this.transform.position;
            dir.Normalize();
            //3.���ǵ忡 �°� �̵�
            Vector3 newPosition = transform.position + dir * 10f * Time.deltaTime;
            this.transform.position = newPosition;
        }
    }
}
//collision �浹
//collider �浹ü