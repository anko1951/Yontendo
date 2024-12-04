using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetActiveImage : MonoBehaviour
{
    [SerializeField]
    private Image[] images; // Image配列
    [SerializeField]
    private int statusNumber;

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
            Debug.LogWarning("PlayerDataオブジェクトが見つかりませんでした！");
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (playerData == null)
        {
            Debug.LogError("PlayerDataが設定されていません。処理を中断します。");
            return; // エラー時は処理を中断
        }

        int nowLv = 0;
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
}
