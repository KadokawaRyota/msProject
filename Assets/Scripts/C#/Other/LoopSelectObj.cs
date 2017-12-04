using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSelectObj : MonoBehaviour {

    public GameObject VegitField;
    public GameObject SnowField;
    public GameObject DesertField;
    public GameObject PlaneField;

    private float threRight = 25.0f;
    private float threLeft = -25.0f;

    private float diffVegit;
    private float diffSnow;
    private float diffDesert;
    private float diffPlane;

    void Start () {
		
	}
	
	void Update () {

        // 各街の位置とカメラ位置を比較
        diffVegit = VegitField.transform.position.x - transform.position.x;
        diffSnow = SnowField.transform.position.x - transform.position.x;
        diffDesert = DesertField.transform.position.x - transform.position.x;
        diffPlane = PlaneField.transform.position.x - transform.position.x;


        // カメラ右側のフィールドの補正
        if(diffVegit > threRight)
        {
            VegitField.transform.position += new Vector3(-40.0f, 0.0f, 0.0f);
        }
        else if (diffSnow > threRight)
        {
            SnowField.transform.position += new Vector3(-40.0f, 0.0f, 0.0f);
        }
        else if (diffDesert > threRight)
        {
            DesertField.transform.position += new Vector3(-40.0f, 0.0f, 0.0f);
        }
        else if (diffPlane > threRight)
        {
            PlaneField.transform.position += new Vector3(-40.0f, 0.0f, 0.0f);
        }

        // カメラ左側のフィールドの補正
        if ( diffVegit < threLeft )
        {
            VegitField.transform.position += new Vector3(40.0f, 0.0f, 0.0f);
        }
        else if (diffSnow < threLeft)
        {
            SnowField.transform.position += new Vector3(40.0f, 0.0f, 0.0f);
        }
        else if (diffDesert < threLeft)
        {
            DesertField.transform.position += new Vector3(40.0f, 0.0f, 0.0f);
        }
        else if (diffPlane < threLeft)
        {
            PlaneField.transform.position += new Vector3(40.0f, 0.0f, 0.0f);
        }

    }
}
