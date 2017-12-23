using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// スコア用
/// </summary>
public class ScoreManager : NetworkBehaviour {

	[SyncVar,SerializeField]
	int syncScore = 0;

	[SerializeField]
	int localScore = 0;

	[SerializeField]
	NetConnector netConnector;

	void Update()
	{
		TransmitScore (10);

		if (!netConnector.GetLocalPlayer()) {
			localScore = syncScore;
		}
	}

	//クライアントからホストへ、Position情報を送る
	[Command]
	void CmdProvideScoreToServer(int score)
	{
		//サーバーが受け取る値
		syncScore = score;
	}

	//クライアントのみ実行される
	[ClientCallback]
	//位置情報を送るメソッド
	void TransmitScore(int i)
	{
		if (netConnector.GetLocalPlayer()) {
			CmdProvideScoreToServer (i);
		}
	}
}
