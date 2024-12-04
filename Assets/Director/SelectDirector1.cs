using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectDirector1 : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Image[] pointers;
    [SerializeField]
    UnityEngine.UI.Image[] boards;

    [SerializeField]
    string beforeSceneName;
    [SerializeField]
    float intarval;// 操作間隔


    private PlayerData playerData;
    private int selectNow = 0;
    private int showBoard = 0;
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
            if(Input.GetAxis("Vertical") != 0){
            timer = 0;
            pointers[selectNow].gameObject.SetActive(false);
            float temp = Input.GetAxis("Vertical");// W/S または ↑/↓
            selectNow += (int)Mathf.Sign(temp);// 値を 1 または -1 に変換する
            selectNow = Mathf.Clamp(selectNow,0,pointers.Length-1);
            }
            pointers[selectNow].gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            if(showBoard != 2)
            {
                showBoard++;
                showBoard = Mathf.Clamp(showBoard,0,boards.Length);
            }
            else
            {
                showBoard = 0;
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Z)){
            ReChangeScene();
        }
    }

    //一つ前のシーンへ
    void ReChangeScene(){
        SceneManager.LoadScene(beforeSceneName);
    }

}
