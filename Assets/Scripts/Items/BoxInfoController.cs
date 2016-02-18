using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoxInfoController : MonoBehaviour {
	public float MotionEffect;
	public float MotionTime;
	public float PauseTime;
    public string Title;
	public string Message;
	public Color PauseColor;
	protected Vector3 m_StartPos;
	protected bool m_InPause;

	// Use this for initialization
	void Start () {
		m_StartPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D hit)
	{
		if(!m_InPause && hit.gameObject.tag == "Player")
		{
			iTween.MoveFrom (this.gameObject, m_StartPos + Vector3.up * MotionEffect, MotionTime); 
			this.StartCoroutine (InfoCoroutine ());
		}
	}
	
	IEnumerator InfoCoroutine()
	{
		m_InPause = true;
		yield return new WaitForSeconds(MotionTime);
		this.GetComponent<SpriteRenderer> ().color = PauseColor;

		GameObject messageBox = GameObject.Find("MessageBox") as GameObject;
		messageBox.GetComponent<MessageBoxController> ().ShowDialog(Title, Message);	

		yield return new WaitForSeconds(PauseTime);
		this.GetComponent<SpriteRenderer> ().color = Color.white;
		m_InPause = false;
	}
}
