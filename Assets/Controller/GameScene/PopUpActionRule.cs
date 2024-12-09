using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpActionRule : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Image image;

    Vector3 firstPosition ;
    Vector3 secondPosition ;
    bool isPopUp = true;

    // Start is called before the first frame update
    void Awake(){
        firstPosition = image.gameObject.transform.position;
        secondPosition = new Vector3(image.gameObject.transform.position.x,-220,image.gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            isPopUp = !isPopUp;
        }

        if(isPopUp){
            image.transform.position = firstPosition;
        }
        else
        {
            image.transform.position = secondPosition;
        }
    }
}
