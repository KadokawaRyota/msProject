using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlide : MonoBehaviour {


    public float timeRate = 2.0f;           // スライド速度

    private Vector3 destCameraPos;          // カメラの次の参照位置
    private bool move = false;              // スライド中かどうかの判定
    private float startTime;                // Δタイム取得の為のスライド開始時間
    private Vector3 startPosition;          // スライド開始位置

    public CharactorInfo.CHARA charNum = CharactorInfo.CHARA.TANUKI;    // キャラクタ識別番号

    CharactorInfo charaInfo;

    void Start() {
        
        charNum = GameObject.Find("CharactorInfo").GetComponent<CharactorInfo>().GetCharaSelectData();
        charaInfo = GameObject.Find("CharactorInfo").GetComponent<CharactorInfo>();
        charaInfo.SetCharaSelectData(charNum);
        transform.position += new Vector3(10.0f, 0.0f, 0.0f) * ((int)charNum);
        destCameraPos = transform.position;
        startPosition += transform.position;
    }
     
    void Update() {

        var diff = Time.timeSinceLevelLoad - startTime;
        var rate = diff / timeRate;

        if (move)
        {
            if (rate > 1)
            {
                transform.position = destCameraPos;
                move = false;
                return;
            }
            transform.position = Vector3.Lerp(startPosition, destCameraPos, rate);
        }

    }

    public void OnClickButton( string dir )
    {
        if (!move)
        {
            if( dir == "right" )
            {
                // 右にスライド
                destCameraPos = new Vector3(10.0f, 0.0f, 0.0f) + transform.position;
                startTime = Time.timeSinceLevelLoad;
                startPosition = transform.position;
                move = true;

                charNum += 1;
                if(charNum == CharactorInfo.CHARA.MAX)
                {
                    charNum = CharactorInfo.CHARA.TANUKI;
                }
                charaInfo.SetCharaSelectData(charNum);
            }
            else if( dir == "left" )
            {
                // 左にスライド
                destCameraPos = new Vector3(-10.0f, 0.0f, 0.0f) + transform.position;
                startTime = Time.timeSinceLevelLoad;
                startPosition = transform.position;
                move = true;

                if (charNum == CharactorInfo.CHARA.TANUKI)
                {
                    charNum = CharactorInfo.CHARA.MAX;
                }
                charNum -= 1;
                charaInfo.SetCharaSelectData(charNum);
            }
        }
    }
}
