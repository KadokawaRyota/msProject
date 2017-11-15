using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionImageScript : MonoBehaviour
{

    bool missionFlg;    //フラグ


    /*** ミッションイメージ変数 ***/
    [SerializeField]
    Image missionImage;
    int imgCnt;

    bool imgTexFlg;

    /// <summary>
    /// ////////////////////適当
    /// </summary>


    // Use this for initialization
    void Start()
    {
        imgCnt = 0;

        imgTexFlg = false;

        missionImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (imgTexFlg)
        {
            //ロゴ表示
            if (imgCnt < 120)
            {
                imgCnt++;
            }
            else
            {
                //ロゴを移動させる
                if (missionImage.gameObject.transform.localPosition.y < Screen.height * 0.5f - missionImage.gameObject.GetComponent<RectTransform>().sizeDelta.y * 0.5f)
                {
                    missionImage.gameObject.transform.localPosition += new Vector3((Screen.width * 0.5f / missionImage.gameObject.GetComponent<RectTransform>().sizeDelta.x * 0.5f) * 16.0f,
                                                                                    (Screen.height * 0.5f / missionImage.gameObject.GetComponent<RectTransform>().sizeDelta.y * 0.5f) * 10.0f,
                                                                                    0.0f);

                    missionImage.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                }
                else
                {
                    imgTexFlg = false;
                    imgCnt = 0;
                    transform.parent.GetComponent<MissionManagerScript>().ExecMission();
                }
            }
        }
    }

    public void dispMissionImage( bool bDisp )
    {
        imgTexFlg = true;
        missionImage.gameObject.SetActive(bDisp);
    }
}
