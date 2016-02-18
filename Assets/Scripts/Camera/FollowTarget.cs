using UnityEngine;
using System.Collections;
using TileEditor;

public class FollowTarget : MonoBehaviour {

	public float Smoothing;
	public Vector2 Offset;
    
	protected Transform m_Player;
	protected GameObject m_Area;
	public Rect m_FollowArea;


	private void Start()
	{
        Vector3 Min =  this.transform.FindChild("Min").position;
        Vector3 Max =  this.transform.FindChild("Max").position;
		m_Player = GameObject.FindWithTag ("Player").transform;
		m_FollowArea = new Rect (Min.x, Min.y, Max.x - Min.x, Max.y - Min.y);
	}

	void Update () 
	{
		if(m_Player != null)
		{
			Vector3 offset = new Vector3 (Offset.x, Offset.y, this.transform.position.z);
			Vector3 pos = Vector3.Lerp (transform.position, m_Player.position + offset, Time.deltaTime * Smoothing);
			pos.y = Mathf.Clamp(pos.y, m_FollowArea.y, m_FollowArea.y + m_FollowArea.height);
			pos.x = Mathf.Clamp(pos.x, m_FollowArea.x, m_FollowArea.x + m_FollowArea.width);
           
			transform.position = pos;
		}
	}
}
