using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour {

	ButtonExecution button;		//メニューボタンスクリプト用

	float startPos;     //開始時の位置を記憶

	public float moveSpeed;     //開閉のスピード

	bool menuOpen = false;  //メニューバーの開閉フラグ

	void Start () {
		startPos = gameObject.transform.localPosition.x;		//メニューを閉じるときの終点を保存
    }
	
	// Update is called once per frame
	void Update () {

		//メニューを開く処理
		if(menuOpen)
		{
			if(gameObject.transform.localPosition.x < Screen.width * 0.4f)
			{
				gameObject.transform.localPosition += new Vector3(moveSpeed, 0.0f,0.0f);
			}
		}
		//メニューを閉じる処理
		else
		{
			if (gameObject.transform.localPosition.x > startPos)
			{
				gameObject.transform.localPosition -= new Vector3(moveSpeed, 0.0f, 0.0f);
			}
		}

	}

	//メニューバーの処理
	public void ChangeMenuBar()
	{
		if (menuOpen)
		{
			//クローズ
			menuOpen = false;
		}
		else
		{
			//オープン
			menuOpen = true;
		}
	}

}
