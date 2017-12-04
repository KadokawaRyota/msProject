using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlide : MonoBehaviour {

    public float timeRate = 2.0f;

    private Vector3 destCameraPos;
    private bool move = false;
    private float startTime;
    private Vector3 startPosition;

    void Start() {
        destCameraPos = transform.position;
        startPosition = transform.position;
    }

    void Update() {

        var diff = Time.timeSinceLevelLoad - startTime;
        var rate = diff / timeRate;

        if (move)
        {
            if (rate > 1)
            {
                transform.position = destCameraPos;
                move = false;
                return;
            }
            transform.position = Vector3.Lerp(startPosition, destCameraPos, rate);
        }

    }

    public void OnClickButton( string dir )
    {
        if (!move)
        {
            if( dir == "right" )
            {
                // 右にスライド
                destCameraPos = new Vector3(10.0f, 0.0f, 0.0f) + transform.position;
                startTime = Time.timeSinceLevelLoad;
                startPosition = transform.position;
                move = true;
            }
            else if( dir == "left" )
            {
                // 左にスライド
                destCameraPos = new Vector3(-10.0f, 0.0f, 0.0f) + transform.position;
                startTime = Time.timeSinceLevelLoad;
                startPosition = transform.position;
                move = true;
            }
        }
    }
}
