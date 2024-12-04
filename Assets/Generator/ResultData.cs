using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultData : MonoBehaviour
{
    public GameData gameData;

    int resultScore;
    int killCount;
    bool isDied;
    float playTime;
    float life;
    float lifeMax; 
    // Start is called before the first frame update
    void Start()
    {
        resultScore = gameData.resultScore;
        playTime = gameData.playTime;
        life = gameData.life;
        lifeMax = gameData.lifeMax;
    }

    // Update is called once per frame
    void Update(){}
}
