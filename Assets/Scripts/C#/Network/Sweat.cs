using UnityEngine;
using System.Collections;
/**************************************************
 * 汗エフェクトクラス
 * 
 * 2017/03/27 橋本 周
 * ************************************************/
public class Sweat : MonoBehaviour {

    public ParticleSystem SweatRight;       //右の汗エフェクト取得
    public ParticleSystem SweatLeft;        //左の汗エフェクト取得

	[SerializeField]
    bool Use = false;       //使用フラグ
	bool playFlg = false;       //使用フラグ

	// Use this for initialization
	void Start () {

        Use = false;        //使用フラグの初期化
    }
	
	// Update is called once per frame
	void Update () {

		//カメラポジションの取得
		if (Camera.main != null)
		{
			transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
		}

        //使用フラグが無効の時
		if (!playFlg)
        {
            //箱の所持数が3個以上のとき
			if (Use)
            {
                //エフェクトの再生
                SweatRight.Play();
                SweatLeft.Play();

				playFlg = true;     //使用フラグの有効
            }
        }

        //使用フラグが有効の時
        else
        {
            //2個以下のとき
			if (!Use)
            {
                //エフェクトの停止
                SweatRight.Stop();
                SweatLeft.Stop();

				playFlg = false;    //使用フラグの無効
            }
        }
    }
}
