using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour {

    public enum FADE
    {
        NONE = 0,
        IN,
        OUT
    };

    FADE fade;
    string sceneName;
    Color c;

    float speed = 0.01f;

    public Image img;
    public GameObject manager;
	
    // Use this for initialization
	void Start () {
		GameObject f = GameObject.Find ("FadeManager");
		if (f != null) {
			c = new Color (0.0f, 0.0f, 0.0f, 0.0f);
			fade = FADE.NONE;
			DontDestroyOnLoad (manager.gameObject);
		} else {
			Destroy (gameObject);
		}
	}

    // Update is called once per frame
    void Update () {

        //フェード開始フラグ
        if (fade == FADE.IN)
        {
            if (c.a < 1.0f)
            {
                c.a += speed;
                
                if(c.a > 1.0f)
                {
                    c.a = 1.0f;
                    SceneManager.LoadScene(sceneName);
                    fade = FADE.OUT;
                }
            }
        }
        else if (fade == FADE.OUT)
        {
            if (c.a > 0.0f)
            {
                c.a -= speed * 2;

                if (c.a < 0.0f)
                {
                    c.a = 0.0f;
                    fade = FADE.NONE;
                }
            }
        }

        gameObject.GetComponent<Image>().color = c;
	}

    public void SetFade(string name)
    {
        if (fade == FADE.NONE)
        {
            fade = FADE.IN;
            sceneName = name;

           /*if(name == "Title")
            {
                img.gameObject.SetActive(true);
            }
            else
            {
                img.gameObject.SetActive(false);
            }*/
        }
    }
}
