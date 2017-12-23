//------------------------------------------------------------------------------
//          ファイルインクルード
//------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//------------------------------------------------------------------------------
//          メイン
//------------------------------------------------------------------------------
public class Timer : MonoBehaviour 
{
    //--------------------------------------------------------------------------
    //          変数定義
    //--------------------------------------------------------------------------
    public int nTimeMin; 
    public int nTimeSec;
    public bool bCountDownFlug;
    private float fTimeCounter;

    public RawImage NumObjectMin00;
    public RawImage NumObjectMin01;
    public RawImage NumObjectSec00;
    public RawImage NumObjectSec01;

    private float TexWidth = 1.0f / 11.0f;

    public int secondTime { get; private set; }

    [SerializeField]
    GameObject manager;

	[SerializeField]
    MissionManagerScript missionManager;

	[SerializeField]
    NetworkMissionManager netMissionManager;

    //--------------------------------------------------------------------------
    //          初期化処理
    //--------------------------------------------------------------------------
    void Start () 
    {
        bCountDownFlug = false;     // カウントダウンフラグfalse
        fTimeCounter = 0.0f;        // フレームカウンタ初期化

        if(SceneManager.GetActiveScene().name == "Offline")
        {
            missionManager = manager.GetComponent<MissionManagerScript>();
        }
        else
        {
            netMissionManager = manager.GetComponent<NetworkMissionManager>();
        }
	}

    //--------------------------------------------------------------------------
    //          更新処理
    //--------------------------------------------------------------------------
    void Update () 
    {
        if (bCountDownFlug)
        {
            fTimeCounter = fTimeCounter + Time.deltaTime;

            if (fTimeCounter >= 1.0f)
            {
                nTimeSec--;

                if (nTimeSec < 0)
                {
                    if (nTimeMin <= 0)
                    {
                        nTimeMin = 0;
                        nTimeSec = 0;
                        bCountDownFlug = false;

						if (null != missionManager)
							missionManager.missionEndFlg = true;    //ミッション終了フラグを切り替え
						else if (null != netMissionManager) {
							netMissionManager.missionEndFlg = true;
						}

                    }

                    else
                    {
                        nTimeMin--; 
                        nTimeSec = 59;
                    }
                }

                fTimeCounter = 0.0f;
            }
        }

        UpdateNumObject();

	}

    //--------------------------------------------------------------------------
    //          オブジェクト情報更新処理
    //--------------------------------------------------------------------------
    void UpdateNumObject()
    {
        NumObjectMin00.uvRect = new Rect((nTimeMin / 10) * TexWidth, 0, TexWidth, 1);   // 10の桁
        NumObjectMin01.uvRect = new Rect((nTimeMin % 10) * TexWidth, 0, TexWidth, 1);   // 1の桁
        NumObjectSec00.uvRect = new Rect((nTimeSec / 10) * TexWidth, 0, TexWidth, 1);   // 10の桁
        NumObjectSec01.uvRect = new Rect((nTimeSec % 10) * TexWidth, 0, TexWidth, 1);   // 1の桁

        secondTime = nTimeMin * 60 + nTimeSec;
    }
}


