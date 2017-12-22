using UnityEngine;
using UnityEngine.UI;

public class HomeTutorial : MonoBehaviour {

    TutorialManager tutoManager;

    [SerializeField]
    TextManager textManager;

	// Use this for initialization
	void Start () {
        GameObject manager = GameObject.Find("TutorialManager");

        if(null != manager)
        {
            tutoManager = manager.GetComponent<TutorialManager>();
        }


        //名前入力チュートリアルを終えてなかったら
        if(!tutoManager.GetNameTuto())
        {
            textManager.gameObject.SetActive(true);
        }
        else
        {
            Destroy(gameObject);
        }
	}

    public TutorialManager GetTextManager()
    {
        return tutoManager;
    }
}
