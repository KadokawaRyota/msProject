using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_ControllerDebugText : MonoBehaviour
{
    private IEnumerator Start()
    {
        while (true)
        {
            //Debug.LogFormat("Time:{0}", System.DateTime.Now);
            yield return new WaitForSeconds(1.0f);
        }
    }
} 
