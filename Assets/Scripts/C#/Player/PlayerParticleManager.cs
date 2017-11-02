using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleManager : MonoBehaviour 
{
    [SerializeField]
    ParticleSystem PlayerParticleObject;    // パーティクルオブジェクト    
    
    public bool bEmitFlug;                  // パーティクル発生フラグ(プレイヤ側の状態で切り替える)
    public int nEmitCountNum;               // パーティクル発生までのフレーム数
    private int nFlugChangeCounter;         // フラグ切り替え用カウンター

    //--------------------------------------------------------------------
    //      初期化処理
    //--------------------------------------------------------------------
    void Start () 
    {
        nFlugChangeCounter = 0;             // カウンタ初期化
	}

    //--------------------------------------------------------------------
    //      更新処理
    //--------------------------------------------------------------------
	void Update () 
    {
        ////        発生フラグがTRUEの場合のみ
        ///////////////////////////////////////////////////////////////////
        if (bEmitFlug)
        {
            // 一定フレーム経過後
            if (nEmitCountNum <= nFlugChangeCounter)
            {
                PlayerParticleObject.Emit(1);   // パーティクル発生
                nFlugChangeCounter = 0;         // カウンタリセット
                    //bEmitFlug = false;            // フラグをfalseに
            }

            else
            {
                nFlugChangeCounter++;           // カウンタを加算
            }
        }
	}

	//フラグ切り替え関数
	public void SetSmokeFlg(bool flg)
	{
		bEmitFlug = flg;
	}
}
