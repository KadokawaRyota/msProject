using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeFrame : MonoBehaviour {

    ObjectController pullObjectScript;
    private GameObject ropeObject;
    private Renderer render;
    private float offset;

    public float scrollSpeed = 0.01f;


    void Start () {
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

        // レンダラー呼び出しでoffset調整
        offset = scrollSpeed;
        Renderer render = GetComponent<Renderer>();
        render.material.mainTextureOffset += new Vector2(scrollSpeed, 0);
    }
}
