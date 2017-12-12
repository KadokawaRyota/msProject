//------------------------------------------------------------------------------
//          ファイルインクルード
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------------
//          メイン
//------------------------------------------------------------------------------
public class Scr_FingerManager : MonoBehaviour 
{
    //--------------------------------------------------------------------------
    //          変数定義
    //--------------------------------------------------------------------------
    public Touch FirstFiger;        // タップ情報(1本目)
    public Touch SecondFiger;       // タップ情報(2本目)
    private int nTouchCount;        // フレームカウント
    private int nTouchCountOld;     // フレームカウント(前フレーム)
    public int nContollerID;
    public int nCameraID;



    public Scr_ControllerManager    ControllerManager;
    public Scr_CameraController     CameraController;
    public PunipuniController punicon;

    //--------------------------------------------------------------------------
    //          初期化処理
    //--------------------------------------------------------------------------
	void Start () 
    {
        nTouchCount     = 0;
        nTouchCountOld  = 0;
        nContollerID = -1;
        nCameraID = -1;


        if (GameObject.Find("PuniconCamera/ControllerManager") != null)
        {
            ControllerManager = GameObject.Find("PuniconCamera/ControllerManager").GetComponent<Scr_ControllerManager>();
        }
       
        if (GameObject.Find("PuniconCamera/CameraController") != null)
        {
            CameraController = GameObject.Find("PuniconCamera/CameraController").GetComponent<Scr_CameraController>();
        }

        if (GameObject.Find("PuniconCamera/Punicon") != null)
        {
            punicon = GameObject.Find("PuniconCamera/Punicon").GetComponent<PunipuniController>();
        }
	}

    //--------------------------------------------------------------------------
    //          更新処理
    //--------------------------------------------------------------------------
	void Update () 
    {
        ////        タップ本数情報保存
        ////////////////////////////////////////////////////////////////////////
        nTouchCount = Input.touchCount;
        
        ////        規定本数以上タップされていた場合、値を補正
        ////////////////////////////////////////////////////////////////////////
        if (nTouchCount > 2)
        {
            nTouchCount = 2;
        }

        ////        前フレームと今のフレームのタップ本数が違う場合の処理
        ////////////////////////////////////////////////////////////////////////
        if (nTouchCountOld != nTouchCount)
        {
            ////        各コントローラへのタップ情報割り振り
            ////////////////////////////////////////////////////////////////////
            SetFingerInfo(nTouchCount);  
        }

        UpdateSetFingerInfo(nTouchCount);
        //------------------------------------------------------------------------------------------
        //if (nTouchCount >= 1)
        ControllerManager.ControllerUpdate();
        //if (nTouchCount >= 2)
        CameraController.CameraUpdate();


        //------------------------------------------------------------------------------------------

        ////        タップ本数を保存
        ////////////////////////////////////////////////////////////////////////
        nTouchCountOld = nTouchCount;
    }

    //--------------------------------------------------------------------------
    //          各コントローラーへのタップ情報保存関数
    //--------------------------------------------------------------------------
    public void SetFingerInfo(int TouchCount)
    {
        ////        前フレームのタップ本数が0本
        ////////////////////////////////////////////////////////////////////
        if (nTouchCountOld == 0)
        {
            // ぷにぷにコントローラに1本目のタップ情報を割り当て
            if (TouchCount >= 1)
            {
                nContollerID = Input.GetTouch(0).fingerId;
            }

            // カメラコントローラに2本目のタップ情報を割り当て
            if (TouchCount == 2)
            {
                nCameraID = Input.GetTouch(1).fingerId;
            }
        }

        ////        前フレームのタップ本数が1本
        ////////////////////////////////////////////////////////////////////
        else if (nTouchCountOld == 1)
        {
            if (TouchCount == 0)
            {
                ControllerManager.TouchRelease();
                punicon.EndPunipuni();
            }

            // カメラコントローラに2本目のタップ情報を割り当て
            if (TouchCount == 2)
            {
                // 
                if (nContollerID == 0)
                {
                    SecondFiger = Input.GetTouch(1);
                    nCameraID = Input.GetTouch(1).fingerId;
                    Scr_CameraController.tTouchInfo = SecondFiger;
                }

                else if (nContollerID == 1)
                {
                    SecondFiger = Input.GetTouch(0);
                    nCameraID = Input.GetTouch(0).fingerId;
                    Scr_CameraController.tTouchInfo = SecondFiger;                
                }

            }
        }

        ////        前フレームのタップ本数が2本
        ////////////////////////////////////////////////////////////////////
        else if (nTouchCountOld == 2)
        {
            if (TouchCount == 0)
            {
                ControllerManager.TouchRelease();
                punicon.EndPunipuni();
            }

            // ぷにぷにコントローラに1本目のタップ情報を割り当て
            if (TouchCount == 1)
            {
                nContollerID = Input.GetTouch(0).fingerId;  
            }

            CameraController.ResetValue();
        }
    }

    void UpdateSetFingerInfo(int FingerCount)
    {

        
        if (FingerCount == 1)
        {
            FirstFiger = Input.GetTouch(0);
            Scr_ControllerManager.tTouchInfo = FirstFiger;
        }

        if (FingerCount == 2)
        {
            if (Input.GetTouch(0).fingerId == nContollerID)
            {
                FirstFiger = Input.GetTouch(0);
                Scr_ControllerManager.tTouchInfo = FirstFiger;

                SecondFiger = Input.GetTouch(1);
                Scr_CameraController.tTouchInfo = SecondFiger;
            }

            else
            {
                FirstFiger = Input.GetTouch(1);
                Scr_ControllerManager.tTouchInfo = FirstFiger;

                SecondFiger = Input.GetTouch(0);
                Scr_CameraController.tTouchInfo = SecondFiger;               
            }
        }
    }
}
