using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("LoadCheckPoint", 2.0f);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadCheckPoint()
    {
        if (Data.Game.CheckPointLevel != null && Data.Game.CheckPointLevel != string.Empty)
            Application.LoadLevel(Data.Game.CheckPointLevel);
        else
            Application.LoadLevel("Home");
    }
}
