﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour {

	Vector2 startPos;
	RectTransform rect;     //開始時の位置を記憶

	public float moveSpeed;     //開閉のスピード

	bool menuOpen = false;  //メニューバーの開閉フラグ

    [SerializeField]
    float stopPosX = 500.0f;

    [SerializeField]
    Sprite openImage;

    [SerializeField]
    Sprite closeImage;

    Image img;

	void Start () {

        img = GetComponent<Image>();
		rect = GetComponent<RectTransform>();		//メニューを閉じるときの終点を保存
		startPos = GetComponent<RectTransform>().anchoredPosition;
    }
	
	// Update is called once per frame
	void Update () {

		//メニューを開く処理
		if(menuOpen)
		{
			if( rect.anchoredPosition.x < stopPosX)
			{
				rect.anchoredPosition += new Vector2(moveSpeed, 0.0f);
			}
		}
		//メニューを閉じる処理
		else
		{
			if (rect.anchoredPosition.x > startPos.x)
			{
				rect.anchoredPosition -= new Vector2(moveSpeed, 0.0f);
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

            img.sprite = openImage;
		}
		else
		{
			//オープン
			menuOpen = true;

            img.sprite = closeImage;
        }
	}

}
