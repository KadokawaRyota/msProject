//------------------------------------------------------------------------------
//          ファイルインクルード
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//------------------------------------------------------------------------------
//          定義
//------------------------------------------------------------------------------
enum TOUCH_MODE
{
    NONE,       // タップされていない
    TOUCH,      // タップされている
};

//------------------------------------------------------------------------------
//          メイン
//------------------------------------------------------------------------------
public class Scr_ControllerManager : MonoBehaviour 
{
    //--------------------------------------------------------------------------
    //          変数定義
    //--------------------------------------------------------------------------
    InputManager inputManager;                  // インプットマネージャー
    Vector3 TouchPositionStart;                 // タップ開始位置
    Vector3 TouchPositionNow;                   // 現在のタップ位置

    public Vector3 ControllerVec;               // タップ点の始点から終点へのベクトル
    public float ControllerVecLength;         // ベクトルの長さ

    public float MaxMoveSpeed;                  // 最高速度
    private Vector3 MoveVec;                    // 移動方向  
    private static TOUCH_MODE TouchMode;        // 現在のタップ状態

    //--------------------------------------------------------------------------
    //          初期化処理
    //--------------------------------------------------------------------------
	void Start () 
    {
        inputManager = InputManager.Instance;
        
        ////        初期値設定
        ////////////////////////////////////////////////////////////////////////
        TouchMode = TOUCH_MODE.NONE;    // 現在のタップ状態
    }

    //--------------------------------------------------------------------------
    //          更新処理
    //--------------------------------------------------------------------------
	void Update () 
    {
        ////    現在のタップの状態をデバック表示
        ////////////////////////////////////////////////////////////////////////
        //Debug.Log("ARMMODE：" + TouchMode);
        
        ////        タップ状態別の更新処理
        ////////////////////////////////////////////////////////////////////////
        switch (TouchMode)
        {
            case TOUCH_MODE.NONE:       //通常時
                None();
                break;
            case TOUCH_MODE.TOUCH:      //タップ時
                Move();
                break;
        }
    }

    //--------------------------------------------------------------------------
    //          タップ時処理
    //--------------------------------------------------------------------------
    public void OnClickTrigger()
    {
        TouchMode = TOUCH_MODE.TOUCH;                            // タップ状態にする
        TouchPositionStart = InputManager.GetTouchPosition();    // タップ位置取得
        
        ////        デバック処理
        ////////////////////////////////////////////////////////////////////////
        Debug.Log("始点：" + TouchPositionStart);                // タップ位置表示
    }

    //--------------------------------------------------------------------------
    //          タップ解除処理
    //--------------------------------------------------------------------------
    public void OutClick()
    {
        TouchMode = TOUCH_MODE.NONE;

		ControllerVec = new Vector3(0.0f, 0.0f, 0.0f);
		ControllerVecLength = ControllerVec.magnitude;
	}

    //--------------------------------------------------------------------------
    //          各状態での更新処理
    //--------------------------------------------------------------------------
        ////        タップされていない
        ////////////////////////////////////////////////////////////////////////
        private void None()
        {
            bool clicked = InputManager.GetTouchTrigger();
            if (clicked)
            {
                OnClickTrigger();
            }
        }

        ////        タップされている
        ////////////////////////////////////////////////////////////////////////
        void Move()
        {
            ////    タップされていない場合
            ////////////////////////////////////////////////////////////////////
            if (InputManager.GetTouchRelease())
            {
                OutClick();
                return;
            }

            ////    タップされている場合の処理
            ////////////////////////////////////////////////////////////////////

            // タップ位置取得
            TouchPositionNow = InputManager.GetTouchPosition();    
            
            // 始点と終点の方向ベクトル生成
            //Vector3 vec3 = (TouchPositionNow - TouchPositionStart).normalized;
            //ControllerVec = (TouchPositionNow - TouchPositionStart).normalized;
		ControllerVec = TouchPositionNow - TouchPositionStart;
		ControllerVecLength = ControllerVec.magnitude;

            ////    デバック処理
            ////////////////////////////////////////////////////////////////////
            
            // タップ位置表示()
            Debug.Log("ベクトルの長さ : " + ControllerVec);
              
        }
}
