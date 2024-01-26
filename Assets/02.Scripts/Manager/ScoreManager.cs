using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{   //역할 :  점수를 관리한다
    //목표 : 적을 잡을 때 마다 점수를 올리고, 현재 점수를 UI에 표시하고 싶다
    //필요 속성
    // - 현재 점수를 표시할 UI
    public Text ScoreTextUI;            //UnityEngine.UI에서 가져온 Text 클래스이기 때문에 참조를 해줘야한다
    public Text BestScoreTextUI;            //UnityEngine.UI에서 가져온 Text 클래스이기 때문에 참조를 해줘야한다

    // - 현재 점수를 기억할 변수
    public int Score = 0;
    public int BestScore = 0;

    // 목표 : 게임을 시작할 때 최고점수를 불러오고, UI에 표시하고 싶다
    // 구현 순서:
    // 1. 게임을 시작할 때
    private void Start()
    {
        // 2. 최고 점수를 불러온다
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        // 3. UI에 표시한다
        BestScoreTextUI.text = $"최고점수 : {BestScore}";
    }






}
