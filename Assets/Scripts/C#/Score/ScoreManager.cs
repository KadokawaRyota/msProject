using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// スコア用
/// </summary>
public class ScoreManager : NetworkBehaviour {

	[SyncVar,SerializeField]
	int syncUsePlayerNum;

	[SyncVar,SerializeField]
	int syncScore = 0;

	[SyncVar,SerializeField]
	int syncServerScore = 0;

	[SerializeField]
	int localScore = 0;

	[SerializeField]
	int localServerScore = 0;

	[SerializeField]
	PlayerNetworkSetup pNetSet;

	CharactorInfo charaInfo;

	void Start()
	{
		//netConnector = gameObject.GetComponent<PlayerNetworkSetup> ().GetNetConnector ();
		charaInfo = GameObject.Find("CharactorInfo").GetComponent<CharactorInfo>();
		pNetSet = GetComponent<PlayerNetworkSetup>();
		SendUseCharaToServer ();
		ServerScore ();
		localServerScore = syncServerScore;
	}
	void Update()
	{
		//ServerScore ();
		if (!isServer) {
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
			CmdProvideScoreToServer ((int)pNetSet.GetUseChara(),score);
		}
	}

	[ClientRpc]
	void RpcServerScoreToClient(int score)
	{
		syncServerScore = score;
	}

	[ServerCallback]
	void ServerScore()
	{
		RpcServerScoreToClient (GameObject.Find ("ScoreManager").GetComponent<ServerScore> ().GetServerScore((int)pNetSet.GetUseChara()));
	}


	[Command]
	void CmdUseChara(int num)
	{
		syncUsePlayerNum = num;
	}

	[ClientCallback]
	void SendUseCharaToServer()
	{
		if (gameObject.GetComponent<NetworkIdentity> ().isLocalPlayer) {
			CmdUseChara ((int)charaInfo.GetCharaSelectData ());
		}
	}

	
}
