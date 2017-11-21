using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour {

    public float walkspeed = 0.01f;          // 歩き速度
    public float moveIn = 0.50f;            // 慣性
    public GameObject destPos1, destPos2;   // 目的地
    public bool dest1 = true;
    public bool dest2 = false;

    public float moveTimeRange = 2.0f;
    public bool modeRand = true;

    private Vector3 dirVecZ;
    private Vector3 moveVec;                // NPC移動方向
    private Vector3 surfaceNormal;          // 法線
    private Vector3 moveForward;
    private Vector2 NPCinputVec;
    private float waitTime = 0.0f;
    private bool move = false;
    private Animator animator;          // アニメーション情報
    enum AnimationNum { Idle, Walk, Running };
    AnimationNum animationNum;

    void Start () {
        NPCinputVec = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        // アニメーション情報の取得
        animator = GetComponent<Animator>();

        // アニメーション状態の初期化
        animationNum = AnimationNum.Idle;
    }

    void Update()
    {
        waitTime = waitTime + Time.deltaTime;

        /// NPCを自動的に動かす( 乱数 )
        /// 
        if (modeRand)
        {
            if (waitTime > moveTimeRange)
            {
                if (move)
                {
                    move = false;
                    animationNum = AnimationNum.Idle;
                }
                else
                {
                    move = true;
                    NPCinputVec = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                    NPCinputVec = NPCinputVec.normalized;
                    moveForward = transform.forward * NPCinputVec.y + transform.right * NPCinputVec.x;

                    animationNum = AnimationNum.Walk;
                }
                waitTime = 0.0f;
            }
        }

        /// NPCを自動的に動かす( 目的地を往復 )
        /// 
        else
        {
            if (waitTime > moveTimeRange && !move)
            {
                move = true;
                waitTime = 0.0f;
            }
        }
    }

    void FixedUpdate () {

        /// NPCを球面に対して垂直に立たせる
        surfaceNormal = transform.position - Vector3.zero;
        surfaceNormal = surfaceNormal.normalized;

        dirVecZ = Vector3.Scale(transform.forward, new Vector3(1, 1, 1)).normalized;
        dirVecZ = Vector3.ProjectOnPlane(dirVecZ, surfaceNormal);
        transform.rotation = Quaternion.LookRotation(dirVecZ, surfaceNormal);

        /// NPCを自動的に動かす( 乱数 )
        /// 

        if (modeRand)
        {
            if (move)
            {
                moveVec = Vector3.ProjectOnPlane(moveForward, surfaceNormal) * walkspeed;
                moveVec += (Vector3.zero - moveVec) * moveIn;
                transform.position += moveVec;
                transform.rotation = Quaternion.LookRotation(moveVec.normalized, surfaceNormal);
            }
        }
        /// NPCを自動的に動かす( 目的地を往復 )
        /// 

        else
        {
            if (move)
            {
                //目的地が1の場合
                if (dest1)
                {
                    moveForward = destPos1.transform.position - transform.position;

                    if (moveForward.magnitude < 2.0f)
                    {
                        dest1 = false;
                        dest2 = true;
                        move = false;
                    }

                    moveForward = moveForward.normalized;
                    moveVec = Vector3.ProjectOnPlane(moveForward, surfaceNormal) * walkspeed;
                    moveVec += (Vector3.zero - moveVec) * moveIn;
                    transform.position += moveVec;
                    transform.rotation = Quaternion.LookRotation(moveVec.normalized, surfaceNormal);

                }

                // 目的地が2の場合
                else if (dest2)
                {
                    moveForward = destPos2.transform.position - transform.position;

                    if (moveForward.magnitude < 2.0f)
                    {
                        dest1 = true;
                        dest2 = false;
                        move = false;
                    }

                    moveForward = moveForward.normalized;
                    moveVec = Vector3.ProjectOnPlane(moveForward, surfaceNormal) * walkspeed;
                    moveVec += (Vector3.zero - moveVec) * moveIn;
                    transform.position += moveVec;
                    transform.rotation = Quaternion.LookRotation(moveVec.normalized, surfaceNormal);

                }
            }
        }


        /// アニメーション状態によって動作を変える
        /// 
        switch (animationNum)
        {
            case AnimationNum.Idle:
                {
                    animator.SetBool("is_walk", false);
                    animator.SetBool("is_running", false);
                    //particleManager.SetSmokeFlg(false);
                    break;
                }
            case AnimationNum.Walk:
                {
                    animator.SetBool("is_walk", true);
                    animator.SetBool("is_running", false);
                    //particleManager.SetSmokeFlg(true);

                    //footStampTime += Time.deltaTime;

                    //if (footStampTime > footStampSetTime)
                    //{
                    //    footStampTime = 0;
                    //    Instantiate(footStampPrefab, transform.position, transform.rotation);
                    //}
                    break;
                }
            case AnimationNum.Running:
                {
                    animator.SetBool("is_running", true);
                    //animator.SetBool("is_walk", false);
                    //particleManager.SetSmokeFlg(true);

                    //footStampTime += Time.deltaTime;

                    //if (footStampTime > footStampSetTime)
                    //{
                    //    footStampTime = 0;
                    //    Instantiate(footStampPrefab, transform.position, transform.rotation);
                    //}
                    break;
                }
        }

    }
}
