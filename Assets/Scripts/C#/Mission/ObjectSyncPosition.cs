﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


//channel(SyncVar変数をUpdateする時の設定Qos参照)
//sendInterval(どれくらいの頻度でUpdateするか)
[NetworkSettings(channel = 0, sendInterval = 0.033f)]

public class ObjectSyncPosition : NetworkBehaviour
{
    [SyncVar]           //ホストから全クライアントへ送る
    Vector3 syncPos;
    
    //Positionの現在位置
    [SerializeField]
    Transform myTransform;

    //Leap:2ベクトル間を補間する
    [SerializeField]
    float lerpRate = 15;

    //前フレームの最終位置
    Vector3 lastPos;

    //threshold:しきい値、境目となる値のこと
    //0.5unitを越えなければ移動していないこととする
    float threshold = 0;

	void Start()
    {
    }

    void Update()
    {
        LerpPosition();     //2点間を補完する
    }

    void FixedUpdate()
    {
        TransmitPosition();
    }

    //ポジション補間
    void LerpPosition()
    {
		NetworkObjectController netObjCon = GetComponent<NetworkObjectController>();

		/*if(isServer == true)
		{
			myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
		}*/
		//補間対象は相手プレイヤーのみ
		//if (GetComponent<NetworkIdentity>().hasAuthority == false)
		if (netObjCon.player.GetComponent<PlayerObjectAuthority>().hasAuthority != gameObject)
		{
			//Lerp(from,to,割合) from～toのベクトル間を補完する
			myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
		}
    }


    //クライアントからホストへ、Position情報を送る
    [Command]
    void CmdProvidePositionToServer(Vector3 pos)
    {
        //サーバーが受け取る値
        syncPos = pos;
    }

    //クライアントのみ実行される
    [ClientCallback]
    //位置情報を送るメソッド
    void TransmitPosition()
    {
		NetworkObjectController netObjCon = GetComponent<NetworkObjectController>();

		if (netObjCon.player != null)
		{
			/***ネットワークトラフィックの軽減処理***/
			//自プレイヤーであり、現在位置と前フレームのい最終位置との距離がthresholdより大きいとき
			//if (GetComponent<NetworkIdentity>().hasAuthority == true && Vector3.Distance(myTransform.position, lastPos) > threshold)
			if (netObjCon.player.GetComponent<PlayerObjectAuthority>().hasAuthority == gameObject && Vector3.Distance(myTransform.position, lastPos) > threshold)
			{
				CmdProvidePositionToServer(myTransform.position);

				//現在位置を最終位置として保存
				lastPos = myTransform.position;
			}
		}
    }
}
