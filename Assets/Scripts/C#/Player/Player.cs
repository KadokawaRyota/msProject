using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	GameObject controllerManager;

	Vector3 vec;
	float length;
	float moveSpeed;
    
	Vector3 move;

    public float MOVE_SPEED;         //移動速度係数
    public float MOVE_COEFFIENT;      //慣性係数

    enum PLAYER_STATE
	{
		PLAYER_NONE = 0,
		PLAYER_MOVE
	};

	PLAYER_STATE state;

	// Use this for initialization
	void Start () {
		state = PLAYER_STATE.PLAYER_NONE;

        controllerManager = GameObject.Find("PuniconCamera/ControllerManager");//.transform.FindChild("ControllManager").gameObject;

    }
	
	// Update is called once per frame
	void Update () {

		SetStatus();

        //長さの調節
		if(length >= 100)
		{
			state = PLAYER_STATE.PLAYER_MOVE;
			
			if(length >= 300)
			{
				length = 300;
			}

			length *= 0.1f;
				
		}
		else
		{
			state = PLAYER_STATE.PLAYER_NONE;
		}

		if(state == PLAYER_STATE.PLAYER_MOVE)
		{

			move = new Vector3(vec.x * length * MOVE_SPEED,
								0.0f,
								vec.y * length * MOVE_SPEED);
		}

		gameObject.transform.localPosition += move;
		move += new Vector3((0 - move.x) * MOVE_COEFFIENT,
							0.0f,
							(0 - move.z) * MOVE_COEFFIENT);
		
		/*Touch touch = Input.GetTouch(0);

		if (touch.phase == TouchPhase.Moved)
		{
			gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + direction.x * length * 0.000001f,
															gameObject.transform.localPosition.y,
															gameObject.transform.localPosition.z + direction.y * length * 0.000001f);
		}
		else if(touch.phase == TouchPhase.Ended)
		{
			gameObject.transform.localPosition += new Vector3((0 - gameObject.transform.localPosition.x) * 0.2f,
															0.0f,
															(0 - gameObject.transform.localPosition.z) * 0.2f);
		}
		*/
		
	}

	void SetStatus()
	{
		Scr_ControllerManager controller = controllerManager.GetComponent<Scr_ControllerManager>();

		vec = (controller.ControllerVec).normalized;
		length = controller.ControllerVecLength;
		Debug.Log(vec);
	}
}
