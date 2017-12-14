using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeConnect : MonoBehaviour {

    ObjectController pullObjectScript;

    private GameObject pullObject;
    private GameObject pullPlayer;
    private Vector3 midPos;             // オブジェクトの中間地
    private Vector3 surfaceVec;

    float distX;                        // オブジェクトの伸縮値

	void Start () {
        pullPlayer = transform.parent.gameObject;
	}
	
	void Update () {

        pullObjectScript = GameObject.Find("MissionManager/Transportation/MissionObject/Object").GetComponent<ObjectController>();

        // 引くオブジェクトとプレイヤーが紐づけされたか判断
        if (pullObjectScript.player == null)
        {
            GetComponent<Renderer>().enabled = false;
            return;
        }
        else
        {
            GetComponent<Renderer>().enabled = true; 
        }

        // 引くオブジェクトの取得
        pullObject = pullObjectScript.gameObject;


        // オブジェクトの中間点を求めて配置
        midPos = (pullObject.transform.position - pullPlayer.transform.position) / 2.0f;
        surfaceVec = midPos;
        transform.position = pullPlayer.transform.position + midPos;

        // オブジェクト間の距離を求めてスケールの調整
        distX = Vector3.Distance(pullObject.transform.position, pullPlayer.transform.position);
        transform.localScale = new Vector3(0.1f, 1.0f, distX / 10.0f);

        // レンダラー呼び出しでtiling調整
        Renderer render = GetComponent<Renderer>();
        render.material.mainTextureScale = new Vector2(1.0f, distX);


        // オブジェクト間をつなぐように回転
        transform.LookAt(pullObject.transform.position);

        // 地面に埋まらないように調整
        transform.localPosition += transform.up * 0.50f;
    }

    public void SetObject( GameObject transportObject )
    {
        pullObject = transportObject;
    }
}
