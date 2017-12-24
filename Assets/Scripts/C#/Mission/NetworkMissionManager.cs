using UnityEngine;
using UnityEngine.UI;
public class NetworkMissionManager : MonoBehaviour
{
    GameObject Player = null;

    bool missionFlg = false;     //フラグの初期化
    public bool missionEndFlg = false; //終了フラグ（trueのときにリザルトフラグを立てる）

    [SerializeField]
    GameObject MissionCanvas;

    //配列化して、ミッションを入れる。今は運搬が入りっぱなし。
    [SerializeField]
    GameObject MissionType;

    //上バー関連
    [SerializeField]
    int missionStartCount = 30; //BG出現までの待機時間
    int missionUICount = 0;
    bool UIFlg = false;

    [SerializeField]
    Image missionBg;

    //カットイン関連
    bool cutInFlg = false;

    [SerializeField]
    GameObject missionCut;


    [SerializeField]
    Image cutBg;

    [SerializeField]
    Image cutChara;

    //タイム関連
    [SerializeField]
    Timer timer;

    [SerializeField]
    int timerStartCnt = 60;

    public Score score;

	[SerializeField]
	GameObject resultManager;

	AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        missionFlg = false;     //フラグの初期化
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
    }

    // Update is called once per frame
    void Update()
    {
        //ミッションを決めたりする処理をここに入れる予定
        //ミッション開始中
        if (missionFlg)
        {
            //MissionUI
            if (!UIFlg)
            {
                if (missionUICount > missionStartCount)
                {
                    UIFlg = true;
                    missionBg.GetComponent<Animator>().SetTrigger("open");

                    return;
                }

                missionUICount++;
            }

            //タイマー再生
            if (!timer.bCountDownFlug)
            {
                if (timerStartCnt <= 0)
                {
                    timer.bCountDownFlug = true;
                    Destroy(missionCut);
					audioManager.Play_BGM (AudioManager.BGM.Mission);
					audioManager.GetBGMMasterSource ().volume = 0.7f;
                    return;
                }

                timerStartCnt--;
            }


            //カットイン
            if (!cutInFlg)
            {
                //cutBg.gameObject.SetActive(true);
                cutChara.GetComponent<Animator>().SetTrigger("start");
                cutInFlg = true;
            }

			if (missionEndFlg) {

				//リザルト処理開始
				resultManager.GetComponent<Result> ().StartResult();
				missionFlg = false;
			}
				
        }
    }

    public void StartMission()
    {
        missionFlg = true;

        cutChara.gameObject.SetActive(true);

        //オブジェクトの表示（トランスポートミッション）
        MissionType.GetComponent<NetworkTransportationScript>().dispMission();
        //プレイヤーがトランスポートミッション中だとする。
        Player.GetComponent<playerTransportationScript>().CmdProvidebRunTimeToServer(true);

		//audioManager.GetBGMMasterSource ().volume = 0.2f;
		audioManager.Play_SE (AudioManager.SE.MissionStart);
    }

    public void SetPlayer( GameObject player )
    {
        Player = player;
    }
}
