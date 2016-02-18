using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageBoxController : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		this.GetComponent<Image> ().enabled = false;
		this.GetComponentInChildren<Text> ().text = string.Empty;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowDialog(string title, string text)
	{
		this.GetComponent<Image> ().enabled = true;
		transform.FindChild("Text").GetComponent<Text> ().text = text;
        transform.FindChild("Title").GetComponent<Text>().text = title;
		Time.timeScale = 0;
	}

	public void Close()
	{
		Time.timeScale = 1;
		this.GetComponent<Image> ().enabled = false;
        transform.FindChild("Text").GetComponent<Text>().text = string.Empty;
        transform.FindChild("Title").GetComponent<Text>().text = string.Empty;        
	}
}
