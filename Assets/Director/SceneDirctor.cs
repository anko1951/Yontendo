using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirctor : MonoBehaviour
{
    [SerializeField]
    string NextSceneName;
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
    }

    public void ChangeScene(){
        SceneManager.LoadScene(NextSceneName);
    }
}
