//------------------------------------------------------------------------------
//          ファイルインクルード
//------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

//------------------------------------------------------------------------------
//          メイン
//------------------------------------------------------------------------------
public class Score : MonoBehaviour 
{
    //--------------------------------------------------------------------------
    //          変数定義
    //--------------------------------------------------------------------------
    [SerializeField]
    int nScore;         // スコア(オブジェクト適用)
    int nTargetScore;   // 目標スコア
    float fPlusNum;     // 増加値
    float fCalcScore;          // 計算用スコア

    [SerializeField]
    int nColumnNum;            // 桁数

    [SerializeField]
    float PlusFrame;           // 増加速度

    [SerializeField]
    RawImage[] NumObject;      // オブジェクト
    private float fTimeCounter;       // フレームカウンタ
    private float TexWidth = 1.0f / 11.0f;  // テクスチャ幅定義

    //--------------------------------------------------------------------------
    //          初期化処理
    //--------------------------------------------------------------------------
	void Start () 
    {
        ////    変数初期化
        ////////////////////////////////////////////////////////////////////////
        nScore = 0;                 // スコア初期化
        fCalcScore = 0.0f;          // 計算用スコア初期化
        nTargetScore = 0;           // 目標スコア初期化
        fPlusNum = 0;               // 増加値初期化
        fTimeCounter = 0.0f;        // フレームカウンタ初期化
	}

    //--------------------------------------------------------------------------
    //          更新処理
    //--------------------------------------------------------------------------
	void Update () 
    {
        if (fCalcScore < nTargetScore)
        {
            fCalcScore = fCalcScore + (fPlusNum / PlusFrame);
        }

        else
        {
            fCalcScore = nTargetScore;
        }

        nScore = (int)fCalcScore;
        UpdateNumObject();
        //Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
        {
            //SetPlusScore(100);
            //GameObject.Find("ResultManager").GetComponent<Result>().StartResult();
        } 
		
	}


    //--------------------------------------------------------------------------
    //          オブジェクト情報更新処理
    //--------------------------------------------------------------------------
    void UpdateNumObject()
    {
        ////    変数
        ////////////////////////////////////////////////////////////////////////
        int nCopyScore = nScore;    // 現在のスコア
        int CopyColumn = 1;         // 除算用桁


        ////    除算用桁数算出
        ////////////////////////////////////////////////////////////////////////
        for(int i = 0 ; i < nColumnNum - 1 ; i++)
        {
            CopyColumn = CopyColumn * 10;
        }

        ////    テクスチャ座標更新
        ////////////////////////////////////////////////////////////////////////
        for (int i = 0; i < nColumnNum; i++)
        {
            NumObject[i].uvRect = new Rect((nCopyScore / CopyColumn) * TexWidth, 0, TexWidth, 1);   // テクスチャ適応
            nCopyScore = nCopyScore % CopyColumn;                                                   // スコアの桁変更
            CopyColumn = CopyColumn / 10;                                                           // 除算用桁数変更
        }
    }

    //--------------------------------------------------------------------------
    //          スコア加算関数
    //--------------------------------------------------------------------------
    public void SetPlusScore(int PlusScore)
    {
        nTargetScore = nTargetScore + PlusScore;
        fPlusNum = nTargetScore - nScore;
    }
}
