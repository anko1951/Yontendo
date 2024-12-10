using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeTradeDirector : MonoBehaviour
{
    public GameData gameData;

    [SerializeField]
    UnityEngine.UI.Image hpBar;
    [SerializeField]
    float timePerHealth;
    [SerializeField]
    float eatScore;
    [SerializeField]
    TextMeshProUGUI eatScoreText; // TextMeshProの参照を設定

    private float currentVelocity; // 内部的に使われる速度
    private float times;
    private bool isCalledMethod = false;


    void Awake(){
        gameData.life -= gameData.playTime/timePerHealth ;
    }
    // Start is called before the first frame update
    void Start(){
        gameData.playTime = 0 ;
    }

    // Update is called once per frame
    void Update()
    {
        times += Time.deltaTime;

        if (gameData != null && hpBar != null)
        {
            float targetFillAmount = Mathf.Clamp01(gameData.life/gameData.lifeMax);
            // SmoothDampでFillAmountを滑らかに変化
            hpBar.fillAmount = Mathf.SmoothDamp(
                hpBar.fillAmount, targetFillAmount, ref currentVelocity, 0.2f
            );
        }

        if(gameData != null && hpBar != null && times > 5.0f){
            float cutScore = (gameData.lifeMax - gameData.life) * eatScore;
            gameData.resultScore -= (int)cutScore;
            float damage;
            gameData.life = gameData.lifeMax ;
            if(gameData.resultScore < 0){
                damage = gameData.resultScore * -1 ;
                gameData.life -= damage;
                gameData.resultScore = 0 ;
            }
            float targetFillAmount = Mathf.Clamp01(gameData.life/gameData.lifeMax);

            // SmoothDampでFillAmountを滑らかに変化
            hpBar.fillAmount = Mathf.SmoothDamp(
                hpBar.fillAmount, targetFillAmount, ref currentVelocity, 0.2f
            );
            WriteScore(cutScore);
        }

    }

    void WriteScore(float cutScore){
        if (eatScoreText != null && gameData != null && !isCalledMethod )
        {
            // PlayerDataからスコアを取得して表示
            float score = gameData.resultScore;
            eatScoreText.text = ""+(int)cutScore; // 小数点以下2桁で表示
        }

        isCalledMethod = true ;
    }
}
