using UnityEngine;
using System.Collections;

public class MadFireController : MonoBehaviour {

    public float MotionTime;
    public float Height;
    public float Pause;
    public float Delay;

    protected Vector3 m_StartPos;
    // Use this for initialization
    void Start()
    {
        m_StartPos = this.transform.position;
        Invoke("OnMotionStart", Delay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMotionStart()
    {
        this.transform.localScale = new Vector3(1, -1, 1);

        iTween.MoveTo(this.gameObject, iTween.Hash(
            "position", m_StartPos + Vector3.up * Height,
            "time", MotionTime,
            "delay", Pause,
            "easetype", iTween.EaseType.easeOutCirc,    
            "oncomplete", "OnMotionEnd"));
    }

    void OnMotionEnd()
    {
        this.transform.localScale = new Vector3(1, 1, 1);

        iTween.MoveTo(this.gameObject, iTween.Hash(
            "position", m_StartPos,
            "time", MotionTime,
            "easetype", iTween.EaseType.easeInCirc,
            "oncomplete", "OnMotionStart"));
    }
}
