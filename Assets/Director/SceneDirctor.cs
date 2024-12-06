using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirctor : MonoBehaviour
{
    [SerializeField]
    string NextSceneName;
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
            ChangeScene();
        }

        if(Input.GetKeyDown(KeyCode.Z)){
            ReChangeScene();
        }
    }

    public void ChangeScene(){
        SceneManager.LoadScene(NextSceneName);
    }

    public void ReChangeScene(){
        SceneManager.LoadScene(beforeSceneName);
    }
    
}
