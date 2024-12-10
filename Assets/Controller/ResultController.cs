using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    public GameData gameData;
    [SerializeField]
    string NextSceneName;
    [SerializeField]
    string ClearSceneName;
    [SerializeField]
    string BatEndSceneName;
    [SerializeField]
    string beforeSceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log(gameData.life);
            if(gameData.isClear){
                ClearScene();
            }
            else if(gameData.life <= 0){
                BatEndScene();
            }
            else{
            ChangeScene();
            }
        }

        if(Input.GetKeyDown(KeyCode.Z)){
            ReChangeScene();
        }
    }

    public void ChangeScene(){
        SceneManager.LoadScene(NextSceneName);
    }

    public void ClearScene(){
        SceneManager.LoadScene(ClearSceneName);
    }

    public void BatEndScene(){
        SceneManager.LoadScene(BatEndSceneName);
    }

    public void ReChangeScene(){
        SceneManager.LoadScene(beforeSceneName);
    }
    
}
