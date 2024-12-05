using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BoardDirector : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Image[] pointers;
    [SerializeField]
    UnityEngine.UI.Image[] boards;

    [SerializeField]
    string beforeSceneName;
    [SerializeField]
    float intarval;// 操作間隔
    [SerializeField]
    GameObject[] imageSetter;


    private PlayerData playerData;
    private int selectNow = 0;
    private int showBoard = 0;
    private int needUp = 0;
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
            int[] maxNum = {2,4,pointers.Length-1} ;
            int[] minNum = {0,3,5} ;
            pointers[selectNow].gameObject.SetActive(false);
            float temp = Input.GetAxis("Vertical");// W/S または ↑/↓
            selectNow += (int)Mathf.Sign(temp) * -1 ;// 値を 1 または -1 に変換する
            selectNow = Mathf.Clamp(selectNow,minNum[showBoard],maxNum[showBoard]);
            }
            pointers[selectNow].gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            if(selectNow == 5)
            {
                switch(needUp){
                    case 0:
                        playerData.LvUpDash();
                        Debug.Log(playerData.GetDashLv()*100);
                        imageSetter[needUp].GetComponent<SetActiveImage>().SetImage(needUp);
                        break;
                    case 1:
                        playerData.LvUpHungryDefault();
                        imageSetter[needUp].GetComponent<SetActiveImage>().SetImage(needUp);
                        break;
                    case 2:
                        playerData.LvUpJump();
                        imageSetter[needUp].GetComponent<SetActiveImage>().SetImage(needUp);
                        break;
                    default: break;
                }
                DefaultBoard();
            }
            else if(selectNow == 4)// No選択
            {
                DefaultBoard();
            }
            else if(selectNow == 3)
            {
                showBoard++;
                showBoard = Mathf.Clamp(showBoard,0,boards.Length);
                boards[showBoard].gameObject.SetActive(true);
                StepSelector(5);
            }
            else
            {
                needUp = selectNow ;
                showBoard++;
                showBoard = Mathf.Clamp(showBoard,0,boards.Length);
                boards[showBoard].gameObject.SetActive(true);
                StepSelector(3);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Z) && showBoard != 2){
            ReChangeScene();
        }
    }

    //デフォルト画面に戻す処理
    void DefaultBoard(){
        needUp = 0;
        boards[2].gameObject.SetActive(false);
        boards[1].gameObject.SetActive(false);
        showBoard = 0;
        StepSelector(0);
        selectNow = 0;
    }

    //受け取った値でカーソルを動かすやつ
    void StepSelector(int step){
        pointers[selectNow].gameObject.SetActive(false);
        selectNow = step ;
        pointers[step].gameObject.SetActive(true);
    }
    
    //一つ前のシーンへ
    void ReChangeScene(){
        SceneManager.LoadScene(beforeSceneName);
    }

}
