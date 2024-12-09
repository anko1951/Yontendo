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
        else
        {
            FindOutPlayerData();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        playerData.SetIsDead(false);
        gameData.isDied = false;
        gameData.resultScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameData.playTime += Time.deltaTime;
    }

    public void PlayerDiedScene(){
        gameData.isDied = true;
        gameData.deathCount++;
        playerData.EatCharge(1);
        SceneManager.LoadScene(resultSceneName);
    }

    public void AreaClearScene(){
        gameData.isDied = false;
        SceneManager.LoadScene(resultSceneName);
    }

    //強制タイトル
    private void FindOutPlayerData(){
        Debug.LogWarning("PlayerDataが見つかりませんでした！Titleへ戻ります。");
        SceneManager.LoadScene("Title");
    }

    public void BeforeScene(){
        playerData.EatCharge(1);
        SceneManager.LoadScene("OgawaHouse");
    }

}
