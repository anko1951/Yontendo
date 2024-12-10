using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MeshProTotalDirector : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText; // TextMeshProの参照を設定

    public GameData gameData; // GameDataのインスタンス

    private void Awake(){
        gameData.playTime = 0;
        gameData.playerScore = gameData.resultScore ;
    }

    private void Start()
    {
        if (scoreText != null && gameData != null)
        {
            // PlayerDataからスコアを取得して表示
            float score = gameData.resultScore;
            scoreText.text = ""+score; // 小数点以下2桁で表示
        }
        else
        {
            Debug.LogError("scoreTextまたはplayerDataが設定されていません");
        }

        gameData.resultScore = 0 ;
    }

    //強制タイトル
    private void FindOutPlayerData(){
        Debug.LogWarning("PlayerDataが見つかりませんでした！Titleへ戻ります。");
        SceneManager.LoadScene("Title");
    }
}

