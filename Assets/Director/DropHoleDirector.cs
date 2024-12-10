using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHoleDirector : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemys;
    [SerializeField]
    GameObject gameDirector;

    private int enemyCount;


    // Start is called before the first frame update
    void Start()
    {
        enemyCount = enemys.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCount <= 0){
            gameDirector.SendMessage("AreaClearScene");
        }
    }

    public void CountEnemy(){
        enemyCount-- ;
    }
}
