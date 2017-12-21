using System.Collections;
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
        if(isServer == true)
        syncPos = this.transform.localPosition;
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

		if(isServer == true)
		{
            //サーバーで補間。
			myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
            //送りたい情報を格納
            syncPos = myTransform.position;
        }
        else
        {
            //サーバーから送られてきた値を格納
            myTransform.position = syncPos;
        }
    }


    //クライアントからホストへ、Position情報を送る
    [ClientRpc]
    void RpcProvidePositionToServer(Vector3 pos)
    {
        //サーバーで呼び出し、クライアント側で実行。syncPosにサーバーの値を格納する。
        syncPos = pos;
    }

    //クライアントのみ実行される
    [ServerCallback]
    //位置情報を送るメソッド
    void TransmitPosition()
    {

                RpcProvidePositionToServer(myTransform.position);

				//現在位置を最終位置として保存
				lastPos = myTransform.position;
    }
}
