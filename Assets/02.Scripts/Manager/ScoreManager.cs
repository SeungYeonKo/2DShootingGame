using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{  /*
    디자인패턴 : 옛날부터 소프트웨어 개발 과정에서 발견된 설계 노하우에 이름을 붙여 재사용하기 좋은 형태로 묶어 정리한 것
        장점 : 서로 같은 패턴을 알고 있을 때 의사소통이 잘된다(내용,설계원칙,특성,조건 등)
            -모범사례이므로, 가독성/유지보수/확장성/신뢰성Up
        단점 : 오용과 남용
            -초기에 학습 곡선이 있다(처음 적용에 시간이 든다)
        알면 좋은 패턴 : 싱글톤, 오브젝트 풀, 상태, 옵저버, 팩토리*/
    public Text ScoreTextUI;           
    public Text BestScoreTextUI;            

    private int _score = 0;
    public int BestScore = 0;

    //  ScoreManager가 점수를 관리하는 유일한 매니저이므로 싱글톤을 적용하는게 편하다
    public static ScoreManager Instance;  //ScoreManager 객체
    private void Awake()
    {
        Debug.Log("ScoreManager 객체의 Awake 호출");
        if(Instance == null)
        {
            Debug.Log("새로 생성된 것");
            Instance = this;
        }
        else
        {
            Debug.Log("이미 있다!!!!!!!!!!!!!!!!!!");
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        BestScoreTextUI.text = $"최고 점수 : {BestScore}";
    }

    //목표 : score속성에 대한 캡슐화 (get/set)
    public int GetScore()
    {
        return _score;
    }

    //캡슐화
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

         
            // 타입별로 저장/로드가 가능한 Set/Get 메서드가 있다
            PlayerPrefs.SetInt("BestScore", BestScore);

            // 3. UI에 표시한다
            BestScoreTextUI.text = $"최고 점수 : {BestScore}";
        }

    }
}
