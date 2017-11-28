using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{
    public int nTimeMin; 
    public int nTimeSec;
    public bool bCountDownFlug;
    private float fTimeCounter;

    public RawImage NumObjectMin00;
    public RawImage NumObjectMin01;
    public RawImage NumObjectSec00;
    public RawImage NumObjectSec01;

    private float TexWidth = 1.0f / 11.0f;

    void Start () 
    {
        bCountDownFlug = false;
        fTimeCounter = 0.0f;


        
	}
    
    void Update () 
    {
        if (bCountDownFlug)
        {
            fTimeCounter = fTimeCounter + Time.deltaTime;

            if (fTimeCounter >= 1.0f)
            {
                nTimeSec--;

                if (nTimeSec < 0)
                {
                    if (nTimeMin <= 0)
                    {
                        nTimeMin = 0;
                        nTimeSec = 0;
                        bCountDownFlug = false;
                    }

                    else
                    {
                        nTimeMin--; 
                        nTimeSec = 59;
                    }
                }

                fTimeCounter = 0.0f;
            }
        }

        UpdateNumObject();

	}

    void UpdateNumObject()
    {
        NumObjectMin00.uvRect = new Rect((nTimeMin / 10) * TexWidth, 0, TexWidth, 1);   // 10の桁
        NumObjectMin01.uvRect = new Rect((nTimeMin % 10) * TexWidth, 0, TexWidth, 1);   // 1の桁
        NumObjectSec00.uvRect = new Rect((nTimeSec / 10) * TexWidth, 0, TexWidth, 1);   // 10の桁
        NumObjectSec01.uvRect = new Rect((nTimeSec % 10) * TexWidth, 0, TexWidth, 1);   // 1の桁
    }
}


