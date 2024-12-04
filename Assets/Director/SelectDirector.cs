using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectDirector : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Image[] images;
    [SerializeField]
    string[] stages;
    [SerializeField]
    string beforeSceneName;
    [SerializeField]
    float intarval;// 操作間隔


    private PlayerData playerData;
    private int selectNow = 0;
    private float timer;

    void Awake()
    {
        GameObject playerDataObj = GameObject.FindWithTag("PlayerData"); // PlayerDataのタグを持つオブジェクトを取得
        if (playerDataObj != null)
        {
            playerData = playerDataObj.GetComponent<PlayerData>(); // PlayerDataスクリプトを取得
        }
    }

    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= intarval)
        {
            if(Input.GetAxis("Horizontal") != 0){
            timer = 0;
            images[selectNow].gameObject.SetActive(false);
            float temp = Input.GetAxis("Horizontal");// A/D または ←/→
            selectNow += (int)Mathf.Sign(temp);// 値を 1 または -1 に変換する
            selectNow = Mathf.Clamp(selectNow,0,images.Length-1);
            }
            images[selectNow].gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            ChangeScene();
        }

        if(Input.GetKeyDown(KeyCode.Z)){
            ReChangeScene();
        }
    }

    //選択したシーンへ
    void ChangeScene(){
        if(playerData != null)
        {
            playerData.FullCharge();
        }
        SceneManager.LoadScene(stages[selectNow]);
    }

    //一つ前のシーンへ
    void ReChangeScene(){
        SceneManager.LoadScene(beforeSceneName);
    }

}
