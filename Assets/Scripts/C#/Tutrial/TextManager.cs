using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    [SerializeField]
    HomeTutorial homeTuto;

    [SerializeField]
    Home homeCanvas;

    //文字出力用
    [SerializeField]
    Text text;

    //文字の概要の最大数
    [SerializeField]
    int textLineMax;

    //文字の概要
    [SerializeField]
    string[] textLine;

    [SerializeField]
    int[] textSize;

    int textLineNum = 0;

    [SerializeField]
    InputField inputField;

    [SerializeField]
    GameObject NextButton;

    CharactorInfo charaInfo;

	AudioManager audioManager;

	// Use this for initialization
	void Start () {
        GameObject info = GameObject.Find("CharactorInfo");
        if(null != info)
        {
            charaInfo = info.GetComponent<CharactorInfo>();
        }
        text.text = textLine[0];
        text.fontSize = textSize[0];

		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
	}

    public void NextTextHome()
    {
        textLineNum++;      //次の文字へ

        if(textLineNum == textLineMax)
        {
            homeTuto.GetTextManager().SetNameTuto(true);
            Destroy(homeTuto.gameObject);
            return;
        }

        if (textLineNum == 4)
        {
            string name = inputField.text;

			//名前未入力時テキスト　
            if (name == "")
            {
                text.fontSize = 40;
                text.text = "んっ?\n言いたくないのかな？\nじゃぁ「ふれんず」\nって呼ぶね!";
                charaInfo.SetPlayerName("ふれんず");
                homeCanvas.SetName("ふれんず");
            }
            else
            {
                text.text = "「" + name + "」" + textLine[textLineNum];
                charaInfo.SetPlayerName(name);
                homeCanvas.SetName(name);
            }
        }
        else
        {
            text.text = textLine[textLineNum];
        }


		if (textLineNum == 3) {
			NextButton.SetActive (false);
			inputField.gameObject.SetActive (true);
		} else if (textLineNum >= 3) {
			NextButton.SetActive (true);
			inputField.gameObject.SetActive (false);
		}

        text.fontSize = textSize[textLineNum];

		audioManager.Play_SE (AudioManager.SE.OpenChat);

    }
}
