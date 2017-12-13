using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineCameraStand : MonoBehaviour {

    public OfflinePostureController offlinePostureController;

    public GameObject targetObj;                        // プレイヤー情報
    public Vector3 localPos;                            // カメラローカル位置
    public float mouseInputX;
    public float mouseInputY;


    private Vector3 inputController;                    // コントローラー入力位置
    private Vector3 inputCameraController_last;         // カメラコントローラー前回入力位置
    private Vector3 inputCameraController_now;          // カメラコントローラー現在入力位置

    private Vector3 targetPos;                          // プレイヤー位置情報
    private Quaternion targetRot;                       // プレイヤー角度情報
    private Vector3 cameraDir;                          // カメラの進行方向射影ベクトル
    private Vector3 playerToCamera;                     // プレイヤーからカメラへのベクトル
    private Quaternion rotCamera;                       // カメラの傾き情報
    private float touchDiff;                            // マルチタップ,タッチ位置の差分
    private float rotSpeed = 0.5f;                      // 回転読度調整用定数


    public Vector3 GetCameraDirection
    {
        get { return this.cameraDir; }
    }

    Scr_ControllerManager controllerManager;            // コントローラのマネージャ
    Scr_CameraController cameraController;              // カメラコントローラのマネージャ

    int debug_cnt = 0;
    Vector3 _tes = Vector3.zero;

    void Start () {
        cameraDir = Vector3.Scale(transform.forward, new Vector3(1, 1, 1)).normalized;

        //コントロールマネージャの取得
        if (GameObject.Find("PuniconCamera/ControllerManager") != null)
        {
            controllerManager = GameObject.Find("PuniconCamera/ControllerManager").GetComponent<Scr_ControllerManager>();
            cameraController = GameObject.Find("PuniconCamera/CameraController").GetComponent<Scr_CameraController>();
        }
    }
	
	void Update () {


        // プレイヤー位置の取得
        targetPos = targetObj.transform.position;

        // プレイヤー回転量の取得
        targetRot = targetObj.transform.rotation;
        targetRot.y = 0.0f;

        // カメラの姿勢をプレイヤーと同期
        cameraDir = Vector3.ProjectOnPlane(cameraDir, offlinePostureController.GetsurfaceNormal);
        //transform.rotation = Quaternion.LookRotation(cameraDir, postureController.GetsurfaceNormal);
        rotCamera = Quaternion.LookRotation(cameraDir, offlinePostureController.GetsurfaceNormal);

        // 射影ベクトルで求めたカメラの傾きを退避
        Quaternion rotCameraEvac = rotCamera;

        // カメラのピッチ量に合わせて傾きを変更
        //rotCamera.x += rotPitch;
        transform.rotation = rotCamera;

        // カメラ位置の調整
        transform.position = targetObj.transform.position + rotCameraEvac * localPos;
        //transform.position = targetObj.transform.position + rotCamera * localPos;

        // エディタ上の処理
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            // マウスの右クリックを押している間
            if (Input.GetMouseButton(1))
            {
                // マウスの移動量
                mouseInputX = Input.GetAxis("Mouse X");
                mouseInputY = Input.GetAxis("Mouse Y");

                // カメラの回転処理
                transform.RotateAround(targetPos, offlinePostureController.GetsurfaceNormal, mouseInputX * Time.deltaTime * 200f);
                cameraDir = Vector3.ProjectOnPlane(targetPos - transform.position, offlinePostureController.GetsurfaceNormal);


            }
        }
        // スマホ上の処理
        else if (Application.isMobilePlatform)
        {
            // 2本指以上の時
            if (Input.touchCount == 2)
            {
                touchDiff = cameraController.GetCameraRotateLength();

                if (Mathf.Abs( touchDiff ) > 50.0f)
                {
                    transform.RotateAround(targetPos, offlinePostureController.GetsurfaceNormal, touchDiff * Time.deltaTime * rotSpeed);
                    cameraDir = Vector3.ProjectOnPlane(targetPos - transform.position, offlinePostureController.GetsurfaceNormal);
                }
            }
        }

    }
}
