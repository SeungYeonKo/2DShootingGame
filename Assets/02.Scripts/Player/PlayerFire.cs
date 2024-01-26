using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // [�Ѿ� �߻� ����]
    // ��ǥ: �Ѿ��� ���� �߻��ϰ� �ʹ�.
    // �Ӽ�:
    //   - �Ѿ� ������
    //   - �ѱ�
    // ���� ����
    // 1. �߻� ��ư�� ������
    // 2. ���������κ��� �Ѿ��� �������� �����,
    // 3. ���� �Ѿ��� ��ġ�� �ѱ��� ��ġ�� �ٲ۴�.

    [Header("�Ѿ� ������")]
    public GameObject BulletPrefab;  // �Ѿ� ������
    public GameObject SubBulletPrefab;  // ���� �Ѿ� ������
  

    [Header("�ѱ���")]
    public GameObject[] Muzzles;     // �ѱ���
    public GameObject[] SubMuzzles;  // �ѱ���

    [Header("Ÿ�̸�")]
    public float Timer = 10f;       //�Ѿ˹߻��Ÿ�̸�
    public const float COOL_TIME = 0.6f;        //�Ѿ˹߻��Ÿ�̸�

    public const float BoomCoolTime = 5f;
    public float BoomTime = 0f;

    [Header("�ڵ� ���")]
    public bool AutoMode = false;

    public AudioSource FireSource;
    public GameObject BoomSkill;

    private void Start()
    {
        Timer = 0f;
        AutoMode = false;
    }

    void Update()
    {// Ÿ�̸� ���
        Timer -= Time.deltaTime;
        BoomTime += Time.deltaTime;

        CheckAutoMode();
        Fire();
        Boom();
    }
    private void Boom()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(BoomTime >= BoomCoolTime)
            { 
                GameObject Boom = Instantiate(BoomSkill);
                Boom.transform.position = Vector3.zero;
                BoomTime = 0;
            }
        }
    }

    private void CheckAutoMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("�ڵ� ���� ���");
            AutoMode = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("���� ���� ���");
            AutoMode = false;
        }
    }
    private void Fire()
    {
        // 1. Ÿ�̸Ӱ� 0���� ���� ���¿��� �߻� ��ư�� ������
        bool ready = AutoMode || Input.GetKeyDown(KeyCode.Space);
        if (Timer <= 0 && ready)
        {
            FireSource.Play();

            // Ÿ�̸� �ʱ�ȭ
            Timer = COOL_TIME;

            // 2. ���������κ��� �Ѿ��� �����.
            //GameObject bullet1 = Instantiate(BulletPrefab);
            //GameObject bullet2 = Instantiate(BulletPrefab);

            // 3. ���� �Ѿ��� ��ġ�� �ѱ��� ��ġ�� �ٲ۴�.
            //bullet1.transform.position = Muzzle.transform.position;
            //bullet2.transform.position = Muzzle2.transform.position;

            // ��ǥ: �ѱ� ���� ��ŭ �Ѿ��� �����, ���� �Ѿ��� ��ġ�� �� �ѱ��� ��ġ�� �ٲ۴�.
            for (int i = 0; i < Muzzles.Length; i++)
            {
                // 1. �Ѿ��� �����
                GameObject bullet = Instantiate(BulletPrefab);
                // 2. ��ġ�� �����Ѵ�.
                bullet.transform.position = Muzzles[i].transform.position;
            }

            for (int i = 0; i < SubMuzzles.Length; i++)
            {
                // 1. �Ѿ��� �����
                GameObject subBullet = Instantiate(SubBulletPrefab);

                // 2. ��ġ�� �����Ѵ�.
                subBullet.transform.position = SubMuzzles[i].transform.position;
            }
        }
    }

}
