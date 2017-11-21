using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour {

	[SerializeField]
	string sceneName;

    private Image image;

    float m_Time;


    [SerializeField]
	float onTime;   //ついてる時間
    [SerializeField]
    float offTime;  //消えてる時間

    void Start()
    {
        m_Time = 0.0f;
        image = GetComponent<Image>();
    }

    void Update()
    {
        m_Time += Time.deltaTime;

        if (m_Time > onTime && image.enabled)
        {
            image.enabled = false;
            m_Time = 0.0f;
        }
        else if(m_Time > offTime && !( image.enabled ))
        {
            image.enabled = true;
            m_Time = 0.0f;
        }
    }

	public void LoadFade()
	{
		//Transitioner.Instance.TransitionToScene (sceneName);

	}
}

