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

    [SerializeField]
    GameObject Score1;
    [SerializeField]
    GameObject Score2;
    [SerializeField]
    GameObject Score3;
    [SerializeField]
    GameObject Score4;


    CharactorInfo charInfo;                 // キャラ情報スクリプト
    GameObject player;
    GameObject scoreObject;

    public CharactorInfo.CHARA resultCharNum;   // キャラ番号
    public bool resultFlg = false;              // フラグ

    //private int score = 0;                          // プレイヤーの最終スコア
	private int serverScore = 0;

	[SerializeField]
	NetConnector netConnector;

	//[SerializeField]
	////Score score;

	[SerializeField]
	GameObject canvas;

	float count = 0f;

	[SerializeField]
	float nextSceneTime = 5f;

	LoadSceneManager loadSceneManager;

	AudioManager audioManager;
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
		player = netConnector.GetLocalPlayer ();
        scoreObject = GameObject.Find("ResultManager/ResultObjectScore/Canvas/Score000");

		GameObject manager = GameObject.Find ("LoadSceneManager");

		if (null != manager) {
			loadSceneManager = manager.GetComponent<LoadSceneManager> ();
		}

		GameObject audio = GameObject.Find ("AudioManager");

		if (null != audio) {
			audioManager = audio.GetComponent<AudioManager> ();
		}
    }
	
	// Update is called once per frame
	void Update () {

		//debug
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Find("ResultManager").GetComponent<Result>().StartResult();
        }*/

        if (resultFlg)
        {
            /// リザルトカメラの配置
            /// 
            GameObject resultObject = null;

            // キャラ情報の取得
            resultCharNum = charInfo.GetCharaSelectData();

            if (player != null)
            {

                switch (resultCharNum)
                {
                    // タヌキ
                    case CharactorInfo.CHARA.TANUKI:
                        {
                            // リザルトオブジェクトの指定
                            resultObject = targetObject_Green;

                            // リザルトカメラ位置の指定
                            resultCamera.transform.position = Green_CameraPos.transform.position;
                            resultCamera.transform.rotation = Green_CameraPos.transform.rotation;

                            // スコア位置の指定
                            scoreObject.transform.position = Score1.transform.position;
                            scoreObject.transform.rotation = Score1.transform.rotation;

                            break;
                        }
                    // ネコ
                    case CharactorInfo.CHARA.CAT:
                        {
                            // リザルトオブジェクトの指定
                            resultObject = targetObject_Town;

                            // リザルトカメラ位置の指定
                            resultCamera.transform.position = Town_CameraPos.transform.position;
                            resultCamera.transform.rotation = Town_CameraPos.transform.rotation;

                            // スコア位置の指定
                            scoreObject.transform.position = Score2.transform.position;
                            scoreObject.transform.rotation = Score2.transform.rotation;
                            break;
                        }
                    // キツネ
                    case CharactorInfo.CHARA.FOX:
                        {
                            // リザルトオブジェクトの指定
                            resultObject = targetObject_Sand;

                            // リザルトカメラ位置の指定
                            resultCamera.transform.position = Sand_CameraPos.transform.position;
                            resultCamera.transform.rotation = Sand_CameraPos.transform.rotation;

                            // スコア位置の指定
                            scoreObject.transform.position = Score3.transform.position;
                            scoreObject.transform.rotation = Score3.transform.rotation;
                            break;
                        }
                    // イヌ
                    case CharactorInfo.CHARA.DOG:
                        {
                            // リザルトオブジェクトの指定
                            resultObject = targetObject_Ice;

                            // リザルトカメラ位置の指定
                            resultCamera.transform.position = Ice_CameraPos.transform.position;
                            resultCamera.transform.rotation = Ice_CameraPos.transform.rotation;

                            // スコア位置の指定
                            scoreObject.transform.position = Score4.transform.position;
                            scoreObject.transform.rotation = Score4.transform.rotation;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }


                /// プレイヤー情報の保持

                // プレイヤースコアの取得
                // ←ここでリザルト時のプレイヤーの得点を保持
                //score = 777;

				//プレイヤーから町の総合得点を受け取る
				serverScore = netConnector.GetLocalPlayer ().GetComponent<PlayerSyncScore> ().GetServerScore ();
					

                // 得点をリザルト用オブジェクトに加算
				GameObject.Find("ResultManager/ResultObjectScore/Canvas").GetComponent<Score>().SetPlusScore(serverScore);

                /// 不要なオブジェクトの削除

                // プレイヤーオブジェクトの削除
                Destroy(player);
                player = null;

                // リザルトカメラ起動
                GameObject.Find("ResultManager").transform.Find("ResultCamera").gameObject.SetActive(true);

                // Canvasの削除
                if (Application.loadedLevelName == "Offline")
                {
                    if(GameObject.Find("OfflineCanvas") != null)
                    Destroy(GameObject.Find("OfflineCanvas"));
                }
                else if(Application.loadedLevelName == "Main")
                {
                    Destroy(GameObject.Find("OnlineCanvas"));
                }

				GameObject.Find ("PuniconCamera").gameObject.SetActive (false);
				GameObject.Find ("NetworkMissionManager").gameObject.SetActive(false);

                // NPCの削除
                //Destroy(GameObject.Find("NPC"));

				canvas.gameObject.SetActive (true);
            }

			if (count >= nextSceneTime) {

				netConnector.NetDisconnect ();
				loadSceneManager.LoadNextScene ("Result");

			} else {
				
				count += Time.deltaTime;
			}
        }
	}

    //リザルトスタートフラグ
    public void StartResult()
    {
        resultFlg = true;

        player = GameObject.FindWithTag("Player");

		audioManager.Stop_BGM ();
		audioManager.Play_SE (AudioManager.SE.Result);

        //player.SetActive(false);

        //resultCamera.gameObject.SetActive(true);

        //resultCanvas.gameObject.SetActive(true);
    }


}
