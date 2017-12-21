using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//リザルト
public class Result : MonoBehaviour {

    [SerializeField]
    Camera resultCamera;        //リザルトカメラ

    [SerializeField]
    Canvas resultCanvas;

    [SerializeField]
    GameObject targetObject_Green;      //タヌキの村のオブジェクト

    [SerializeField]
    GameObject targetObject_Town;      //ネコの村のオブジェクト

    [SerializeField]
    GameObject targetObject_Sand;      //キツネの村のオブジェクト

    [SerializeField]
    GameObject targetObject_Ice;      //イヌの村のオブジェクト

    CharactorInfo charInfo;                     // キャラ情報スクリプト
    public CharactorInfo.CHARA resultCharNum;   // キャラ番号
    public bool resultFlg = false;              // フラグ
    public float distance;                     // オブジェクトからカメラへの距離

    private Vector3 dirCameraVec;               // オブジェクトからカメラへのベクトル


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

            // キャラ情報の取得
            resultCharNum = charInfo.GetCharaSelectData();

            switch (resultCharNum)
            {
                // タヌキ
                case CharactorInfo.CHARA.TANUKI:
                    {
                        dirCameraVec = targetObject_Green.transform.forward.normalized;
                        resultCamera.transform.position = targetObject_Green.transform.position + dirCameraVec * distance;
                        return;
                    }
                // ネコ
                case CharactorInfo.CHARA.CAT:
                    {
                        dirCameraVec = targetObject_Town.transform.forward.normalized;
                        resultCamera.transform.position = targetObject_Town.transform.position + dirCameraVec * distance;
                        return;
                    }
                // キツネ
                case CharactorInfo.CHARA.FOX:
                    {
                        dirCameraVec = targetObject_Sand.transform.forward.normalized;
                        resultCamera.transform.position = targetObject_Sand.transform.position + dirCameraVec * distance;
                        return;
                    }
                // イヌ
                case CharactorInfo.CHARA.DOG:
                    {
                        dirCameraVec = targetObject_Ice.transform.forward.normalized;
                        resultCamera.transform.position = targetObject_Ice.transform.position + dirCameraVec * distance;
                        return;
                    }
                default:
                    {
                        return;
                    }
            }



            /// 不要なオブジェクトの削除

            // プレイヤーオブジェクトの削除
            if(  )

            //

            // ぷにコンの先所

            // Missionオブジェクトの削除

            // OfflineCanvasの削除

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
