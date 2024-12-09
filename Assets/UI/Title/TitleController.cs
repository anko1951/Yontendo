using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    GameObject sceneDirector;
    public void OnStartButtonClicked()
    {
        sceneDirector.SendMessage("ChangeScene");
    }
}
