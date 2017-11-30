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
public class scr_TalkTips : MonoBehaviour 
{
	//--------------------------------------------------------------------------
	//          パブリックオブジェクト
	//--------------------------------------------------------------------------
    public GameObject TipsTitle;    // タイトル
    public GameObject[] Tips;       // 中身

	//--------------------------------------------------------------------------
	//          プライベートオブジェクト
	//--------------------------------------------------------------------------
	private int nTipsNum_now;	// 現在のTips番号
	public int nChildrenNum;	// 子オブジェクトの数

	//--------------------------------------------------------------------------
	//          初期化
	//--------------------------------------------------------------------------
	void Start () 
	{
		nTipsNum_now = 0;						    // 現在のTips番号(子の数と同じ)
		nChildrenNum = transform.childCount - 1;    // 子オブジェクトの数を取得(タイトルオブジェクトを引いた物)

		for(int i = 0 ; i < nChildrenNum ; i++) // 子オブジェクトを全て非表示化
		{
			Tips [i].SetActive (false);
		}

        TipsTitle.SetActive(true);              // タイトルを表示TRUE
        Tips[nTipsNum_now].SetActive(true);	    // 最初の子オブジェクトの表示TRUE
	}
	
	//--------------------------------------------------------------------------
	//          更新
	//--------------------------------------------------------------------------
	void Update () 
	{
		// if トリガーを検知
		//if (InputManager.GetTouchTrigger ()) {TipsChangeNext();} 
	}

	//--------------------------------------------------------------------------
	//          Tips切り替え(進める)
	//--------------------------------------------------------------------------
    public void TipsChangeNext()
    {
        ////    現在のTips番号が子オブジェクトの総数以下
        ////////////////////////////////////////////////////////////////////////
        if (nTipsNum_now < nChildrenNum - 1)
        {
            Tips[nTipsNum_now].SetActive(false);    // 現在のオブジェクトを非表示
            nTipsNum_now++;                         // 次のオブジェクト番号に切り替え
            Tips[nTipsNum_now].SetActive(true);     // 次のオブジェクトを表示
        }

        ////    Tips番号が最後だった場合
        ////////////////////////////////////////////////////////////////////////
        else
        {
            ChainCall(0);                           // 連続して呼び出す場合の関数
        }    
    }
    //--------------------------------------------------------------------------
    //          Tips切り替え(戻す)
    //--------------------------------------------------------------------------
    public void TipsChangeBack()
    {
        // 現在のTips番号が0以上
        if (nTipsNum_now > 0)
        {
            Tips[nTipsNum_now].SetActive(false);  // 現在のオブジェクトを非表示
            nTipsNum_now--;                       // ひとつ前のオブジェクトに
            Tips[nTipsNum_now].SetActive(true);   // 次のオブジェクトを表示
        }

        // Tips番号が最後だった場合
        else
        {
            ChainCall(1);                      // 連続して呼び出す場合の関数
        }
    }

    //--------------------------------------------------------------------------
    //          連続呼び出し関数(0: 進める　1:戻す)
    //--------------------------------------------------------------------------
    public void ChainCall(int i)
    { 
        switch(TalkTipsManager.GetTipsList())
        {
            case TIPS_LIST.NONE:
                if(i == 0)
                    TalkTipsManager.SetTips(TIPS_LIST.TEST00);
                break;

            case TIPS_LIST.TEST00:
                if (i == 0)
                    break;
                else
                    TalkTipsManager.SetTips(TIPS_LIST.NONE);

                    break;

            case TIPS_LIST.TEST01:
                break;

            default:
                break;
        }

        this.gameObject.SetActive(false);  // 自身を無効化
    }
}
