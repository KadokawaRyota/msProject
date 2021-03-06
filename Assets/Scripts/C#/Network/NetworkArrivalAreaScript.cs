﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class NetworkArrivalAreaScript : NetworkBehaviour 
{
    // Use this for initialization
    GameObject parent;


    GameObject player;

    void Start()
    {
        parent = GameObject.Find("NetworkMissionManager/NetworkTransportation/ArrivalAreas");
        player = GameObject.Find("Player");

        transform.parent = parent.transform;

		if(isServer)
			NetworkServer.Spawn(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Server]
    void OnTriggerEnter(Collider collider)
    {
        /*オブジェクトを綺麗に確実に他プレイヤーとの紐付けを切る手順
        1:サーバー側でオブジェクトの判定を取り、全プレイヤーにオブジェクトがゴールエリアに入った事を通知する。
        2：ゴールエリアに入ってるかどうかをローカルのプレイヤー側で判別後、プレイヤー側のオブジェクトとの紐付けを解除する。
        2.5：ここでプレイヤーに加点する。
        3：サーバー側でリストに入ってる全てのプレイヤーが紐付けを切った事を確認し、サーバー側のリストを解放する。
        4：サーバー側でエリアに対応した街に加点する。*/

        if (collider.gameObject.tag == "transportObject" && isServer )
        {
            //サーバー側のオブジェクトがゴールしたことを知らせる。
            collider.GetComponent<serverObjectController>().SetGoal(true);
        }
    }
}
