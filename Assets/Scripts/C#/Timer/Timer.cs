//------------------------------------------------------------------------------
//          ファイルインクルード
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//------------------------------------------------------------------------------
//          メイン
//------------------------------------------------------------------------------
public class Timer : MonoBehaviour 
{
    //--------------------------------------------------------------------------
    //          変数定義
    //--------------------------------------------------------------------------
    public int nTimeMin; 
    public int nTimeSec;
    public bool bCountDownFlug;
    public float fTimeCounter;
    public int secondTime;

    public RawImage NumObjectMin00;
    public RawImage NumObjectMin01;
    public RawImage NumObjectSec00;
    public RawImage NumObjectSec01;

    private float TexWidth = 1.0f / 11.0f;

    //--------------------------------------------------------------------------
    //          初期化処理
    //--------------------------------------------------------------------------
    void Start () 
    {
        bCountDownFlug = false;     // カウントダウンフラグfalse
        fTimeCounter = 0.0f;        // フレームカウンタ初期化
	}

    //--------------------------------------------------------------------------
    //          更新処理
    //--------------------------------------------------------------------------
    void Update () 
    {
        if (bCountDownFlug)
        {
            fTimeCounter = fTimeCounter + Time.deltaTime;

            if (fTimeCounter >= 1.0f)
            {
                nTimeSec--;

                if (nTimeSec < 0)
                {
                    if (nTimeMin <= 0)
                    {
                        nTimeMin = 0;
                        nTimeSec = 0;
                        bCountDownFlug = false;

                        //タイムアップなので終了処理を呼ぶ（MissionManagerから）
                    }

                    else
                    {
                        nTimeMin--; 
                        nTimeSec = 59;
                    }
                }

                fTimeCounter = 0.0f;
            }
        }

        UpdateNumObject();

	}

    //--------------------------------------------------------------------------
    //          オブジェクト情報更新処理
    //--------------------------------------------------------------------------
    void UpdateNumObject()
    {
        NumObjectMin00.uvRect = new Rect((nTimeMin / 10) * TexWidth, 0, TexWidth, 1);   // 10の桁
        NumObjectMin01.uvRect = new Rect((nTimeMin % 10) * TexWidth, 0, TexWidth, 1);   // 1の桁
        NumObjectSec00.uvRect = new Rect((nTimeSec / 10) * TexWidth, 0, TexWidth, 1);   // 10の桁
        NumObjectSec01.uvRect = new Rect((nTimeSec % 10) * TexWidth, 0, TexWidth, 1);   // 1の桁

        secondTime = nTimeMin * 60 + nTimeSec;
    }
}


