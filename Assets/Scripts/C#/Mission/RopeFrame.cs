using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeFrame : MonoBehaviour {

    bool enable;
    private Renderer render;
    private float offset;

    public float scrollSpeed = 0.01f;


    void Start () {
        render = GetComponent<Renderer>();
        GetComponent<Renderer>().enabled = false;
    }
	
	void Update () {

        if (!enable) return;

        // レンダラー呼び出しでoffset調整
        offset = scrollSpeed;
        render.material.mainTextureOffset += new Vector2(scrollSpeed, 0);
    }

    public void SetEnable( bool bEnable )
    {
        enable = bEnable;
        GetComponent<Renderer>().enabled = bEnable;
    }
}
