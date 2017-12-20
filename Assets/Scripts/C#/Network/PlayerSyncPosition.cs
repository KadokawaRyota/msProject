using UnityEngine;
using UnityEngine.Networking;

//channel(SyncVar変数をUpdateする時の設定Qos参照)
//sendInterval(どれくらいの頻度でUpdateするか)
[NetworkSettings(channel = 0, sendInterval = 0.033f)]

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
    [SerializeField]
    float threshold = 0.5f;

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
        if(isLocalPlayer && Vector3.Distance(myTransform.position,lastPos) > threshold)
        {
            CmdProvidePositionToServer(myTransform.position);

            //現在位置を最終位置として保存
            lastPos = myTransform.position;
        }
		/*if(isLocalPlayer)
		{
			CmdProvidePositionToServer(myTransform.position);
		}*/
	}

}
