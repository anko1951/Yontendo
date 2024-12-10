using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHoleController : MonoBehaviour
{
    private GameObject gameDirector;
    private GameObject dropHoleDirector;
    // Start is called before the first frame update
    void Awake(){
        gameDirector = GameObject.FindWithTag("GameDirector");
        dropHoleDirector = GameObject.FindWithTag("HoleDirector");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        PlayerController pCl = other.gameObject.GetComponent<PlayerController>();
        EnemyController eCl = other.gameObject.GetComponent<EnemyController>();
        if(pCl != null){
            Destroy(other.gameObject);
            gameDirector.SendMessage("PlayerDiedScene");
        }
        if(eCl != null){
            dropHoleDirector.GetComponent<DropHoleDirector>().CountEnemy();
            Destroy(other.gameObject);
        }
    }
}
