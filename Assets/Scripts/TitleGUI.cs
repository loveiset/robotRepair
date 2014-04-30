using UnityEngine;
using System.Collections;

public class TitleGUI : MonoBehaviour {
    public GUISkin customSkin;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnGUI()
    {
        GUI.skin = customSkin;
        int buttonW = 100;
        int buttonH = 50;

        float halfScreenW = Screen.width / 2;
        float halfButtonW = buttonW / 2;

        if (GUI.Button(new Rect(halfScreenW-halfButtonW, 560, buttonW, buttonH), "Play Game"))
        {
            Application.LoadLevel("game");
        }
    }
}
