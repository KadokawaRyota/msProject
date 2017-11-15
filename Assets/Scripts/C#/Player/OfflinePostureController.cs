using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflinePostureController : MonoBehaviour {

	Camera camera;

    static float screenWidth = 1080.0f;
    static float screenHeight = 1920.0f;
    static float puniVecMax = Mathf.Sqrt(screenWidth * screenWidth + screenHeight * screenHeight);

    public float moveWalkSpeed = 0.1f;   // 歩き移動速度係数
    public float moveRunSpeed = 0.2f;    // 走り移動速度係数
    public float moveIn = 0.50f;         // 慣性
    public float moveThre = 0.50f;       // 移動速度変化の閾値

    public OfflineCameraStand cameraStand;

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
    private Vector2 inputVecN;          // 現在の入力移動方向( 正規化 )
	private float radPlayer;            // 球面中心点からプレイヤーまでの距離
	private Vector3 prePosition;        // プレイヤー前回位置
	private Vector3 difPosition;        // プレイヤー位置の差分

	Scr_ControllerManager controllerManager;    //コントローラのマネージャ
    private float VecLength;            // 入力されたベクトルの長さ


  	PlayerParticleManager particleManager;	//プレイヤーパーティクル
    // キーボード入力値
    //float inputHorizontal;
	//float inputVertical;

	void Start()
	{
		GameObject.Find("Scene Camera").SetActive(false);

		camera = GetComponentInChildren<Camera>();
		camera.enabled = true;
		AudioListener audio = camera.GetComponent<AudioListener>();
		audio.enabled = true;

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

		//パーティクルスクリプトの取得
		particleManager = GameObject.Find("WalkSmoke").GetComponent<PlayerParticleManager>();
	}

	void Update()
	{
        //inputHorizontal = Input.GetAxisRaw("Horizontal");
        //inputVertical = Input.GetAxisRaw("Vertical");


        //コントローラの方向ベクトルを代入
        inputVec = new Vector2(controllerManager.ControllerVec.x, controllerManager.ControllerVec.y);
        inputVecN = new Vector2(controllerManager.ControllerVec.normalized.x, controllerManager.ControllerVec.normalized.y);
        //inputVecN = new Vector2(inputHorizontal, inputVertical);

        // 入力ベクトルの長さを取得
        VecLength = controllerManager.ControllerVec.magnitude;
    }


	void FixedUpdate()
	{

		// プレイヤー位置の地面の法線の更新
		surfaceNormal = transform.position - Vector3.zero;
		surfaceNormal = surfaceNormal.normalized;
        

		/// 移動処理

		// カメラ進行方向ベクトルを取得
		//Vector3 cameraForward = Vector3.Scale(camera.transform.forward, new Vector3(1, 1, 1)).normalized;
        Vector3 cameraForward = Vector3.Scale(cameraStand.GetCameraDirection, new Vector3(1, 1, 1)).normalized;
        Vector3 moveForward;

		// 方向キーの入力値とカメラの向きから、移動方向を決定
		//moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;
		moveForward = cameraForward * inputVecN.y + camera.transform.right * inputVecN.x;

        // 入力量によって移動速度を変える

        if ( VecLength < ( puniVecMax * moveThre) )
        {
            moveVec = Vector3.ProjectOnPlane(moveForward, surfaceNormal) * moveWalkSpeed;
        }
        else if( VecLength >= ( puniVecMax * moveThre ) )
        {
            moveVec = Vector3.ProjectOnPlane(moveForward, surfaceNormal) * moveRunSpeed;
        }


		//moveVec = Vector3.ProjectOnPlane(moveForward, surfaceNormal);

        // 慣性
		moveVec += (Vector3.zero - moveVec) * moveIn;


        // 移動速度を位置に足しこむ
		transform.position += moveVec;


        // 移動方向にスピードを掛ける
        moveVec = moveVec * 0.05f;
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
			//animator.SetBool("is_walk", false);
			animator.SetBool("is_running", false);
			particleManager.SetSmokeFlg(false);
		}

		// 球面中心点からプレイヤーまでの距離更新]
		radPlayer = Mathf.Sqrt(
			transform.position.x * transform.position.x
			+ transform.position.y * transform.position.y
			+ transform.position.z * transform.position.z);

		// プレイヤー位置差分の取得
		difPosition = transform.position - prePosition;

		prePosition = transform.position;
		
	}
    public Vector3 GetmoveVec()
    {
        return moveVec;
    }

    public Vector3 GetSurfaceNormal()
    {
        return surfaceNormal;
    }
}
