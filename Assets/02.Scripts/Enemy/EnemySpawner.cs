using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawner : MonoBehaviour
{//���� : ���� �ð����� ���� ���������κ��� �����ؼ� �� ��ġ�� ���� ���� �ʹ�
 //�ʿ�Ӽ� :
 //-��������
 //-�����ð�
 //-����ð�
 //�������� : 
 //1. �ð��� �帣�ٰ�
 //2. ���� �ð��� ���� �ð��� �Ǹ�
 //3. ���������κ��� ���� �����Ѵ�
 //4. ������ ���� ��ġ�� �� ��ġ�� �ٲ۴�

    [Header("�� ������")]
    public GameObject EnemyPrefab;              // EnemyBasic ������
    public GameObject EnemyTargetPrefab;   // EnemyTarget ������
    public GameObject EnemyFollowPrefab;   // EnemyFollow ������

    [Header("Ÿ�̸�")]
    public float SpawnTime = 0.8f;      //���� ���� �ð�
    public float CurrentTimer = 0f;      //���� ��� �ð�
    public float Enemyrate;                  //�� ���� Ȯ��

    //��ǥ : �� ���� �ð��� �����ϰ� �ϰ� �ʹ�
    //�ʿ� �Ӽ� :
    //-�ּҽð�
    //-�ִ�ð�
    public float MinTime = 0.5f;        //�ּ� ���� �ð�
    public float MaxTime = 1.5f;       //�ִ� ���� �ð�

    private void Start()
    { //������ �� �� ���� �ð��� �����ϰ� �����Ѵ�
        SetRandomTime();
    }

    private void SetRandomTime()   //���������Լ�
    {
        SpawnTime = Random.Range(MinTime, MaxTime);     //�ּҿ� �ִ� ���� �ð� ���̿��� ������ �ð��� ����
    }

    private void SetRandomRate()   //���������Լ�
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
    {//��������
        //1.�ð��� �帣�ٰ�
        CurrentTimer += Time.deltaTime;     //����ð��� ������Ʈ
        //2.�����ð��̵Ǹ�
        if (CurrentTimer >= SpawnTime)      //�����ð��� ������
        {
            CurrentTimer = 0f;
            //���� ���� �ð��� �����ϰ� ����
            SetRandomTime();
            // ���������κ��� ���� ����
            SetRandomRate();
            //Ÿ�̸� �ʱ�ȭ
        }
    }
}