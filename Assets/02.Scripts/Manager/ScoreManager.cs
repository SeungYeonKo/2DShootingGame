using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{   //���� :  ������ �����Ѵ�
    //��ǥ : ���� ���� �� ���� ������ �ø���, ���� ������ UI�� ǥ���ϰ� �ʹ�
    //�ʿ� �Ӽ�
    // - ���� ������ ǥ���� UI
    public Text ScoreTextUI;            //UnityEngine.UI���� ������ Text Ŭ�����̱� ������ ������ ������Ѵ�
    public Text BestScoreTextUI;            //UnityEngine.UI���� ������ Text Ŭ�����̱� ������ ������ ������Ѵ�

    // - ���� ������ ����� ����
    public int Score = 0;
    public int BestScore = 0;

    // ��ǥ : ������ ������ �� �ְ������� �ҷ�����, UI�� ǥ���ϰ� �ʹ�
    // ���� ����:
    // 1. ������ ������ ��
    private void Start()
    {
        // 2. �ְ� ������ �ҷ��´�
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        // 3. UI�� ǥ���Ѵ�
        BestScoreTextUI.text = $"�ְ����� : {BestScore}";
    }






}
