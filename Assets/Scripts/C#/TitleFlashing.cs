using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFlashing : MonoBehaviour {

    public Text TitleFlash;
    


    bool Flag;
    float Color;

    // Use this for initialization
    void Start()
    {
        Color = 0;
    }
    // Update is called once per frame
    void Update()
    {
        //テキストの透明度を変更する
        TitleFlash.color = new Color(0, 0, 0, Color);
        if (Flag)
            Color -= Time.deltaTime;
        else
            Color += Time.deltaTime;
        if (Color < 0)
        {
            Color = 0;
            Flag = false;
        }
        else if (Color > 1)
        {
            Color = 1;
            Flag = true;
        }
    }
}
