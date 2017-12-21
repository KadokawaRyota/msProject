using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeConnect : MonoBehaviour {

    ObjectController pullObjectScript;

    private GameObject pullObject;
    private GameObject pullPlayer;
    private Vector3 playerPos;
    private Vector3 pullObjectPos;
    private Vector3 midPos;             // オブジェクトの中間地
    private Vector3 surfaceVec;

    float distX;                        // オブジェクトの伸縮値

	void Start () {
        pullPlayer = GameObject.FindWithTag("body"); ;
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

        // プレイヤー体のタグ判別で中心位置の取得
        playerPos = pullPlayer.transform.position;

        // 引くオブジェクトの取得
        pullObject = pullObjectScript.gameObject;
        pullObjectPos = pullObject.GetComponent<Renderer>().bounds.center;

        // オブジェクトの中間点を求めて配置
        midPos = (pullObjectPos - playerPos) / 2.0f;
        surfaceVec = midPos;
        transform.position = playerPos + midPos;

        // オブジェクト間の距離を求めてスケールの調整
        distX = Vector3.Distance(pullObjectPos, playerPos);
        transform.localScale = new Vector3(0.1f, 1.0f, distX / 10.0f);

        // レンダラー呼び出しでtiling調整
        Renderer render = GetComponent<Renderer>();
        render.material.mainTextureScale = new Vector2(1.0f, distX);


        // オブジェクト間をつなぐように回転
        transform.LookAt(pullObjectPos);

    }

    public void SetObject( GameObject transportObject )
    {
        pullObject = transportObject;
    }
}
