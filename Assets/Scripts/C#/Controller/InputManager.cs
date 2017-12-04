﻿//------------------------------------------------------------------------------
//          ファイルインクルード
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------
//          メイン
//------------------------------------------------------------------------------
public class InputManager : MonoBehaviour
{
    //--------------------------------------------------------------------------
    //          変数定義
    //--------------------------------------------------------------------------
    private static InputManager instance;
    private static Vector3 TouchOldPosition;

    //--------------------------------------------------------------------------
    //          デバッグ表示
    //--------------------------------------------------------------------------
    private InputManager()
    {
        // 生成表示
        //Debug.Log("Create SoundManager instance");
    }

    //--------------------------------------------------------------------------
    //          自己生成関数
    //--------------------------------------------------------------------------
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("InputManager");
                //DontDestroyOnLoad(obj);
                instance = obj.AddComponent<InputManager>();
            }

            return instance;
        }
    }

    //--------------------------------------------------------------------------
    //          入力判定処理
    //--------------------------------------------------------------------------
    //  Android,iphoneの場合はタッチを検出する
    //  エディタの場合は左クリックとして判定する
    //--------------------------------------------------------------------------
    ////    各状態フラグ
    ///////////////////////////////////////////////////////////////////////////
    private static bool isTouch;            // タップされているか
    private static bool isTouchTrigger;     // トリガー状態か
    private static bool isTouchRelease;     // リリース状態か
    private static bool isTouchMove;        // タップしたまま移動したか
    private static Touch touch;

    //--------------------------------------------------------------------------
    //          初期化処理
    //--------------------------------------------------------------------------
    void Start()
    {
        isTouch = false;
        isTouchTrigger = false;
        isTouchRelease = false;
        isTouchMove = false;

        ////        マルチタップ無効
        ////////////////////////////////////////////////////////////////////////
        Input.multiTouchEnabled = false;
    }

    //--------------------------------------------------------------------------
    //          更新処理
    //--------------------------------------------------------------------------
    void Update()
    {
        UpdateTouch();
    }

    //--------------------------------------------------------------------------
    //          タップ状態のアップデート
    //--------------------------------------------------------------------------
    private void UpdateTouch()
    {
        ////        タップ状況初期化
        ////////////////////////////////////////////////////////////////////////
        isTouch = false;
        isTouchTrigger = false;
        isTouchRelease = false;
        isTouchMove = false;

        ////        エディタでの更新処理
        ////////////////////////////////////////////////////////////////////////
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if(Input.GetMouseButton(0))
            {
                isTouch = true;
            }
            if (Input.GetMouseButtonDown(0))
            {
                isTouchTrigger = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isTouchRelease = true;
            }
        }

        ////        モバイルでの更新処理
        ////////////////////////////////////////////////////////////////////////
        else if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0)
            {
                //タッチしている
                isTouch = true;
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    //タッチ開始
                    isTouchTrigger = true;
                }

                else if (touch.phase == TouchPhase.Moved)
                {
                    //ドラッグ
                }

                else if (touch.phase == TouchPhase.Ended)
                {
                    //タッチ終了
                    isTouchRelease = true;
                }
            }
        }
    }

    //--------------------------------------------------------------------------
    //          各種判定処理
    //--------------------------------------------------------------------------
        //--------------------------------------------------------------------------
        //          クリック判定処理
        //--------------------------------------------------------------------------
        public static bool GetTouchPress()
        {
            return isTouch;
        }

        //--------------------------------------------------------------------------
        //          トリガー判定処理
        //--------------------------------------------------------------------------
        public static bool GetTouchTrigger()
        {
            return isTouchTrigger;
        }

        //--------------------------------------------------------------------------
        //          リリース判定処理
        //--------------------------------------------------------------------------
        public static bool GetTouchRelease()
        {
            return isTouchRelease;
        }

    //--------------------------------------------------------------------------
    //          タップ位置取得
    //--------------------------------------------------------------------------
    public static Vector3 GetTouchPosition(int i)
    {
        Vector3 screenPos = Vector3.zero;

		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		{
            ////        タップ位置を代入
            ////////////////////////////////////////////////////////////////////////
            screenPos = Input.mousePosition;
        }

        else if (Application.isMobilePlatform)
        {
            touch = Input.GetTouch(i);
            screenPos = touch.position;
            //screenPos = Input.mousePosition;    //screenPos = Input.GetTouch(Input.touchCount).position;
        }

        return screenPos;
    }

    //--------------------------------------------------------------------------
    //         移動距離判定(X軸)
    //--------------------------------------------------------------------------
    public static float GetTouchMoveHorizonal()
    {
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		{
            return Input.GetAxis("Mouse X");
        }

        else
        {
            touch = Input.GetTouch(0);

            Vector3 vec = touch.position;

            vec.z = 10f;

            vec = Camera.main.ScreenToWorldPoint(vec);

            Vector3 old = vec;

            vec = vec - TouchOldPosition;

            TouchOldPosition = old;

            return vec.x;
        }
    }

    //--------------------------------------------------------------------------
    //         移動距離判定(Y軸)
    //--------------------------------------------------------------------------
    public static float GetTouchMoveVertical()
    {
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		{
            return Input.GetAxis("Mouse Y");
        }

        else
        {
            touch = Input.GetTouch(0);

            Vector3 vec = touch.position;

            vec.z = 10f;

            vec = Camera.main.ScreenToWorldPoint(vec);

            Vector3 old = vec;

            vec = vec - TouchOldPosition;

            TouchOldPosition = old;

            return vec.y;
        }
    }
}
