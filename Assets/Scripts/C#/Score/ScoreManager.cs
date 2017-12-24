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

	[SyncVar,SerializeField]
	int syncServerScore = 0;

	[SerializeField]
	int localScore = 0;

	[SerializeField]
	int localServerScore = 0;

	[SerializeField]
	CharactorInfo charaInfo;

	void Start()
	{
		//netConnector = gameObject.GetComponent<PlayerNetworkSetup> ().GetNetConnector ();
		charaInfo = GameObject.Find("CharactorInfo").GetComponent<CharactorInfo>();
		ServerScore ();
		localServerScore = syncServerScore;
	}
	void Update()
	{
		if (!gameObject.GetComponent<NetworkIdentity>().isLocalPlayer) {
			localScore = syncScore;
		}
	}

	//クライアントからホストへ、Position情報を送る
	[Command]
	void CmdProvideScoreToServer(int charaNum, int score)
	{
		//サーバーが受け取る値
		syncScore += score;
		if (isServer) {
			GameObject.Find ("ScoreManager").GetComponent<ServerScore> ().SetServerScore (charaNum, score);
		}
	}

	//クライアントのみ実行される
	[ClientCallback]
	//位置情報を送るメソッド
	public void TransmitScore(int score)
	{
		if (gameObject.GetComponent<NetworkIdentity>().isLocalPlayer) {
			localScore += score;
			CmdProvideScoreToServer ((int)charaInfo.GetCharaSelectData(),score);
		}
	}

	[ClientRpc]
	void RpcServerScoreToClient()
	{
		syncServerScore = GameObject.Find ("ScoreManager").GetComponent<ServerScore> ().GetServerScore((int)charaInfo.GetCharaSelectData());
	}

	[ServerCallback]
	void ServerScore()
	{
		RpcServerScoreToClient ();
	}
}
