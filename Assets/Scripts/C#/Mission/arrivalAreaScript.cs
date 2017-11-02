using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrivalAreaScript : MonoBehaviour {
    [SerializeField]
    GameObject Mission;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        //オブジェクトに自分が紐付いていたら
        if( collider.GetComponent<ObjectController>().player.name == "OfflinePlayer_Tanuki")
        {
            collider.GetComponent<ObjectController>().Refresh();
            //加点する
        }
    }
}
