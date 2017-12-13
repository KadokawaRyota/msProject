//------------------------------------------------------------------------------
//          ファイルインクルード
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------
//          メイン
//------------------------------------------------------------------------------
public class Scr_CameraController : MonoBehaviour 
{
    //--------------------------------------------------------------------------
    //          変数定義
    //--------------------------------------------------------------------------
    public static Touch tTouchInfo;       // タップ情報
    public Vector3  CameraMoveLength_Start; // 始点タップ位置(カメラ移動用)
    public Vector3  CameraMoveLength_End;   // 終点タップ位置(カメラ移動用)
    public float    fCameraMoveLength;      // カメラ移動距離(速度？)

    //--------------------------------------------------------------------------
    //          初期化処理
    //--------------------------------------------------------------------------
	void Start () 
    {
        // 回転情報変数初期化
        ResetValue();
	}

    //--------------------------------------------------------------------------
    //          更新処理
    //--------------------------------------------------------------------------
	public void CameraUpdate () 
    {
        if (Input.touchCount >= 2)
        {
            if (tTouchInfo.phase == TouchPhase.Began)
            {
                TouchTrigger();   // トリガー
            }
            else if (tTouchInfo.phase == TouchPhase.Moved || tTouchInfo.phase == TouchPhase.Stationary)
            {
                TouchMove();      // 押下
            }
            else if (tTouchInfo.phase == TouchPhase.Ended)
            {
                TouchRelease();   // リリース
            }
        }
        else
        {
            //ResetValue();
        }
	}

    //--------------------------------------------------------------------------
    //          タップされた瞬間の処理
    //--------------------------------------------------------------------------
    void TouchTrigger()
    {
        // 始点を設定
        CameraMoveLength_Start = tTouchInfo.position;
        CameraMoveLength_End = tTouchInfo.position;
        fCameraMoveLength = 0.0f;
    }

    //--------------------------------------------------------------------------
    //          タップ中の処理
    //--------------------------------------------------------------------------
    void TouchMove()
    {
        // 終点を設定
        CameraMoveLength_End = tTouchInfo.position;

        // 長さを算出
        fCameraMoveLength = CameraMoveLength_End.x - CameraMoveLength_Start.x;

        //Debug.Log("始点" + CameraMoveLength_Start + "終点" + CameraMoveLength_End + "ベクトル" + fCameraMoveLength);
    }

    //--------------------------------------------------------------------------
    //          タップ終了時の処理
    //--------------------------------------------------------------------------
    void TouchRelease()
    {
        // 変数初期化関数
        ResetValue();
    }

    //--------------------------------------------------------------------------
    //          変数初期化関数
    //--------------------------------------------------------------------------
    public void ResetValue()
    {
        CameraMoveLength_Start = Vector3.zero;  // 始点
        CameraMoveLength_End = Vector3.zero;    // 終点
        fCameraMoveLength = 0.0f;               // 始点～終点の長さ
    }

    //--------------------------------------------------------------------------
    //          カメラ回転情報のゲッター
    //--------------------------------------------------------------------------
    public float GetCameraRotateLength()
    {
        if (tTouchInfo.phase == TouchPhase.Moved) return fCameraMoveLength;
        else return 0.0f;
    }

    //--------------------------------------------------------------------------
    //          カメラコントローラのタップ位置取得
    //--------------------------------------------------------------------------
    public Vector3 GetCameraTouchPos()
    {
        return tTouchInfo.position;
    }
}
