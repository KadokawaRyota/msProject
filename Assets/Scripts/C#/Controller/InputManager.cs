//------------------------------------------------------------------------------
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
    //private static bool[] isTouch = new bool[2];            // タップされているか
    //private static bool[] isTouchTrigger = new bool[2];     // トリガー状態か
    //private static bool[] isTouchRelease = new bool[2];     // リリース状態か
    //private static bool[] isTouchMove = new bool[2];        // タップしたまま移動したか
    //private static Touch[] touch = new Touch[2];
    
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
        //////        タップ情報の初期化
        //////////////////////////////////////////////////////////////////////////
        //for (int i = 0; i < 2; i++)
        //{
        //    isTouch[i]          = false;
        //    isTouchTrigger[i]   = false;
        //    isTouchRelease[i]   = false;
        //    isTouchMove[i]      = false;        
        //}
        
        isTouch = false;
        isTouchTrigger = false;
        isTouchRelease = false;
        isTouchMove = false;

        

        ////        マルチタップ無効
        ////////////////////////////////////////////////////////////////////////
        //Input.multiTouchEnabled = false;
    }

    //--------------------------------------------------------------------------
    //          更新処理
    //--------------------------------------------------------------------------
    void Update()
    {
        //for (int i = 0; i < 2; i++)
        //{
        //    UpdateTouch(i);
        //}

        UpdateTouch(0);

        
    }

    //--------------------------------------------------------------------------
    //          タップ状態のアップデート
    //--------------------------------------------------------------------------
    private void UpdateTouch(int Num)
    {
       
        ////        タップ状況初期化
        ////////////////////////////////////////////////////////////////////////
        //isTouch[Num]        = false;
        //isTouchTrigger[Num] = false;
        //isTouchRelease[Num] = false;
        //isTouchMove[Num]    = false;
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
                //isTouch[0] = true;
                isTouch = true;
            }
            if (Input.GetMouseButtonDown(Num))
            {
                //isTouchTrigger[0] = true;
                isTouchTrigger = true;
            }

            if (Input.GetMouseButtonUp(Num))
            {
                //isTouchRelease[0] = true;
                isTouchRelease = true;
            }
        }

        ////        モバイルでの更新処理
        ////////////////////////////////////////////////////////////////////////
        else if (Application.isMobilePlatform)
        {
            //タッチしている
            //isTouch[Num] = true;
            //isTouch = true;
            ////
            /////////////////////////////////////////////////////////////////
            //touch[Num] = Input.GetTouch(Num);
            touch = Input.GetTouch(0);
            //タッチ開始
            //if (touch[Num].phase == TouchPhase.Began)isTouchTrigger[Num] = true;
            if (touch.phase == TouchPhase.Began) isTouchTrigger = true;

            //ドラッグ
            //else if (touch[Num].phase == TouchPhase.Moved)isTouchMove[Num] = true;
            else if (touch.phase == TouchPhase.Moved) isTouchMove = true;

            //タッチ終了
            //else if (touch[Num].phase == TouchPhase.Ended)isTouchRelease[Num] = true;
            else if (touch.phase == TouchPhase.Ended) isTouchRelease = true;
        }
    }

    //--------------------------------------------------------------------------
    //          各種判定処理
    //--------------------------------------------------------------------------
        //--------------------------------------------------------------------------
        //          クリック判定処理
        //--------------------------------------------------------------------------
        //public static bool GetTouchPress(int Num){return isTouch[Num];}
        public static bool GetTouchPress() { return isTouch; }
        
        //--------------------------------------------------------------------------
        //          トリガー判定処理
        //--------------------------------------------------------------------------
        //public static bool GetTouchTrigger(int Num){return isTouchTrigger[Num];}
        public static bool GetTouchTrigger() { return isTouchTrigger; }

        //--------------------------------------------------------------------------
        //          リリース判定処理
        //--------------------------------------------------------------------------
        //public static bool GetTouchRelease(int Num){return isTouchRelease[Num];}
        public static bool GetTouchRelease() { return isTouchRelease; }
    
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
            screenPos = Input.GetTouch(i).position;
            //screenPos = Input.mousePosition;    //screenPos = Input.GetTouch(Input.touchCount).position;
        }

        return screenPos;
    }

    public static int GetTapFingerCount()
    {
        //touch.tapCount;
        return 0;
    }
}
