using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetActiveImage : MonoBehaviour
{
    [SerializeField]
    private Image[] images; // Image配列
    [SerializeField]
    private int selectStatus;

    private PlayerData playerData;

    private void Awake()
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
    private void Start()
    {
        SetImage(selectStatus);
    }

    public void SetImage(int statusNumber){
        if (playerData == null)
        {
            FindOutPlayerData();
            return; // エラー時は処理を中断
        }

        int nowLv = 0;
        images[0].gameObject.SetActive(false);
        images[1].gameObject.SetActive(false);
        images[2].gameObject.SetActive(false);// 初期化処理
        switch (statusNumber)
        {
            case 0:
                nowLv = playerData.GetDashLv();
                break;
            case 1:
                nowLv = playerData.GetHungryDefaultLv();
                break;
            default:
                nowLv = playerData.GetJumpLv();
                break;
        }

        // 安全なインデックスを計算
        int temp = Mathf.Clamp(nowLv-1, 0, images.Length - 1);

        // 対応するImageのGameObjectをアクティブ化
        images[temp].gameObject.SetActive(true);
    }

    //強制タイトル
    private void FindOutPlayerData(){
        Debug.LogWarning("PlayerDataが見つかりませんでした！Titleへ戻ります。");
        SceneManager.LoadScene("Title");
    }
}
