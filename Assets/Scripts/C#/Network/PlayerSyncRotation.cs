﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSyncRotation : NetworkBehaviour {

	//SyncVar: ホストサーバーからクライアントへ送られる
	//プレイヤーの角度
	[SyncVar]
	Quaternion syncPlayerRotation;

	//FirstPersonCharacterのカメラの角度
	[SyncVar]
	Quaternion syncCamRotation;

	[SerializeField]
	Transform playerTransform;

	[SerializeField]
	Transform camTransform;

	[SerializeField]
	float lerpRate = 15;

    //前フレームの最終角度
    Quaternion lastPlayerRot;
    Quaternion lastCamRot;

    //しきい値は5。5以上動いたときのみメソッドを実行
    float threshold = 0;

	void Update()
	{
		//現在角度と取得した角度を補間する
		LerpRotations();
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		//クライアント側のPlayerの角度を取得
		TransmitRotations();
		
	}

	//角度を補間するメソッド
	void LerpRotations()
	{
		//自プレイヤー以外のPlayerの時
		if (!isLocalPlayer)
		{
			//プレイヤーの角度とカメラの角度を補間
			playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation,
				syncPlayerRotation, Time.deltaTime * lerpRate);
			camTransform.rotation = Quaternion.Lerp(camTransform.rotation,
				syncCamRotation, Time.deltaTime * lerpRate);
		}
	}

	//クライアントからホストへ送られる
	[Command]
	void CmdProvideRotationsToServer(Quaternion playerRot, Quaternion camRot)
	{
		syncPlayerRotation = playerRot;
		syncCamRotation = camRot;
	}

	//クライアント側だけが実行できるメソッド
	[Client]
	void TransmitRotations()
	{
		if (isLocalPlayer)
		{
            //CmdProvideRotationsToServer(playerTransform.rotation, camTransform.rotation);

            /***ネットワークトラフィックの軽減処理***/
            if (Quaternion.Angle(playerTransform.rotation,lastPlayerRot) > threshold
            || Quaternion.Angle(camTransform.rotation,lastCamRot) > threshold)
            {
                CmdProvideRotationsToServer(playerTransform.rotation, camTransform.rotation);

                lastPlayerRot = playerTransform.rotation;
                lastCamRot = camTransform.rotation;
            }
		}
	}
}
