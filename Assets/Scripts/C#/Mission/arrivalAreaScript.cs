using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrivalAreaScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        //オブジェクトに自分が紐付いていたら((プレイヤーを判別する要素が欲しい））
        if( collider.GetComponent<ObjectController>().player != null )
        {
            collider.GetComponent<ObjectController>().Refresh();
            //加点する
        }
    }
}
