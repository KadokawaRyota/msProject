using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//すべてのチュートリアルを管理
public class TutorialManager : MonoBehaviour {

    //現在存在しているオブジェクト実態の記憶領域
    static TutorialManager instance;

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

    public static bool nameTuto = false;       //名前入力
    public static bool offlineTuto = false;    //Offline時
       
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
