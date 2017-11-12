using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PostureController : NetworkBehaviour
{
    Camera camera;
    public Vector3 GetsurfaceNormal      // プレイヤー位置の地面の法線
    {
        get { return this.surfaceNormal; }
    }
    private Vector3 playerNormal;       // プレイヤーオブジェクトの法線
    private Vector3 surfaceNormal;      // プレイヤー接地面の法線
    private Animator animator;          // アニメーション情報
    private Vector3 dirVec;             // 現在のプレイヤー進行方向
    private Vector3 moveVec;            // プレイヤー移動量
    private Vector2 inputVec;           // 現在の入力移動方向
    private float radPlayer;            // 球面中心点からプレイヤーまでの距離
    private Vector3 prePosition;        // プレイヤー前回位置
    private Vector3 difPosition;        // プレイヤー位置の差分

    Scr_ControllerManager controllerManager;    //コントローラのマネージャ
    float length;

	[SerializeField]
	PlayerParticleManager particleManager;  //プレイヤーパーティクル


	// キーボード入力値
	float inputHorizontal;
    float inputVertical;


    void Start()
    {
		gameObject.transform.localPosition = new Vector3(0.0f, 25.0f, 0.0f);
		//GameObject.Find("OnlineCanvas/LodingImage").SetActive(false);     なにこれ1
        // アニメーション情報の取得
        animator = GetComponent<Animator>();

        // プレイヤーオブジェクト法線の初期化
        playerNormal = new Vector3(0.0f, 1.0f, 0.0f);

        // プレイヤー位置の地面の法線の初期化
        surfaceNormal = transform.localPosition - Vector3.zero;
        surfaceNormal = surfaceNormal.normalized;

        // プレイヤー進行方向の初期化
        dirVec = new Vector3(0.0f, 0.0f, 1.0f);

        // プレイヤー移動量ベクトルの初期化
        moveVec = Vector3.zero;

        // 球面中心点からプレイヤーまでの距離
        radPlayer = Mathf.Sqrt(
            transform.position.x * transform.position.x
            + transform.position.y * transform.position.y
            + transform.position.z * transform.position.z);

		//コントロールマネージャの取得
		controllerManager = GameObject.Find("PuniconCamera/ControllerManager").GetComponent<Scr_ControllerManager>();

        prePosition = transform.position;

		camera = GameObject.Find("PlayerCamera").GetComponent<Camera>();

		NetConnector netConnector = GameObject.Find("NetConnector").GetComponent<NetConnector>();

		//Offline時のときは位置、回転の同期スクリプトを無効にする
		if(!isLocalPlayer)
		{
			gameObject.GetComponent<PlayerSyncPosition>().enabled = true;
			gameObject.GetComponent<PlayerSyncRotation>().enabled = true;
		}
    }

	[Client]
    void Update()
    {
		//inputHorizontal = Input.GetAxisRaw("Horizontal");
		//inputVertical = Input.GetAxisRaw("Vertical");


		//コントローラの方向ベクトルを代入
		if (isLocalPlayer)
			inputVec = new Vector2(controllerManager.ControllerVec.normalized.x, controllerManager.ControllerVec.normalized.y);

		//inputVecN = new Vector2(inputHorizontal, inputVertical);
	}

	[Client]
    void FixedUpdate()
    {

        // プレイヤー位置の地面の法線の更新
        surfaceNormal = transform.position - Vector3.zero;
        surfaceNormal = surfaceNormal.normalized;


		/// 移動処理
		/// 

		// カメラ進行方向ベクトルを取得
		if (isLocalPlayer)
		{
			Vector3 cameraForward = Vector3.Scale(camera.transform.forward, new Vector3(1, 1, 1)).normalized;
			Vector3 moveForward;

			// 方向キーの入力値とカメラの向きから、移動方向を決定
			//moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;
			moveForward = cameraForward * inputVec.y + camera.transform.right * inputVec.x;

			// 移動方向にスピードを掛ける
			moveVec = moveForward * 0.05f;

			moveVec = Vector3.ProjectOnPlane(moveForward, surfaceNormal) * 0.05f;
			moveVec += (Vector3.zero - moveVec) * 0.5f;
			transform.position += moveVec;

			if (moveVec.magnitude > 0)
			{
				dirVec = moveVec.normalized;
			}

			// プレイヤーの回転
			transform.rotation = Quaternion.LookRotation(dirVec, surfaceNormal);

			// アニメーション状態取得
			if (moveVec.magnitude > 0)
			{
				animator.SetBool("is_running", true);
				particleManager.SetSmokeFlg(true);
			}
			else
			{
				animator.SetBool("is_running", false);
				particleManager.SetSmokeFlg(false);
			}

			// 球面中心点からプレイヤーまでの距離更新
			radPlayer = Mathf.Sqrt(
				transform.position.x * transform.position.x
				+ transform.position.y * transform.position.y
				+ transform.position.z * transform.position.z);

			// プレイヤー位置差分の取得
			difPosition = transform.position - prePosition;

			prePosition = transform.position;
		}
    }
}
