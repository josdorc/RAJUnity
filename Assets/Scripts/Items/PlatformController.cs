using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {
    public float MotionTime;
    public float Pause;
    public float Delay;
    protected Vector3[] m_Path;

    // Use this for initialization
	void Start () {
        
        m_Path = new Vector3[transform.childCount];

        for(int i=0; i < transform.childCount; i++)
            m_Path[i] = transform.GetChild(i).position;

        Invoke("OnMotionStart", Delay);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMotionStart()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash(
            "path", m_Path,
            "time", MotionTime,
            "delay", Pause,
            "easetype", iTween.EaseType.linear,
            "looptype", iTween.LoopType.pingPong));
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.transform.tag == "Player")
            hit.transform.parent = this.transform;
    }

    void OnCollisionExit2D(Collision2D hit)
    {
        if (hit.transform.tag == "Player")
            hit.transform.parent = null;
    }
}
