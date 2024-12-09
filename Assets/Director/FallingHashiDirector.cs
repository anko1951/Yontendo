using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FallingHashiDirector : MonoBehaviour
{
    private GameObject[] sakus;
    private GameObject[] hashes;

    [SerializeField] 
    GameObject Boss;
    [SerializeField]
    GameObject[] Konoe;

    private float time ;
    private float setTime = 0.5f;
    private bool isDelete = false ;
    private bool isSakuDeleted = false;

    private void Awake() {
        // タグ "Saku" を持つすべてのオブジェクトを取得
        sakus = GameObject.FindGameObjectsWithTag("Saku");

        // タグ "Hashi" を持つすべてのオブジェクトを取得
        hashes = GameObject.FindGameObjectsWithTag("Hashi");
    }

    private void Update() {
        time += Time.deltaTime;
        
        if(time > setTime * 12.0f && isDelete && !isSakuDeleted){
            foreach (GameObject saku in sakus){
                Destroy(saku);
            }
            isSakuDeleted = true ;
        }

        if(time > setTime * 15.0f && isDelete){
            foreach (GameObject hashi in hashes){
                Destroy(hashi);
            }
            CallIsBattle();
            Destroy(gameObject);
        }

        if(time > setTime && isDelete && !isSakuDeleted){
            
            foreach (GameObject saku in sakus){
                // Rigidbodyコンポーネントを取得
                BoxCollider bc = saku.GetComponent<BoxCollider>();

                if (bc != null)
                {
                    // あったらそのまま消す
                    Destroy(bc);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        // Playerが接触したかどうか
        if (other.CompareTag("Player"))
        {
            foreach (GameObject hashi in hashes)
            {
                // Rigidbodyコンポーネントを取得
                Rigidbody rb = hashi.GetComponent<Rigidbody>();

                // Rigidbodyが存在すればisKinematicをfalseに設定
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
            }
            time = 0 ;
            isDelete = true ;
            Destroy(gameObject.GetComponent<BoxCollider>());
        }
    }

    void CallIsBattle(){
        BossController bossC = Boss.GetComponent<BossController>();
        bossC.SetIsBattleStart();
        for(int i = 0 ; i < Konoe.Length ; i++ ){
            KonoeController konoeController = Konoe[i].GetComponent<KonoeController>();
            konoeController.SetIsBattleStart();
        }
    }
}

