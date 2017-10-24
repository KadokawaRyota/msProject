using UnityEngine;



public class TapEffect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem tapEffect;           // タップエフェクト

    [SerializeField]
    Camera TargetCamera;                // カメラの座標

    [SerializeField]
    Scr_ControllerManager contmanager;                // カメラの座標

    public enum EFFECT_TYPE
    {
        NONE = 0,   // 出現していない
        TAP,        // タップエフェクト
        HOLD        // ホールドエフェクト
    }; 
    
    public bool EffectFlug = false;     // エフェクト発生フラグ 
    public Vector3 EffectPos;           // エフェクト発生位置
    public EFFECT_TYPE Effecttype;      // エフェクトタイプ
    public int nEffecttype_int;         // エフェクトタイプ(int型)
         
    // 初期化処理
    void Start()
    {
        ////    エフェクト情報初期化
        /////////////////////////////////////////////////////////////////////////////////////
        Effecttype = EFFECT_TYPE.NONE;
        nEffecttype_int = 0;
    }

    void Update()
    {
        // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
        if (EffectFlug)
        {
            Vector3 pos = Vector3.zero;

            switch (Effecttype)
            {
                case EFFECT_TYPE.NONE:
                    break;

                case EFFECT_TYPE.TAP:
                    tapEffect.transform.position = contmanager.TouchPositionStart;//EffectPos;
                    pos = TargetCamera.ScreenToWorldPoint(new Vector3(EffectPos.x, EffectPos.y, 1.0f));
                    tapEffect.transform.position = pos;
                    tapEffect.transform.localScale = new Vector3(10, 10, 10);
                    tapEffect.Emit(3);
                    EffectStatusReset();
                    Debug.Log("タップエフェクト発生");
                    break;

                case EFFECT_TYPE.HOLD:
                    tapEffect.transform.position = EffectPos;
                    pos = TargetCamera.ScreenToWorldPoint(new Vector3(EffectPos.x, EffectPos.y, 1.0f));
                    tapEffect.transform.position = pos;
                    tapEffect.transform.localScale = new Vector3(4, 4, 4);
                    tapEffect.Emit(1);
                    break;
                default:
                    break;
            }
        }
    }

    ////    エフェクトタイプセット関数
    ////    0-NONE 1-TAP 2-HOLD
    //////////////////////////////////////////////////////////////////////////////////////
    public void SetEffectType(int type)
    {
        Effecttype = (EFFECT_TYPE)type;
        nEffecttype_int = type;
    }

    public void EffectStatusReset()
    {
        EffectFlug = false;
        Effecttype = EFFECT_TYPE.NONE;
        if (nEffecttype_int == 2)
        tapEffect.Clear();
    }

}