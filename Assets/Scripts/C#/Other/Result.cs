using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//リザルト
public class Result : MonoBehaviour {

    [SerializeField]
    Camera resultCamera;        //リザルトカメラ

    [SerializeField]
    Canvas resultCanvas;

    [SerializeField]
    GameObject targetObject_Green;      //タヌキの村のオブジェクト

    [SerializeField]
    GameObject targetObject_Town;      //ネコの村のオブジェクト

    [SerializeField]
    GameObject targetObject_Sand;      //キツネの村のオブジェクト

    [SerializeField]
    GameObject targetObject_Ice;      //イヌの村のオブジェクト


    bool resultFlg = false;     //フラグ

    void Awake()
    {
        resultFlg = false;
    }

    // Use this for initialization
    void Start () {

        if(targetObject_Green == null)
        {
            Debug.Log("タヌキのリザルトオブジェクトの設定してー");
        }

        if (targetObject_Sand == null)
        {
            Debug.Log("キツネのリザルトオブジェクトの設定してー");
        }

        if (targetObject_Ice == null)
        {
            Debug.Log("イヌのリザルトオブジェクトの設定してー");
        }

        if (targetObject_Town == null)
        {
            Debug.Log("ネコのリザルトオブジェクトの設定してー");
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (resultFlg)
        {

        }
	}

    //リザルトスタートフラグ
    public void StartResult()
    {
        resultFlg = true;

        GameObject player = GameObject.Find("Player");

        player.SetActive(false);

        resultCamera.gameObject.SetActive(true);

        resultCanvas.gameObject.SetActive(true);
    }
}
