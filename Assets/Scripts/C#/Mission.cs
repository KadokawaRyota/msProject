using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour {

	bool missionFlg;    //フラグ


	/*** ミッションイメージ変数 ***/
	[SerializeField]
	Image missionImage;
	int imgCnt;

	bool imgTexFlg;
    bool objectDispFlg;

    [SerializeField]
    GameObject Transportation;


    /// <summary>
    /// ////////////////////適当
    /// </summary>


// Use this for initialization
void Start () {
		missionFlg = false;     //フラグの初期化
        missionFlg = true;

        imgCnt = 0;

		imgTexFlg = false;
        objectDispFlg = false;

		missionImage.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
		//ミッション開始
		if(missionFlg)
		{
			if (!imgTexFlg)
			{
                //ロゴ表示
                if (imgCnt < 120)
                {
                    imgCnt++;
                }
                else
                {
                    if (missionImage.gameObject.transform.localPosition.y < Screen.height * 0.5f - missionImage.gameObject.GetComponent<RectTransform>().sizeDelta.y * 0.5f)
                    {
                        missionImage.gameObject.transform.localPosition += new Vector3((Screen.width * 0.5f / missionImage.gameObject.GetComponent<RectTransform>().sizeDelta.x * 0.5f) * 16.0f,
                                                                                       (Screen.height * 0.5f / missionImage.gameObject.GetComponent<RectTransform>().sizeDelta.y * 0.5f) * 10.0f,
                                                                                        0.0f);

                        missionImage.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                    }
                    //実行されるミッションによって変える・・・今は運搬のみ
                    Transportation.GetComponent<TransportationScript>().StartMission();
                }
			}
		}
	
	}

	public void StartMission()
	{
		missionFlg = true;
		missionImage.gameObject.SetActive(true);
	}
}
