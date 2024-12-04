using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDirector : MonoBehaviour
{
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
        if(playerData != null)
        {
            playerData.FullCharge();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
