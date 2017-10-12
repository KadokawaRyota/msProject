using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSyncPosition : NetworkBehaviour {

	[SyncVar]			//ホストから全クライアントへ送る

	Vector3 syncPos;

	//Playerの現在位置
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

	void FixedUpdate()
	{
		TransmitPosition();
		LerpPosition();		//2点間を補完する
	}

	//ポジション補間
	void LerpPosition()
	{
		//補間対象は相手プレイヤーのみ
		if(!isLocalPlayer)
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
        /***ネットワークトラフィックの軽減処理***/
        //自プレイヤーであり、現在位置と前フレームのい最終位置との距離がthresholdより大きいとき
        /*if(isLocalPlayer && Vector3.Distance(myTransform.position,lastPos) > threshold)
        {
            //終了
            CmdProvidePositionToServer(myTransform.position);

            //開始
            //現在位置を最終位置として保存
            lastPos = myTransform.position;
        }*/
		if(isLocalPlayer)
		{
			CmdProvidePositionToServer(myTransform.position);
		}
	}
}
