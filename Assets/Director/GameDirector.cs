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

    // Start is called before the first frame update
    void Start()
    {
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
