﻿//------------------------------------------------------------------------------
//          ファイルインクルード
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//------------------------------------------------------------------------------
//          定義
//------------------------------------------------------------------------------
public enum TOUCH_MODE
{
    NONE,       // タップされていない
    TRIGGER,    // タップされた瞬間
    MOVE,       // タップをキープしている
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
    public Vector3 TouchPositionStart;          // タップ開始位置
    public Vector3 TouchPositionNow;            // 現在のタップ位置
    public Vector3 ControllerVec;               // タップ点の始点から終点へのベクトル
    public float ControllerVecLength;           // ベクトルの長さ
    public float MaxMoveSpeed;                  // 最高速度
    private Vector3 MoveVec;                    // 移動方向  
    private static TOUCH_MODE TouchMode;        // 現在のタップ状態

    //--------------------------------------------------------------------------
    //          初期化処理
    //--------------------------------------------------------------------------
	void Start () 
    {
        ////       インプットマネージャ生成
        ////////////////////////////////////////////////////////////////////////
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
        ////        タップ状態別の更新処理
        ////////////////////////////////////////////////////////////////////////
        if (InputManager.GetTouchTrigger()) TouchTrigger();                                  // タップされた瞬間
        if (InputManager.GetTouchPress() && !InputManager.GetTouchTrigger()) TouchMove();    // タップをキープしている状態
        if (InputManager.GetTouchRelease()) TouchRelease();                                  // タップを解除した瞬間
    }

    //--------------------------------------------------------------------------
    //          タップされた瞬間(トリガー処理)
    //--------------------------------------------------------------------------    
    public void TouchTrigger()
    {
        ////        タッチ状態の変更
        ////////////////////////////////////////////////////////////////////////
        TouchMode = TOUCH_MODE.TRIGGER;

        ////        タップ位置取得
        ////////////////////////////////////////////////////////////////////////
        TouchPositionStart = InputManager.GetTouchPosition();
        TouchPositionNow = InputManager.GetTouchPosition();
    }

    //--------------------------------------------------------------------------
    //          タップされている(ムーブ処理)
    //--------------------------------------------------------------------------
    void TouchMove()
    {
        ////        タッチ状態の変更
        ////////////////////////////////////////////////////////////////////////
        TouchMode = TOUCH_MODE.MOVE;

        ////        現在のタップ位置取得
        ////////////////////////////////////////////////////////////////////////
        TouchPositionNow = InputManager.GetTouchPosition();

        ////        始点と終点の方向ベクトル生成
        ////////////////////////////////////////////////////////////////////////
        ControllerVec = (TouchPositionNow - TouchPositionStart).normalized;
        ControllerVecLength = ControllerVec.magnitude;
    }

    //--------------------------------------------------------------------------
    //          タップが解除された(リリース処理)
    //--------------------------------------------------------------------------
    void TouchRelease()
    {
        ////        タッチ状態の変更
        ////////////////////////////////////////////////////////////////////////
        TouchMode = TOUCH_MODE.NONE;
    }

    //--------------------------------------------------------------------------
    //          タップ状態の取得
    //--------------------------------------------------------------------------
    public TOUCH_MODE GetTouchMode()
    {
        ////        現在のタッチ状態を返す
        ////////////////////////////////////////////////////////////////////////
        return TouchMode;
    }
}
