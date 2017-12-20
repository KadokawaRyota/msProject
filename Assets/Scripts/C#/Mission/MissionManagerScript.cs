using UnityEngine;
using UnityEngine.UI;


public class MissionManagerScript : MonoBehaviour
{

    bool missionFlg = false;     //フラグの初期化
    bool missionEndFlg = false; //終了フラグ（trueのときにリザルトフラグを立てる）

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

    //タイム関連
    [SerializeField]
    GameObject timer;

    // Use this for initialization
    void Start()
    {
        missionFlg = false;     //フラグの初期化
    }

    // Update is called once per frame
    void Update()
    {
        //ミッションを決めたりする処理をここに入れる予定

        if (missionFlg)
        {
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
        }
    }

    public void StartMission()
    {
        missionFlg = true;
        //ミッションキャンバスを表示して移動
        MissionCanvas.GetComponent<MissionCanvasScript>().ImgFlgSwitch(true);
        //オブジェクトの表示
        MissionType.GetComponent<TransportationScript>().dispMission();
    }
}
