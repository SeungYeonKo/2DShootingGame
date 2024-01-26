using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{   //���� :  ������ �����Ѵ�
    //��ǥ : ���� ���� �� ���� ������ �ø���, ���� ������ UI�� ǥ���ϰ� �ʹ�
    //�ʿ� �Ӽ�
    // - ���� ������ ǥ���� UI
    public Text ScoreTextUI;            //UnityEngine.UI���� ������ Text Ŭ�����̱� ������ ������ ������Ѵ�
    public Text BestScoreTextUI;            //UnityEngine.UI���� ������ Text Ŭ�����̱� ������ ������ ������Ѵ�

    // - ���� ������ ����� ����
    private int _score = 0;
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

    //목표 : score속성에 대한 캡슐화 (get/set)
    public int GetScore()
    {
        return _score;
    }
    public void SetScore(int score)
    {
        //유효성 검사
        if(score < 0)
        {
            return;
        }
        _score = score;

        // 목표 : Score를 화면에 표시한다
        ScoreTextUI.text = $"점수 : {_score}";

        //목표 : 최고 점수를 갱신하고  UI 에 표시하고 싶다
        // 1. 만약 현재 점수가 최고 점수보다 크다면
        if (_score > BestScore)
        {
            // 2. 최고 점수를 갱신하고,
            BestScore = _score;

            // 목표 : 최고 점수를 저장
            // PlayerPrefs 클래스를 사용
            // -> 값을 키(key)와 값(value) 형태로 저장하는 클래스
            // 저장할 수 있는 데이터타입 : int, float, string
            // 타입별로 저장/로드가 가능한 Set/Get 메서드가 있다
            PlayerPrefs.SetInt("BestScore", BestScore);

            // 3. UI에 표시한다
            BestScoreTextUI.text = $"최고 점수 : {BestScore}";
        }

    }
}
