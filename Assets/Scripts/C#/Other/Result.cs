using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//リザルト
public class Result : MonoBehaviour {

    [SerializeField]
    Camera resultCamera;                    //リザルトカメラ

    [SerializeField]
    Canvas resultCanvas;

    [SerializeField]
    GameObject targetObject_Green;          //タヌキの村のオブジェクト

    [SerializeField]
    GameObject targetObject_Town;           //ネコの村のオブジェクト

    [SerializeField]
    GameObject targetObject_Sand;           //キツネの村のオブジェクト

    [SerializeField]
    GameObject targetObject_Ice;            //イヌの村のオブジェクト

    [SerializeField]
    GameObject Green_CameraPos;          //タヌキの村のカメラ位置

    [SerializeField]
    GameObject Town_CameraPos;           //ネコの村のカメラ位置

    [SerializeField]
    GameObject Sand_CameraPos;           //キツネの村のカメラ位置

    [SerializeField]
    GameObject Ice_CameraPos;            //イヌの村のカメラ位置

    CharactorInfo charInfo;                 // キャラ情報スクリプト
    GameObject player;

    public CharactorInfo.CHARA resultCharNum;   // キャラ番号
    public bool resultFlg = false;              // フラグ
    public float distance;                     // オブジェクトからカメラへの距離

    private Vector3 dirCameraVec;               // オブジェクトからカメラへのベクトル
    private string playerName;


    void Awake()
    {
        resultFlg = false;
    }

    // Use this for initialization
    void Start () {

        if(targetObject_Green == null)
        {
            Debug.Log("タヌキのリザルトオブジェクトの設定してー");
        }

        if (targetObject_Sand == null)
        {
            Debug.Log("キツネのリザルトオブジェクトの設定してー");
        }

        if (targetObject_Ice == null)
        {
            Debug.Log("イヌのリザルトオブジェクトの設定してー");
        }

        if (targetObject_Town == null)
        {
            Debug.Log("ネコのリザルトオブジェクトの設定してー");
        }

        charInfo = GameObject.Find("CharactorInfo").GetComponent<CharactorInfo>();

    }
	
	// Update is called once per frame
	void Update () {

        if (resultFlg)
        {
            /// リザルトカメラの配置
            /// 
            GameObject resultObject = null;

            // キャラ情報の取得
            resultCharNum = charInfo.GetCharaSelectData();
            player = GameObject.FindWithTag("Player");

            if (player != null)
            {

                switch (resultCharNum)
                {
                    // タヌキ
                    case CharactorInfo.CHARA.TANUKI:
                        {
                            resultObject = targetObject_Green;
                            dirCameraVec = resultObject.transform.forward.normalized;
                            resultCamera.transform.position = Green_CameraPos.transform.position;
                            resultCamera.transform.rotation = Green_CameraPos.transform.rotation;
                            break;
                        }
                    // ネコ
                    case CharactorInfo.CHARA.CAT:
                        {
                            resultObject = targetObject_Town;
                            dirCameraVec = resultObject.transform.forward.normalized;
                            resultCamera.transform.position = Town_CameraPos.transform.position;
                            resultCamera.transform.rotation = Town_CameraPos.transform.rotation;
                            break;
                        }
                    // キツネ
                    case CharactorInfo.CHARA.FOX:
                        {
                            resultObject = targetObject_Sand;
                            dirCameraVec = resultObject.transform.forward.normalized;
                            resultCamera.transform.position = Sand_CameraPos.transform.position;
                            resultCamera.transform.rotation = Sand_CameraPos.transform.rotation;
                            break;
                        }
                    // イヌ
                    case CharactorInfo.CHARA.DOG:
                        {
                            resultObject = targetObject_Ice;
                            dirCameraVec = resultObject.transform.forward.normalized;
                            resultCamera.transform.position = Ice_CameraPos.transform.position;
                            resultCamera.transform.rotation = Ice_CameraPos.transform.rotation;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                /// スコアに必要な情報の保持

                // プレイヤーの名前の取得
                playerName = charInfo.GetPlayerName();

                // スコアの取得



                /// 不要なオブジェクトの削除

                // プレイヤーオブジェクトの削除
                Destroy(player);
                player = null;

                // リザルトカメラ起動
                GameObject.Find("ResultManager").transform.Find("ResultCamera").gameObject.SetActive(true);

                // Canvasの削除
                if (Application.loadedLevelName == "Offline")
                {
                    Destroy(GameObject.Find("OfflineCanvas"));
                }
                else if(Application.loadedLevelName == "Online")
                {
                    Destroy(GameObject.Find("OnlineCanvas"));
                }

                // NPCの削除
                Destroy(GameObject.Find("NPC"));

            }

        }
	}

    //リザルトスタートフラグ
    public void StartResult()
    {
        resultFlg = true;

        GameObject player = GameObject.Find("Player");

        player.SetActive(false);

        resultCamera.gameObject.SetActive(true);

        resultCanvas.gameObject.SetActive(true);
    }
}
