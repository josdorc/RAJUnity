using UnityEngine;
using System.Collections;

public class WormController : MonoBehaviour {

	public float Speed;
    public float Pause;
	public float Delay;
	public Vector3 Motion;
	protected Vector3 m_StartPosition;
			
	// Use this for initialization
	void Start () {
		m_StartPosition = this.transform.position;

        Invoke("OnMoveBegin", Delay);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMoveBegin()
	{
		iTween.MoveTo (this.gameObject,iTween.Hash(
			"position", m_StartPosition + Motion, 
			"speed", Speed, 
			"delay", Pause, 
			"easetype", iTween.EaseType.linear,
			"oncomplete", 
			"OnMoveEnd"));
	}

	void OnMoveEnd()
	{
		iTween.MoveTo (this.gameObject,iTween.Hash(
			"position", m_StartPosition, 
			"speed", Speed, 
			"delay", Pause, 
			"easetype", iTween.EaseType.linear,
			"oncomplete", 
			"OnMoveBegin"));
	}
}
