using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// スコア用
/// </summary>
public class PlayerSyncScore : NetworkBehaviour {

	//使用しているプレイヤーキャラクター番号をサーバに送る
	[SyncVar,SerializeField]
	int syncUsePlayerNum;

	//獲得したスコアをサーバへ送る
	[SyncVar,SerializeField]
	int syncScore = 0;

	//サーバの総合得点をクライアントへ送信
	[SyncVar,SerializeField]
	int syncServerScore = 0;


	CharactorInfo charaInfo;

	ServerScore serverScore;
	void Start()
	{
		//クライアント側で使用キャラを送信する為取得
		charaInfo = GameObject.Find("CharactorInfo").GetComponent<CharactorInfo>();
		if (null == charaInfo) {


			//エラーログ
			Debug.Log ("PlayerSyncScore:Not charaInfo");
		}

		//サーバ側でクライアントへ町の総合得点を送信する為取得
		serverScore = GameObject.Find ("ServerScore").GetComponent<ServerScore> ();
		if (null == serverScore) {

			//エラーログ
			Debug.Log ("PlayerSyncScore:Not serverScore");
		}

		//サーバへ使用キャラを送信
		SendUseCharaToServer ();
		//ServerScore ();
	}
	void Update()
	{
		//町の総合得点をクライアントへ送信
		ServerScore ();
		if (!isServer) {
			//localScore = syncScore;
		}
	}


	/// <summary>
	/// 総合得点の更新
	/// </summary>
	/// <param name="charaNum">Chara number.</param>
	/// <param name="score">Score.</param>
	[Command]
	void CmdProvideScoreToServer(int charaNum, int score)
	{
		//サーバーが受け取る値
		//syncScore += score;

		//サーバは受け取ったスコアを総合得点へ加算
		if (isServer) {
			serverScore.SetServerScore (charaNum, score);
		}
	}

	//クライアントのみ実行される
	[ClientCallback]
	public void TransmitScore(int score)
	{
		if (gameObject.GetComponent<NetworkIdentity>().isLocalPlayer) {
			//localScore += score;
			CmdProvideScoreToServer ((int)charaInfo.GetCharaSelectData (),score);
		}
	}

	/// <summary>
	/// 仕様キャラの町の総合得点をクライアントへ送信
	/// </summary>
	/// <param name="score">Score.</param>
	[ClientRpc]
	void RpcServerScoreToClient(int score)
	{
		syncServerScore = score;
	}

	[ServerCallback]
	void ServerScore()
	{
		RpcServerScoreToClient (serverScore.GetServerScore(syncUsePlayerNum));
	}


	/// <summary>
	/// 使用しているキャラクターをサーバへ送る
	/// </summary>
	/// <param name="num">Number.</param>
	[Command]
	void CmdUseChara(int num)
	{
		syncUsePlayerNum = num;
	}

	//ラッピング
	[ClientCallback]
	void SendUseCharaToServer()
	{
		if (gameObject.GetComponent<NetworkIdentity> ().isLocalPlayer) {
			CmdUseChara ((int)charaInfo.GetCharaSelectData ());
		}
	}

	/// <summary>
	/// 自分の町の総合得点をサーバから取得
	/// </summary>
	/// <returns>The server score.</returns>
	public int GetServerScore()
	{
		return syncServerScore;
	}
}
