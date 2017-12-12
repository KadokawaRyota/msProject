using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_cameraDebugTest : MonoBehaviour 
{
    [SerializeField]
    Text DebugTextStartX;
    [SerializeField]
    Text DebugTextEndX;
	[SerializeField]
	Text DebugTextLength;
    [SerializeField]
    Text DebugTextLengthMax;
    [SerializeField]
    Text DebugTextLengthMin;

    Scr_CameraController CameraController;
    OfflineCameraStand Camera;

    float MaxLength;
    float MinLength;
    float NowLength;

	// Use this for initialization
	void Start () 
    {
        if (GameObject.Find("PuniconCamera/CameraController") != null)
        {
            CameraController = GameObject.Find("PuniconCamera/CameraController").GetComponent<Scr_CameraController>();
        }

        if (GameObject.Find("OfflinePlayer_Tanuki/StandCameraObj") != null)
        {
            Camera = GameObject.Find("OfflinePlayer_Tanuki/StandCameraObj").GetComponent<OfflineCameraStand>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        DebugTextStartX.text = CameraController.CameraMoveLength_Start.x.ToString("N4");
        DebugTextEndX.text = CameraController.CameraMoveLength_End.x.ToString("N4");
        DebugTextLength.text = CameraController.fCameraMoveLength.ToString("N4");
        NowLength = CameraController.fCameraMoveLength;
        
        /*
        DebugTextStartX.text = Camera.inputStart.x.ToString("N4");
        DebugTextEndX.text = Camera.inputEnd.x.ToString("N4");
        DebugTextLength.text = Camera.inputLength.ToString("N4");
        NowLength = Camera.inputLength;
        */

        if(MaxLength < NowLength)
        {
            MaxLength = NowLength;
            DebugTextLengthMax.text = MaxLength.ToString("N4");
        }
        
        if (MinLength > NowLength)
        {
            MinLength = NowLength;
            DebugTextLengthMin.text = MinLength.ToString("N4");
        }

        if (Input.touchCount >= 3)
        { 
            Reset();
        }


    }

    void Reset()
    {
        MaxLength = 0.0f;
        MinLength = 500.0f;
        NowLength = 0.0f;

        DebugTextStartX.text = 0.0f.ToString("N4");
        DebugTextEndX.text = 0.0f.ToString("N4");
        DebugTextLength.text = 0.0f.ToString("N4");
        DebugTextLengthMax.text = 0.0f.ToString("N4");
        DebugTextLengthMin.text = 0.0f.ToString("N4");
    }
}
