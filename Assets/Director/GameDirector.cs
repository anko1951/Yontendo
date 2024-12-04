using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    GameObject[] foods;
    [SerializeField]
    GameObject[] enemys;
    [SerializeField]
    string resultSceneName;

    public GameData gameData;
    private PlayerData playerData;

    void Awake()
    {
        GameObject playerDataObj = GameObject.FindWithTag("PlayerData"); // PlayerDataのタグを持つオブジェクトを取得
        if (playerDataObj != null)
        {
            playerData = playerDataObj.GetComponent<PlayerData>(); // PlayerDataスクリプトを取得
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        playerData.SetIsDead(false);
        playerData.EatCharge(1);
        gameData.isDied = false;
        gameData.resultScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDiedScene(){
        gameData.isDied = true;
        gameData.deathCount++;
        SceneManager.LoadScene(resultSceneName);
    }

    public void AreaClearScene(){
        gameData.isDied = false;
        SceneManager.LoadScene(resultSceneName);
    }

}
