using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeConnect : MonoBehaviour {


    ObjectController pullObjectScript;

    private GameObject pullObject;
    private GameObject pullPlayer;      //腰のオブジェクトに限る。
    private Vector3 playerPos;
    private Vector3 pullObjectPos;
    private Vector3 midPos;             // オブジェクトの中間地
    private Vector3 surfaceVec;

    float distX;                        // オブジェクトの伸縮値

    GameObject ropeFrame;

	AudioManager audioManager;

	void Start () {
        this.GetComponent<Renderer>().enabled = false;
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
    }
	
	void Update () {

        if (pullObject == null) return;

        // プレイヤー体のタグ判別で中心位置の取得
        playerPos = pullPlayer.transform.position;

        // オブジェクトの中間点を求めて配置
        midPos = (pullObject.transform.position - playerPos) / 2.0f;
        surfaceVec = midPos;
        transform.position = playerPos + midPos;

        // オブジェクト間の距離を求めてスケールの調整
        distX = Vector3.Distance(pullObject.transform.position, playerPos);
        transform.localScale = new Vector3(0.1f, 1.0f, distX / 10.0f);

        // レンダラー呼び出しでtiling調整
        Renderer render = GetComponent<Renderer>();
        render.material.mainTextureScale = new Vector2(1.0f, distX);


        // オブジェクト間をつなぐように回転
        transform.LookAt(pullObject.transform.position);

    }

    public void SetWaist( GameObject PlayerWaist )
    {
        pullPlayer = PlayerWaist;
    }

    public void SetObject( GameObject Object )
    {
        if( Object == null )
        {
            this.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            this.GetComponent<Renderer>().enabled = true;
        }
        pullObject = Object;

		//ロープ接続SE
		audioManager.Play_SE (AudioManager.SE.SetRope);
    }
    public void SetRopeFrame(GameObject gameObject)
    {
        ropeFrame = gameObject;
    }

}
