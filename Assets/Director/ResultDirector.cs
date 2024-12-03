using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultDirector : MonoBehaviour
{
    public GameData gameData;
    public TextMeshProUGUI scoreText; // Textオブジェクトをアタッチする
    private int score;        // スコアの初期値

    void Awake(){
     score = gameData.resultScore ;
    }

    void Start(){
        UpdateScoreText();
    }


    
    // スコアを更新するメソッド
    public void AddScore(int amount)
    {
        score += amount; // スコアを加算
        UpdateScoreText(); // 表示を更新
    }

    // スコアを画面に反映する
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    // デバッグ用（テスト用）
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // スペースキーでスコア加算
        {
            AddScore(10);
        }
    }
}
