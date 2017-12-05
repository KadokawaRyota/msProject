using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//プレイヤー情報保持
public class CharactorInfo : MonoBehaviour {

    //現在存在しているオブジェクト実態の記憶領域
    static CharactorInfo instance;

    //キャラの種類
    public enum CHARA
    {
        TANUKI = 0,
        CAT,
        FOX,
        DOG
    };

    [SerializeField]
    static CHARA charSelectData;   //種類格納

    [SerializeField]
    static string playerName = "Player";      //プレイヤーの名前

    [SerializeField]
    GameObject playerPrefab_0;

    [SerializeField]
    GameObject playerPrefab_1;

    [SerializeField]
    GameObject playerPrefab_2;

    [SerializeField]
    GameObject playerPrefab_3;

    //シングルトン処理
    void Awake()
    {
        //CharactorInfoインスタンスが存在したら
        if (instance != null)
        {
            //今回インスタンス化したCharactorInfoを破棄
            Destroy(this.gameObject);
        }

        //CharactorInfoインスタンスがなかったら
        else if (instance == null)
        {
            //このCharactorInfoをインスタンスとする
            instance = this;
        }

        //シーンを跨いでもCharactorInfoインスタンスを破棄しない
        DontDestroyOnLoad(gameObject);


        if (SceneManager.GetActiveScene().name == "Offline")
        {
            GameObject player = null;

            switch (charSelectData)
            {
                case CHARA.TANUKI:
                    player = Instantiate(playerPrefab_0, new Vector3(0.0f, 25.5f, 0.0f), Quaternion.identity);
                    break;

                case CHARA.CAT:
                    player = Instantiate(playerPrefab_1, new Vector3(0.0f, 25.5f, 0.0f), Quaternion.identity);
                    break;

                case CHARA.FOX:
                    player = Instantiate(playerPrefab_2, new Vector3(0.0f, 25.5f, 0.0f), Quaternion.identity);
                    break;

                case CHARA.DOG:
                    player = Instantiate(playerPrefab_3, new Vector3(0.0f, 25.5f, 0.0f), Quaternion.identity);
                    break;
            }

            player.name = playerName;
        }
    }

    void Start()
    {
        /////キャラセレクトができるようになったら消す/////
        charSelectData = CHARA.DOG;
        playerName = "Player";
        /////////////////////////////////////
    }

    //プレイヤー名設定
    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    //使用キャラ設定
    public void SetCharaSelectData(CHARA chara)
    {
        charSelectData = chara;
    }

    //プレイヤー名取得
    public string GetPlayerName()
    {
        return playerName;
    }

    //使用キャラ取得
    public CHARA GetCharaSelectData()
    {
        return charSelectData;
    }
}
