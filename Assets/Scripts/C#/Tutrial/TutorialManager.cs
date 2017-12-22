using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//すべてのチュートリアルを管理
public class TutorialManager : MonoBehaviour {

    //現在存在しているオブジェクト実態の記憶領域
    static TutorialManager instance;

    public static bool nameTuto = false;       //名前入力
    public static bool offlineTuto = false;    //Offline時

    //シングルトン処理
    void Awake()
    {
        //TutorialManagerインスタンスが存在したら
        if (instance != null)
        {
            //今回インスタンス化したTutorialManagerを破棄
            Destroy(this.gameObject);
        }

        //TutorialManagerインスタンスがなかったら
        else if (instance == null)
        {
            //このCharactorInfoをインスタンスとする
            instance = this;
        }

        //シーンを跨いでもTutorialManagerインスタンスを破棄しない
        DontDestroyOnLoad(gameObject);

    }

    public bool GetNameTuto()
    {
        return nameTuto;
    }

    public void SetNameTuto(bool flg)
    {
        nameTuto = flg;
    }

}
