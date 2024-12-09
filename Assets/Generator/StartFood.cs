using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFood : MonoBehaviour
{
    [SerializeField]
    GameObject[] popObjects;

    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update(){}

    private void OnTriggerEnter(Collider other) {
        PlayerController plC = other.gameObject.GetComponent<PlayerController>();
        if(plC != null){
            for(int i = 0 ; i < popObjects.Length ; i++ ){
                popObjects[i].SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
