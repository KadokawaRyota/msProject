using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerScore : MonoBehaviour {

	//確認用
	/*[SerializeField]
	Score tanukiScore;

	[SerializeField]
	Score nekoScore;

	[SerializeField]
	Score kitsuneScore;

	[SerializeField]
	Score inuScore;*/

	[SerializeField]
	Score[] charaScore;

	//町の総合得点を配列で格納
	int[] score;

	// Use this for initialization
	void Start () {

		//キャラ種類分の配列を確保
		score = new int[(int)CharactorInfo.CHARA.MAX];
	}

	//PlayerSyncScoreから受け取った得点を総合得点に加算
	public void SetServerScore(int num, int Score)
	{
		score [num] += Score;

		//確認用
		charaScore[num].SetPlusScore(Score);
	}

	//総合得点を渡す（引数でキャラの得点を指定）
	public int GetServerScore(int charaNum)
	{
		return score[charaNum];
	}
}
