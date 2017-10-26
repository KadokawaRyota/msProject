//------------------------------------------------------------------------------
//          ファイルインクルード
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------
//          定義
//------------------------------------------------------------------------------
public enum EFFECT_TYPE
{
    NONE = 0,   // 出現していない
    TAP,        // タップエフェクト
    HOLD        // ホールドエフェクト
}; 

//------------------------------------------------------------------------------
//          メイン
//------------------------------------------------------------------------------
public class TapEffect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem tapEffect;   // タップエフェクト

    [SerializeField]
    Camera TargetCamera;        // カメラの座標

    //--------------------------------------------------------------------------
    //          変数定義
    //--------------------------------------------------------------------------
    public bool EffectFlug;         // エフェクト発生フラグ 
    public Vector3 EffectPos;       // エフェクト発生位置
    public EFFECT_TYPE Effecttype;  // エフェクトタイプ

    //--------------------------------------------------------------------------
    //          初期化処理
    //--------------------------------------------------------------------------
    void Start()
    {
        EffectStatusReset();    //    エフェクト情報初期化
    }

    //--------------------------------------------------------------------------
    //          更新処理
    //--------------------------------------------------------------------------
    void Update()
    {
        if (EffectFlug)
        {
            Vector3 pos = Vector3.zero;

            switch (Effecttype)
            {
                case EFFECT_TYPE.NONE:
                    break;

                ////    タップエフェクト
                /////////////////////////////////////////////////////////////////////////////////////
                case EFFECT_TYPE.TAP:       // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
                    // ポジション設定
                    tapEffect.transform.position = EffectPos;
                    pos = TargetCamera.ScreenToWorldPoint(new Vector3(EffectPos.x, EffectPos.y, 1.0f));
                    tapEffect.transform.position = pos;
                    
                    // スケール値変更
                    tapEffect.transform.localScale = new Vector3(8, 8, 8);
                    
                    // 発生
                    tapEffect.Emit(2);
                    
                    // 情報リセット
                    //EffectStatusReset();
                    EffectFlug = false;
                    
                    // デバック表示
                    Debug.Log("タップエフェクト発生");
                    break;

                ////    ホールドエフェクト
                /////////////////////////////////////////////////////////////////////////////////////
                case EFFECT_TYPE.HOLD:
                    // ポジション設定
                    tapEffect.transform.position = EffectPos;
                    pos = TargetCamera.ScreenToWorldPoint(new Vector3(EffectPos.x, EffectPos.y, 1.0f));
                    tapEffect.transform.position = pos;
                    
                    // スケール値設定
                    tapEffect.transform.localScale = new Vector3(4, 4, 4);
                    
                    // 発生
                    tapEffect.Emit(1);
                    break;

                default:
                    break;
            }
        }
    }

    //--------------------------------------------------------------------------
    //      エフェクトタイプセット関数
    //      (0-NONE 1-TAP 2-HOLD)
    //--------------------------------------------------------------------------
    public void SetEffectType(EFFECT_TYPE type)
    {
        Effecttype = type;
    }

    //--------------------------------------------------------------------------
    //      エフェクト状態初期化
    //--------------------------------------------------------------------------
    public void EffectStatusReset()
    {
        EffectFlug = false;                 // 発生フラグ
        EffectPos = Vector3.zero;           // 発生位置
        if (Effecttype == EFFECT_TYPE.HOLD) // エフェクト消去
        tapEffect.Clear();
        Effecttype = EFFECT_TYPE.NONE;      // エフェクトタイプ
    }
}